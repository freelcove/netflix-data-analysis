using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Xml.Linq;

namespace Netflix_Analyzer
{




    public class DBHelper
    {
        private static SqlConnection conn = new SqlConnection();

        public static SqlDataAdapter da;
        public static DataSet ds;
        public static DataTable dt;
        public static SqlDataAdapter adapter;

        public static void ConnectDB()
        {//접속해주는 함수
            try
            {
                //conn.ConnectionString = $"Data Source=192.168.0.111,1433; Initial Catalog = MYDB; Persist Security Info = True; User ID=user1; Password=1234";
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

        public static int countColumn(string table)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"select count(*) as ColCnt from dbo.syscolumns where id=object_id('{table}')";
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, table);
                dt = ds.Tables[0];
                int.TryParse(dt.Rows[0]["ColCnt"].ToString(), out int num);
                Console.WriteLine(num);
                return num;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Column Count 오류");
                DataManager.printLog("Column Count," + ex.Message + "," + ex.StackTrace);
                return 0;
            }
            finally { conn.Close(); }
        }


        public static DataTable selectQueryDT(string table, int id = -1, string filter = "=")
        {
            try
            {
                dt = new DataTable();
                string connect = string.Format("Data Source={0}; Initial Catalog = {1}; Persist Security Info = True; User ID=user1;Password=1234", "192.168.0.104,1433", "Csharp_Team");
                conn = new SqlConnection(connect);
                conn.Open();
                if (id == -1)
                {
                    adapter = new SqlDataAdapter($"SELECT * FROM {table}", conn);
                }
                else
                {
                    adapter = new SqlDataAdapter($"SELECT * FROM {table} where id {filter} {id}", conn);
                }
                adapter.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("select 오류");
                DataManager.printLog("select," + ex.Message + "," + ex.StackTrace);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable AnalyzerDT(string query)
        {
            try
            {
                dt = new DataTable();
                string connect = string.Format("Data Source={0}; Initial Catalog = {1}; Persist Security Info = True; User ID=user1;Password=1234", "192.168.0.104,1433", "Csharp_Team");
                conn = new SqlConnection(connect);
                conn.Open();

                adapter = new SqlDataAdapter(query, conn);

                adapter.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("AnalyzerDT 오류");
                DataManager.printLog("AnalyzerDT," + ex.Message + "," + ex.StackTrace);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void insertQuery(string table, Dictionary<string, object> data)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                string sqlcommand = "";
                if (table.Equals("Countries"))
                {
                    sqlcommand = "insert into countries values(@id, @name, @region, @population, @gdp, @gdp_per_capita)";
                    cmd.Parameters.AddWithValue("@id", data["id"]);
                    cmd.Parameters.AddWithValue("@name", data["name"]);
                    cmd.Parameters.AddWithValue("@region", data["region"]);
                    cmd.Parameters.AddWithValue("@population", data["population"]);
                    cmd.Parameters.AddWithValue("@gdp", data["gdp"]);
                    cmd.Parameters.AddWithValue("@gdp_per_capita", data["gdp_per_capita"]);

                }
                else if (table.Equals("Users"))
                {
                    sqlcommand = "insert into users(subscription_type, join_date, last_payment_date, country, gender, device, birth_date, preferred_genre, average_watch_time) values(@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)";
                    cmd.Parameters.AddWithValue("@p2", data["subscription_type"]);
                    cmd.Parameters.AddWithValue("@p3", data["join_date"]);
                    cmd.Parameters.AddWithValue("@p4", data["last_payment_date"]);
                    cmd.Parameters.AddWithValue("@p5", data["country"]);
                    cmd.Parameters.AddWithValue("@p6", data["gender"]);
                    cmd.Parameters.AddWithValue("@p7", data["device"]);
                    cmd.Parameters.AddWithValue("@p8", data["birth_date"]);
                    cmd.Parameters.AddWithValue("@p9", data["preferred_genre"]);
                    cmd.Parameters.AddWithValue("@p10", data["average_watch_time"]);

                }
                else
                {
                    if (table.Equals("Devices"))
                    {
                        sqlcommand = "insert into devices values(@id,@name)";
                    }
                    else if (table.Equals("Genders"))
                    {
                        sqlcommand = "insert into genders values(@id,@name)";
                    }
                    else if (table.Equals("Genres"))
                    {
                        sqlcommand = "insert into genres values(@id,@name)";
                    }
                    else if (table.Equals("Subscription_Types"))
                    {
                        sqlcommand = "insert into subscription_types values(@id,@name)";
                    }
                    cmd.Parameters.AddWithValue("@id", data["id"]);
                    cmd.Parameters.AddWithValue("@name", data["name"]);
                }
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("insert 오류");
                DataManager.printLog("insert," + ex.Message + "," + ex.StackTrace);

            }
            finally
            {
                conn.Close();
            }
        }

        public static void updateQuery(string table, Dictionary<string, object> data)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                string sqlcommand = "";
                if (table.Equals("Countries"))
                {
                    sqlcommand = "update countires set name = @p2, region = @p3, population = @p4, gdp = @p5, gdp_per_capita = @p6 where id = @p7";
                    cmd.Parameters.AddWithValue("@p2", data["name"]);
                    cmd.Parameters.AddWithValue("@p3", data["region"]);
                    cmd.Parameters.AddWithValue("@p4", data["population"]);
                    cmd.Parameters.AddWithValue("@p5", data["gdp"]);
                    cmd.Parameters.AddWithValue("@p6", data["gdp_per_capita"]);
                    cmd.Parameters.AddWithValue("@p7", data["id"]);
                }
                else if (table.Equals("Users"))
                {
                    sqlcommand = "update users set subscription_type = @p2, join_date = @p3, last_payment_date = @p4, country = @p5, gender = @p6, device = @p7, birth_date = @p8, preferred_genre = @p9, average_watch_time = @p10 where id = @p11";
                    cmd.Parameters.AddWithValue("@p2", data["subscription_type"]);
                    cmd.Parameters.AddWithValue("@p3", data["join_date"]);
                    cmd.Parameters.AddWithValue("@p4", data["last_payment_date"]);
                    cmd.Parameters.AddWithValue("@p5", data["country"]);
                    cmd.Parameters.AddWithValue("@p6", data["gender"]);
                    cmd.Parameters.AddWithValue("@p7", data["device"]);
                    cmd.Parameters.AddWithValue("@p8", data["birth_date"]);
                    cmd.Parameters.AddWithValue("@p9", data["preferred_genre"]);
                    cmd.Parameters.AddWithValue("@p10", data["average_watch_time"]);
                    cmd.Parameters.AddWithValue("@p11", data["id"]);
                }
                else
                {
                    if (table.Equals("Devices"))
                    {
                        sqlcommand = "update devices set name = @p2 where id = @p3";
                    }
                    else if (table.Equals("Genders"))
                    {
                        sqlcommand = "update genders set name = @p2 where id = @p3";
                    }
                    else if (table.Equals("Genres"))
                    {
                        sqlcommand = "update genres set name = @p2 where id = @p3";
                    }
                    else if (table.Equals("Subscription_Types"))
                    {
                        sqlcommand = "update subscription_types set name = @p2 where id = @p3";
                    }
                    cmd.Parameters.AddWithValue("@p2", data["name"]);
                    cmd.Parameters.AddWithValue("@p3", data["id"]);
                }
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("update" + ex.Message);
                DataManager.printLog("update," + ex.Message + "," + ex.StackTrace);

            }
            finally
            {
                conn.Close();
            }
        }
        public static void deleteQuery(string table, int id)
        {
            try
            {
                ConnectDB();
                string sqlcommand = "";
                if (table.Equals("Devices"))
                {
                    sqlcommand = "delete from devices where id = @p2";
                }
                else if (table.Equals("Genders"))
                {
                    sqlcommand = "delete from genders where id = @p2";
                }
                else if (table.Equals("Genres"))
                {
                    sqlcommand = "delete from  genres where id = @p2";
                }
                else if (table.Equals("Subscription_Types"))
                {
                    sqlcommand = "delete from subscription_types where id = @p2";
                }
                else if (table.Equals("Countries"))
                {
                    sqlcommand = "delete from countries where id = @p2";
                }
                else if (table.Equals("Users"))
                {
                    sqlcommand = "delete from users where id = @p2";
                }
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p2", id);
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("delete" + ex.Message);
                DataManager.printLog("delete," + ex.Message + "," + ex.StackTrace);
            }
            finally 
            {
                conn.Close(); 
            }
        }


        //string ps="-1"(기본값(디폴트값)은 -1)
        //selectQuery(string ps="-1")
        //selectQuery(); ps = -1
        //selectQuery("aaa"); ps = aaa
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
                    cmd.CommandText = $"select * from {table} where id{filter}{id}";
                Console.WriteLine(cmd.CommandText);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, table);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("select 오류");
                DataManager.printLog("select," + ex.Message + "," + ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        //---------------------------- 이하 작업중 ----------------------------------------------
        public static void updateQuery(string parkingSpot, string carNumber, string driverName, string phoneNumber, bool isRemove)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                string sqlcommand = "";
                if (isRemove) //출차
                {
                    //sql injection 방지 코드 작성해보기
                    sqlcommand =
                    "update parkingManager set carNumber=''," +
                    "driverName='',phoneNumber=''," +
                    "parkingTime=null where " +
                    "parkingSpot=@p1";
                    cmd.Parameters.AddWithValue("@p1",
                        parkingSpot);
                }
                else //주차
                {//sql injection 방지 코드 작성해보기
                    sqlcommand =
                    "update parkingManager set carNumber=@p1," +
                    "DriverName=@p2,phoneNumber=@p3," +
                    "parkingTime=@p4 where " +
                    "parkingSpot=@p5";
                    cmd.Parameters.AddWithValue("@p1",
                        carNumber);
                    cmd.Parameters.AddWithValue("@p2",
                        driverName);
                    cmd.Parameters.AddWithValue("@p3",
                        phoneNumber);
                    cmd.Parameters.AddWithValue("@p4",
                        DateTime.Now.ToString
                        ("yyyy-MM-dd HH:mm:ss.fff"));
                    cmd.Parameters.AddWithValue("@p5",
                        parkingSpot);
                }
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("update" + ex.Message);
                DataManager.printLog("update," + ex.Message + "," + ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
        private static void executeQuery(string ps,
            string command)
        {
            string sqlcommand = "";
            if (command.Equals("insert"))
                sqlcommand = "insert into parkingManager(parkingSpot) values (@p1)";
            else
                sqlcommand = "delete from parkingManager where parkingSpot = @p1";

            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@p1", ps);
                cmd.CommandText = sqlcommand;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(command);
                DataManager.printLog(command + "," + ex.Message + "," + ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }
        public static void deleteQuery(string ps)
        {
            executeQuery(ps, "delete");
        }
        public static void insertQuery(string ps)
        {
            executeQuery(ps, "insert");
        }

    }
}
