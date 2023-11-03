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

        public UserDAL(IAzureBlobService blobService, IConfiguration configuration) : base(configuration)
        {
            _blobService = blobService;
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
                    (string storedPassword, string storedSalt) = user.RetrieveCredentials();

                    string hashedPassword = HashPassword.GenerateHashedPassword(rawPassword, storedSalt);

                    if (hashedPassword == storedPassword)
                    {
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

            return null;
        }

        public async Task<bool> CreateAsync(User user, Stream profileImageStream = null, string contentType = null)
        {
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
                    profileImagePath = await _blobService.UploadImageAsync(profileImageStream, user.Id.ToString(), contentType);
                }

                string updateQuery = "UPDATE [User] SET ProfileImagePath = @profileImagePath WHERE Id = @id";
                SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction);
                updateCommand.Parameters.AddWithValue("@profileImagePath", profileImagePath);
                updateCommand.Parameters.AddWithValue("@id", userId);

                await updateCommand.ExecuteNonQueryAsync();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                return false;
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
                await connection.CloseAsync();
            }
        }

        public async Task<bool> UpdateAsync(User user, bool updateProfileImage = true)
        {
            try
            {
                await connection.OpenAsync();
                Console.WriteLine("Connection opened.");

                // Start constructing the SQL update command
                var queryBuilder = new StringBuilder(@"
            UPDATE [User] SET 
                Username = @username, 
                Password = @password, 
                Salt = @salt, 
                Email = @email, 
                Banned = @banned, 
                IsAdmin = @isAdmin");

                // Conditionally add the ProfileImagePath to the update command
                if (updateProfileImage)
                {
                    queryBuilder.Append(", ProfileImagePath = @profileImagePath");
                }

                // Finish the SQL command
                queryBuilder.Append(" WHERE Id = @id");
                string query = queryBuilder.ToString();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Use the RetrieveCredentials method to get the password and salt
                    (string password, string salt) = user.RetrieveCredentials();

                    // Add parameters with explicit types
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = user.Id;
                    command.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar)).Value = user.Username;
                    command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar)).Value = password;
                    command.Parameters.Add(new SqlParameter("@salt", SqlDbType.NVarChar)).Value = salt;
                    command.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar)).Value = user.Email;
                    command.Parameters.Add(new SqlParameter("@banned", SqlDbType.Bit)).Value = user.Banned;
                    command.Parameters.Add(new SqlParameter("@isAdmin", SqlDbType.Bit)).Value = user.IsAdmin;

                    // Conditionally add the profile image path parameter
                    if (updateProfileImage)
                    {
                        command.Parameters.Add(new SqlParameter("@profileImagePath", SqlDbType.NVarChar)).Value = (object)user.ProfileImagePath ?? DBNull.Value;
                    }

                    // Log the final state of parameters
                    foreach (SqlParameter param in command.Parameters)
                    {
                        Console.WriteLine($"{param.ParameterName}: {param.Value}");
                    }

                    // Execute the command
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"Update command executed, affected rows: {rowsAffected}");

                    // Check if at least one row was affected
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred:");
                Console.WriteLine(ex.ToString());
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
                Console.WriteLine("Connection closed and method exiting.");
            }
        }






        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await connection.OpenAsync();
                string query = "DELETE FROM [User] WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                return (await command.ExecuteNonQueryAsync()) > 0;
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task<User> GetUserFromIdAsync(int id)
        {
            return await GetUserByIdAsync(id);
        }

        public async Task<User> GetUserFromUsernameAsync(string username)
        {
            User user = null;
            try
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM [User] WHERE Username = @username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user = MapReaderToUser(reader);
                }
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
            }
            finally
            {
                await connection.CloseAsync();
            }
            return user;
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
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

            return user;
        }

        public async Task<List<User>> GetUsersInBatchAsync(int startIndex, int batchSize)
        {
            List<User> users = new List<User>();

            try
            {
                await connection.OpenAsync();

                string query = $"SELECT * FROM [User] ORDER BY Id OFFSET {startIndex} ROWS FETCH NEXT {batchSize} ROWS ONLY";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(MapReaderToUser(reader));
                }
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

            return users;
        }

        public async Task<List<User>> FetchFilteredAndSearchedUsersAsync(string filter, string searchTerm)
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM [dbo].[User] WHERE 1=1";

            if (!string.IsNullOrEmpty(filter))
            {
                if (filter == "Admin")
                    query += " AND IsAdmin = 1";
                else if (filter == "User")
                    query += " AND IsAdmin = 0";
                else if (filter == "Banned")
                    query += " AND Banned = 1";
                else if (filter == "Not Banned")
                    query += " AND Banned = 0";
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += $" AND (Username LIKE '%{searchTerm}%' OR Email LIKE '%{searchTerm}%')";
            }

            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        users.Add(MapReaderToUser(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                await ExceptionHandlingService.HandleExceptionAsync(ex);
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
    }
}
