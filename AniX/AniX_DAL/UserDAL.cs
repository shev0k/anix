using System;
using System.Data.SqlClient;
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
        public UserDAL(IConfiguration configuration) : base(configuration)
        {
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

        public async Task<bool> CreateAsync(User user)
        {
            try
            {
                await connection.OpenAsync();
                string query = "INSERT INTO [User] (Username, Password, Salt, Email, RegistrationDate, Banned, IsAdmin) VALUES (@username, @password, @salt, @email, @registrationDate, @banned, @isAdmin)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", user.Username);
                (string Password, string Salt) = user.RetrieveCredentials();
                command.Parameters.AddWithValue("@password", Password);
                command.Parameters.AddWithValue("@salt", Salt);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@registrationDate", user.RegistrationDate);
                command.Parameters.AddWithValue("@banned", user.Banned);
                command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
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

        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                await connection.OpenAsync();
                string query = "UPDATE [User] SET Username = @username, Password = @password, Salt = @salt, Email = @email, Banned = @banned, IsAdmin = @isAdmin WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@username", user.Username);
                (string Password, string Salt) = user.RetrieveCredentials();
                command.Parameters.AddWithValue("@password", Password);
                command.Parameters.AddWithValue("@salt", Salt);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@banned", user.Banned);
                command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
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
                IsAdmin = Convert.ToBoolean(reader["IsAdmin"])
            };
            user.UpdatePassword(reader["Password"].ToString(), reader["Salt"].ToString());
            return user;
        }
    }
}
