using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace HackathonGlass_2019.Controllers
{
    public class DataBaseHandler
    {
        private const string ConnectionString = "Server= localhost; Database= Hackathon; User Id=sa;Password=Qwe12345";

        public DataTable RunQuery(string query)
        {
            var dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = connection.CreateCommand())
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                connection.Open();
                var table = sda.Fill(dt);
                connection.Close();
            }
            return dt;
        }
    }
}