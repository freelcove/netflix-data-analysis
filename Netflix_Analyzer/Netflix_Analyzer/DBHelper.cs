using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Netflix_Analyzer
{

    public class DBHelper
    {
        private static SqlConnection conn = new SqlConnection();

        public static SqlDataAdapter da;
        public static DataSet ds;
        public static DataTable dt;

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


        //string ps="-1"(기본값(디폴트값)은 -1)
        //selectQuery(string ps="-1")
        //selectQuery(); ps = -1
        //selectQuery("aaa"); ps = aaa
        public static void selectQuery(string table, string id = "-1")
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (id.Equals("-1"))
                    cmd.CommandText = $"select * from {table}";
                else
                    cmd.CommandText = $"select * from {table} where id='{id}'";
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


        //Countries, User 제외 다른 테이블 업데이트용
        public static void updateQuery(string table, int id, int name)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                string sqlcommand = "";
                sqlcommand = "update @p1 set name = @p2 where id = @p3";
                cmd.Parameters.AddWithValue("@p1", table);
                cmd.Parameters.AddWithValue("@p2", name);
                cmd.Parameters.AddWithValue("@p3", id);
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

        //Countries 테이블 업데이트용
        public static void updateQuery(string table, int id, string name, string region, int gdp, int gdp_per_capita)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                string sqlcommand = "";
                sqlcommand = "update @p1 set name = @p2, region = @p3, gdp = @p4, gdp_per_capita = @p5 where id = @p6";
                cmd.Parameters.AddWithValue("@p1", table);
                cmd.Parameters.AddWithValue("@p2", name);
                cmd.Parameters.AddWithValue("@p3", region);
                cmd.Parameters.AddWithValue("@p4", gdp);
                cmd.Parameters.AddWithValue("@p5", gdp_per_capita);
                cmd.Parameters.AddWithValue("@p6", id);
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

        //User 테이블 업데이트용
        public static void updateQuery(string table, int id, int subscription_type, DateTime join_date, DateTime last_payment_date, int country, int gender, int device, DateTime birth_date, int preferred_genre, int average_watch_time)
        {
            try
            {
                ConnectDB();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                string sqlcommand = "";
                sqlcommand = "update @p1 set subscription_type = @p2, join_date = @p3, last_payment_date = @p4, country = @p5, gender = @p6, device = @p7, birth_date = @p8, preferred_genre = @p9, average_watch_time = @p10 where id = @p11";
                cmd.Parameters.AddWithValue("@p1", table);
                cmd.Parameters.AddWithValue("@p2", subscription_type);
                cmd.Parameters.AddWithValue("@p3", join_date);
                cmd.Parameters.AddWithValue("@p4", last_payment_date);
                cmd.Parameters.AddWithValue("@p5", country);
                cmd.Parameters.AddWithValue("@p6", gender);
                cmd.Parameters.AddWithValue("@p7", device);
                cmd.Parameters.AddWithValue("@p8", birth_date);
                cmd.Parameters.AddWithValue("@p9", preferred_genre);
                cmd.Parameters.AddWithValue("@p10", average_watch_time);
                cmd.Parameters.AddWithValue("@p11", id);
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
