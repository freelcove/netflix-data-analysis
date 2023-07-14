using System.Data;
using System.Data.SqlClient;

namespace Netflix_Analyzer
{
    public static class DBManager
    {
        private static string connectionString = "Data Source=192.168.0.104,1433; Initial Catalog = Csharp_Team; Persist Security Info = True; User ID=user1;Password=1234";

        public static DataTable GetData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Users", connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
