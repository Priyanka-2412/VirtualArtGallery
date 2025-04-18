using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace VirtualArtGallery.Util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                string connStr = DBPropertyUtil.GetConnectionString("DefaultConnection");
                SqlConnection connection = new SqlConnection(connStr);
                connection.Open();
                return connection;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                throw;
            }
        }
    }
}