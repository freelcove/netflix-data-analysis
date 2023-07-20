using System;
using System.Data;
using System.Data.SqlClient;

namespace chart_test
{
    public static class DBHelper
    {
        private static SqlConnection conn = new SqlConnection();
        public static SqlDataAdapter da;
        public static DataSet ds;
        public static DataTable dt;

        public static void ConnectDB()
        {
            try
            {
                string connect = string.Format("Data Source={0}; Initial Catalog = {1}; Persist Security Info = True; User ID=user1;Password=1234", "192.168.0.104,1433", "Csharp_Team");
                conn = new SqlConnection(connect);
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("connect Fail");
            }
        }

        public static void selectQuery(string table, int id = -1, string filter = "=")
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (id == -1)
                    cmd.CommandText = $"select * from {table}";
                else
                    cmd.CommandText = $"select * from {table} where id{filter}'{id}'";
                Console.WriteLine(cmd.CommandText);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, table);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("select 오류");
            }
            finally
            {
                conn.Close();
            }
        }

        public static void selectQueryWithGdpPerCapita()
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                // Select the top 10 countries by GDP per capita, excluding China and Russia, and group by subscription type
                cmd.CommandText = @"
        WITH subscription_counts AS (
            SELECT
                c.name AS country,
                u.subscription_type,
                COUNT(*) AS count
            FROM users u
            JOIN countries c ON u.country = c.id
            WHERE c.name NOT IN ('China', 'Russia')
            GROUP BY c.name, u.subscription_type
        )
        SELECT *
        FROM subscription_counts
        WHERE country IN (
            SELECT TOP 10 country
            FROM subscription_counts
            GROUP BY country
            ORDER BY SUM(count) DESC
        )
        ORDER BY count DESC
    ";

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "users");
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("select 오류");
            }
            finally
            {
                conn.Close();
            }
        }



    }
}
