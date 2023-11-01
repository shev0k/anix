using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace AniX_DAL
{
    public abstract class BaseDAL : IDisposable
    {
        protected SqlConnection connection;

        public BaseDAL(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}
