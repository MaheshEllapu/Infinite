using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Mini_Project_RRS
{
    public class Connection
    {
        // Your provided connection string
        private static readonly string connectionString = "Data Source=ICS-LT-2R6BJ84\\SQLEXPRESS; Initial Catalog=Mini_Project_RRS; user id=sa; password=MaheshEllapu@123;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}

