using System;
using System.Data.SqlClient;
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

        public User AuthenticateUser(string username, string rawPassword)
        {
            User user = null;
            try
            {
                connection.Open();
                SqlCommand command = PrepareAuthenticateUserCommand(username);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user = MapReaderToUser(reader);
                    (string storedPassword, string storedSalt) = user.RetrieveCredentials();

                    string hashedPassword = HashPassword.GenerateHashedPassword(rawPassword, storedSalt);
                    Console.WriteLine($"Stored Password: {storedPassword}, Stored Salt: {storedSalt}");
                    Console.WriteLine($"Computed Hashed Password: {hashedPassword}");

                    if (hashedPassword == storedPassword)
                    {
                        return user;
                    }
                    else
                    {
                        // Debugging line
                        Console.WriteLine("Hashed password did not match stored password.");
                    }
                }
                else
                {
                    // Debugging line
                    Console.WriteLine("No user found with the provided username.");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlingService.HandleException(ex);
                throw;
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public bool Create(User user)
        {
            try
            {
                connection.Open();
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
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandlingService.HandleException(ex);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool Update(User user)
        {
            try
            {
                connection.Open();
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
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandlingService.HandleException(ex);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }


        public bool Delete(int id)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM [User] WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                ExceptionHandlingService.HandleException(ex);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public User GetUserFromId(int id)
        {
            return GetUserById(id);
        }

        public User GetUserFromUsername(string username)
        {
            User user = null;
            try
            {
                connection.Open();
                string query = "SELECT * FROM [User] WHERE Username = @username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user = MapReaderToUser(reader);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlingService.HandleException(ex);
            }
            finally
            {
                connection.Close();
            }
            return user;
        }

        private User GetUserById(int id)
        {
            User user = null;
            try
            {
                connection.Open();
                string query = "SELECT * FROM [User] WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user = MapReaderToUser(reader);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlingService.HandleException(ex);
                throw;
            }
            finally
            {
                connection.Close();
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
