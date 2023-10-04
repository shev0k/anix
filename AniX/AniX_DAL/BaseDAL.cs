using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AniX_DAL
{
    public abstract class BaseDAL
    {
        protected SqlConnection connection;

        public BaseDAL()
        {
            string connectionString = "Server=mssqlstud.fhict.local;Database=dbi499309_anixdb;User Id=dbi499309_anixdb;Password=rewindtime;";
            connection = new SqlConnection(connectionString);
        }
    }
}
