using System;
using System.Data.SqlClient;
using AniX_DAL;
using Anix_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;

public class UserDAL : BaseDAL, IUserManagement
{
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
                string storedSalt = reader["Salt"].ToString();
                string storedPassword = reader["Password"].ToString();

                string hashedPassword = HashPassword.GenerateHashedPassword(rawPassword, storedSalt);

                if (hashedPassword == storedPassword)
                {
                    user = MapReaderToUser(reader);
                }
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"An error occurred: {ex.Message}");  // Debug
        }
        finally
        {
            connection.Close();
        }

        return user;
    }

    public bool Create(User user)
    {
        try
        {
            connection.Open();
            string query = "INSERT INTO [User] (Username, Password, Salt, Email, RegistrationDate, Banned, IsAdmin) VALUES (@username, @password, @salt, @email, @registrationDate, @banned, @isAdmin)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@salt", user.Salt);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@registrationDate", user.RegistrationDate);
            command.Parameters.AddWithValue("@banned", user.Banned);
            command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
            return command.ExecuteNonQuery() > 0;
        }
        finally
        {
            connection.Close();
        }
    }

    public User Read(int id)
    {
        try
        {
            connection.Open();
            string query = "SELECT * FROM [User] WHERE Id = @id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapReaderToUser(reader);
            }
            return null;
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
            string query = "UPDATE [User] SET Username = @username, Password = @password, Email = @email, Banned = @banned, IsAdmin = @isAdmin WHERE Id = @id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@banned", user.Banned);
            command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
            return command.ExecuteNonQuery() > 0;
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
        finally
        {
            connection.Close();
        }
    }

    public User GetUserFromUsername(string username)
    {
        try
        {
            connection.Open();
            string query = "SELECT * FROM [User] WHERE Username = @username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapReaderToUser(reader);
            }
            return null;
        }
        finally
        {
            connection.Close();
        }
    }

    public User GetUserFromId(int id)
    {
        return Read(id);
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
        return new User
        {
            Id = Convert.ToInt32(reader["Id"]),
            Username = reader["Username"].ToString(),
            Email = reader["Email"].ToString(),
            RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
            Banned = Convert.ToBoolean(reader["Banned"]),
            IsAdmin = Convert.ToBoolean(reader["IsAdmin"])
        };
    }
}
