using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AniX_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;
using Anix_Shared.DomainModels;

namespace AniX_DAL
{
    public class UserDAL : BaseDAL, IUserManagement
    {
        private readonly IAzureBlobService _blobService;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;

        public UserDAL(
            IAzureBlobService blobService,
            IConfiguration configuration,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService
        ) : base(configuration)
        {
            _blobService = blobService;
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
        }

        public async Task<User> AuthenticateUserAsync(string username, string rawPassword)
        {
            User user = null;
            try
            {
                await connection.OpenAsync();
                SqlCommand command = PrepareAuthenticateUserCommand(username);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user = MapReaderToUser(reader);
                    if (ValidateUserPassword(user, rawPassword))
                    {
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

            return null;
        }

        public async Task<OperationResult> CreateAsync(User user, Stream profileImageStream = null, string contentType = null)
        {
            OperationResult result = new OperationResult();
            SqlTransaction transaction = null;
            try
            {
                await connection.OpenAsync();
                transaction = connection.BeginTransaction();

                string insertQuery = @"INSERT INTO [User] (Username, Password, Salt, Email, RegistrationDate, Banned, IsAdmin)
                               OUTPUT INSERTED.Id
                               VALUES (@username, @password, @salt, @email, @registrationDate, @banned, @isAdmin);
                               SELECT SCOPE_IDENTITY();";

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction);
                insertCommand.Parameters.AddWithValue("@username", user.Username);
                (string Password, string Salt) = user.RetrieveCredentials();
                insertCommand.Parameters.AddWithValue("@password", Password);
                insertCommand.Parameters.AddWithValue("@salt", Salt);
                insertCommand.Parameters.AddWithValue("@email", user.Email);
                insertCommand.Parameters.AddWithValue("@registrationDate", user.RegistrationDate);
                insertCommand.Parameters.AddWithValue("@banned", user.Banned);
                insertCommand.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

                var userId = (int)(await insertCommand.ExecuteScalarAsync());
                user.Id = userId;

                string profileImagePath = "https://anix.blob.core.windows.net/anixprofile/66574.png";

                if (profileImageStream != null && contentType != null)
                {
                    try
                    {
                        profileImagePath = await _blobService.UploadImageAsync(profileImageStream, user.Id.ToString(), contentType);
                    }
                    catch
                    {
                        throw new Exception("Failed to upload the image.");
                    }
                }

                string updateQuery = "UPDATE [User] SET ProfileImagePath = @profileImagePath WHERE Id = @id";
                SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction);
                updateCommand.Parameters.AddWithValue("@profileImagePath", profileImagePath);
                updateCommand.Parameters.AddWithValue("@id", userId);

                await updateCommand.ExecuteNonQueryAsync();

                transaction.Commit();

                result.Success = true;
                result.Message = "User created successfully.";
                return result;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                result.Success = false;
                result.Message = ex.Message;

                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
            }
            finally
            {
                transaction?.Dispose();
                await connection.CloseAsync();
            }
            return result;
        }

        public async Task<bool> UpdateAsync(User user, bool updateProfileImage = true)
        {
            try
            {
                await connection.OpenAsync();
                Console.WriteLine("Connection opened.");

                var queryBuilder = new StringBuilder(@"
            UPDATE [User] SET 
                Username = @username, 
                Password = @password, 
                Salt = @salt, 
                Email = @email, 
                Banned = @banned, 
                IsAdmin = @isAdmin");

                if (updateProfileImage)
                {
                    queryBuilder.Append(", ProfileImagePath = @profileImagePath");
                }

                queryBuilder.Append(" WHERE Id = @id");
                string query = queryBuilder.ToString();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    (string password, string salt) = user.RetrieveCredentials();

                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = user.Id;
                    command.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar)).Value = user.Username;
                    command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar)).Value = password;
                    command.Parameters.Add(new SqlParameter("@salt", SqlDbType.NVarChar)).Value = salt;
                    command.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar)).Value = user.Email;
                    command.Parameters.Add(new SqlParameter("@banned", SqlDbType.Bit)).Value = user.Banned;
                    command.Parameters.Add(new SqlParameter("@isAdmin", SqlDbType.Bit)).Value = user.IsAdmin;

                    if (updateProfileImage)
                    {
                        command.Parameters.Add(new SqlParameter("@profileImagePath", SqlDbType.NVarChar)).Value = (object)user.ProfileImagePath ?? DBNull.Value;
                    }

                    foreach (SqlParameter param in command.Parameters)
                    {
                        Console.WriteLine($"{param.ParameterName}: {param.Value}");
                    }

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"Update command executed, affected rows: {rowsAffected}");

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred:");
                Console.WriteLine(ex.ToString());

                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }

                throw;
            }

            finally
            {
                await connection.CloseAsync();
                Console.WriteLine("Connection closed and method exiting.");
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                await connection.OpenAsync();

                // Start a transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete related entities first
                        string deleteReviewsQuery = "DELETE FROM [Review] WHERE UserId = @id";
                        string deleteUserAnimeQuery = "DELETE FROM [User_Anime] WHERE UserId = @id";
                        string deleteAnimeViewsQuery = "DELETE FROM [AnimeViews] WHERE UserId = @id";

                        using (SqlCommand deleteReviewsCommand = new SqlCommand(deleteReviewsQuery, connection, transaction))
                        {
                            deleteReviewsCommand.Parameters.AddWithValue("@id", id);
                            await deleteReviewsCommand.ExecuteNonQueryAsync();
                        }

                        using (SqlCommand deleteUserAnimeCommand = new SqlCommand(deleteUserAnimeQuery, connection, transaction))
                        {
                            deleteUserAnimeCommand.Parameters.AddWithValue("@id", id);
                            await deleteUserAnimeCommand.ExecuteNonQueryAsync();
                        }

                        using (SqlCommand deleteAnimeViewsCommand = new SqlCommand(deleteAnimeViewsQuery, connection, transaction))
                        {
                            deleteAnimeViewsCommand.Parameters.AddWithValue("@id", id);
                            await deleteAnimeViewsCommand.ExecuteNonQueryAsync();
                        }

                        // Delete the user
                        string deleteUserQuery = "DELETE FROM [User] WHERE Id = @id";
                        using (SqlCommand deleteUserCommand = new SqlCommand(deleteUserQuery, connection, transaction))
                        {
                            deleteUserCommand.Parameters.AddWithValue("@id", id);
                            int rowsAffected = await deleteUserCommand.ExecuteNonQueryAsync();

                            result.Success = rowsAffected > 0;
                            result.Message = result.Success ? "User deleted successfully." : "User not found or could not be deleted.";
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction if an error occurs
                        transaction.Rollback();

                        if (ex is SqlException sqlEx && sqlEx.Number == 547)
                        {
                            result.Success = false;
                            result.Message = "User cannot be deleted because it is referenced by other entities.";
                            await _errorLoggingService.LogErrorAsync(sqlEx, LogSeverity.Warning);
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "An error occurred while deleting the user.";
                            await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                        }
                    }
                }
            }
            finally
            {
                await connection.CloseAsync();
            }
            return result;
        }


        public async Task<User> GetUserFromIdAsync(int id)
        {
            return await GetUserByIdAsync(id);
        }

        private async Task<User> GetUserByIdAsync(int id)
        {
            User user = null;
            try
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM [User] WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user = MapReaderToUser(reader);
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }

                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

            return user;
        }

        public async Task<List<User>> FetchFilteredAndSearchedUsersAsync(string filter, string searchTerm)
        {
            List<User> users = new List<User>();
            var queryBuilder = new StringBuilder("SELECT * FROM [dbo].[User] WHERE 1 = 1");

            var filters = new Dictionary<string, string>
            {
                { "Admin", "IsAdmin = 1" },
                { "User", "IsAdmin = 0" },
                { "Banned", "Banned = 1" },
                { "Not Banned", "Banned = 0" }
            };

            if (!string.IsNullOrEmpty(filter) && filters.ContainsKey(filter))
            {
                queryBuilder.Append($" AND {filters[filter]}");
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                queryBuilder.Append(" AND (Username LIKE @searchTerm OR Email LIKE @searchTerm)");
            }

            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(queryBuilder.ToString(), connection))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        var searchTermParam = $"%{searchTerm}%";
                        command.Parameters.AddWithValue("@searchTerm", searchTermParam);
                    }

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        users.Add(MapReaderToUser(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }

                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

            return users;
        }

        public async Task<bool> DoesUsernameExistAsync(string username)
        {
            await connection.OpenAsync();
            string query = "SELECT COUNT(1) FROM [User] WHERE Username = @username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            int count = Convert.ToInt32(await command.ExecuteScalarAsync());
            await connection.CloseAsync();
            return count > 0;
        }

        public async Task<bool> DoesEmailExistAsync(string email)
        {
            await connection.OpenAsync();
            string query = "SELECT COUNT(1) FROM [User] WHERE Email = @email";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@email", email);
            int count = Convert.ToInt32(await command.ExecuteScalarAsync());
            await connection.CloseAsync();
            return count > 0;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            User user = null;
            try
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM [User] WHERE Email = @email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user = MapReaderToUser(reader);
                }
            }
            catch (Exception ex)
            {
                await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

            return user;
        }

        private SqlCommand PrepareAuthenticateUserCommand(string username)
        {
            string query = "SELECT * FROM [User] WHERE Username = @username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            return command;
        }

        private User MapReaderToUser(SqlDataReader reader)
        {
            User user = new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = reader["Username"].ToString(),
                Email = reader["Email"].ToString(),
                RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
                Banned = Convert.ToBoolean(reader["Banned"]),
                IsAdmin = Convert.ToBoolean(reader["IsAdmin"]),
                ProfileImagePath = reader["ProfileImagePath"].ToString()
            };
            user.UpdatePassword(reader["Password"].ToString(), reader["Salt"].ToString());
            return user;
        }

        private bool ValidateUserPassword(User user, string rawPassword)
        {
            (string storedPassword, string storedSalt) = user.RetrieveCredentials();
            string hashedPassword = HashPassword.GenerateHashedPassword(rawPassword, storedSalt);
            return hashedPassword == storedPassword;
        }
    }
}
