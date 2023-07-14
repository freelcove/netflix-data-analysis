using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

namespace Netflix_Analyzer
{
    public class DataManager
    {
        public static List<User> Users = new List<User>();
        public static List<Country> Countries = new List<Country>();
        public static List<Device> Devices = new List<Device>();
        public static List<Gender> Genders = new List<Gender>();
        public static List<Genre> Genres = new List<Genre>();
        public static List<Subscription_Type> Subscription_Types = new List<Subscription_Type>();
        private static List<string> Tables = new List<string>() { "Countries", "Devices", "Genders", "Genres", "Subscription_Types", "Users" };

        static DataManager()
        {
            Load();
        }
        //전체 테이블 불러오기
        public static void Load()
        {
            LoadCountries();
            LoadDevices();
            LoadGenders();
            LoadGenres();
            LoadSubscription();
            LoadUsers();
        }
        //선택 테이블 부르기
        public static void Load(string table)
        {
            switch (table)
            {
                case "Countries":
                    LoadCountries();
                    break;
                case "Devices":
                    LoadDevices();
                    break;
                case "Genders":
                    LoadGenders();
                    break;
                case "Genres":
                    LoadGenres();
                    break;
                case "Subscription_Types":
                    LoadSubscription();
                    break;
                case "Users":
                    LoadUsers();
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show(table);
                    break;
            }
        }

        //Users 테이블 데이터 불러오기
        private static void LoadUsers()
        {
            try
            {
                int count = 0;
                DBHelper.selectQuery("Users");
                Users.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    User user = new User();
                    int.TryParse(item["id"].ToString(), out int id);
                    int.TryParse(item["subscription_type"].ToString(), out int subscription);
                    int.TryParse(item["country"].ToString(), out int country);
                    int.TryParse(item["gender"].ToString(), out int gender);
                    int.TryParse(item["device"].ToString(), out int device);
                    int.TryParse(item["preferred_genre"].ToString(), out int genre);
                    int.TryParse(item["average_watch_time"].ToString(), out int average_watch_time);
                    string[] join_date = item["join_date"].ToString().Split();
                    string[] last_payment_date = item["last_payment_date"].ToString().Split();
                    string[] birth_date = item["birth_date"].ToString().Split();
                    user.id = id;
                    user.subcription_type = subscription;
                    user.join_date = DateTime.ParseExact(join_date[0], "yyyy-MM-dd", new CultureInfo("en-US"));
                    user.last_payment_date = DateTime.ParseExact(last_payment_date[0], "yyyy-MM-dd", new CultureInfo("en-US"));
                    user.country = country;
                    user.gender = gender;
                    user.device = device;
                    user.birth_date = DateTime.ParseExact(birth_date[0], "yyyy-MM-dd", new CultureInfo("en-US"));
                    user.preferred_genre = genre;
                    user.avrage_watch_time = average_watch_time;
                    count++;
                    Users.Add(user);
                    if (count >100 )
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Subscription_Types");
            }
        }

        private static void LoadSubscription()
        {
            try
            {
                DBHelper.selectQuery("Subscription_Types");
                Subscription_Types.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    Subscription_Type subscription = new Subscription_Type();
                    int.TryParse(item["id"].ToString(), out int id);
                    subscription.id = id;
                    subscription.name = item["name"].ToString();
                    Subscription_Types.Add(subscription);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Subscription_Types");
            }
        }

        private static void LoadGenres()
        {
            try
            {
                DBHelper.selectQuery("Genres");
                Genres.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    Genre genre = new Genre();
                    int.TryParse(item["id"].ToString(), out int id);
                    genre.id = id;
                    genre.name = item["name"].ToString();
                    Genres.Add(genre);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Genres");
            }
        }

        private static void LoadGenders()
        {
            try
            {
                DBHelper.selectQuery("Genders");
                Genders.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    Gender gender = new Gender();
                    int.TryParse(item["id"].ToString(), out int id);
                    gender.id = id;
                    gender.name = item["name"].ToString();
                    Genders.Add(gender);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Genders");
            }
        }

        private static void LoadDevices()
        {
            try
            {
                DBHelper.selectQuery("Devices");
                Devices.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    Device device = new Device();
                    int.TryParse(item["id"].ToString(), out int id);
                    device.id = id;
                    device.name = item["name"].ToString();
                    Devices.Add(device);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Devices");
            }
        }

        private static void LoadCountries()
        {
            try
            {
                DBHelper.selectQuery("Countries");
                Countries.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    Country country = new Country();
                    int.TryParse(item["id"].ToString(), out int id);
                    int.TryParse(item["population"].ToString(), out int population);
                    int.TryParse(item["gdp"].ToString(), out int gdp);
                    int.TryParse(item["gdp_per_capita"].ToString(), out int gdp_per_capita);
                    country.id = id;
                    country.name = item["name"].ToString();
                    country.region = item["region"].ToString();
                    country.population = population;
                    country.gdp = gdp;
                    country.gdp_per_capita = gdp_per_capita;
                    Countries.Add(country);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Countries");
            }
        }
        public static void printLog(string contents)
        {
            //ParkingCarManager.exe랑 같은 경로에
            //LogFolder라는 이름의 폴더가 없다면...
            DirectoryInfo di = new DirectoryInfo("LogFolder");
            if (di.Exists == false)
            {
                di.Create();//새로 만든다.
            }
            //@"LogFolder\ParkingHistory.txt"
            //"LogFolder\\ParkingHistory.txt"
            //true : appned 옵션을 true
            //즉 새로운 텍스트가 나오면 덮어쓰지 않고
            //뒤에다가 이어붙인다.
            using (StreamWriter w = new StreamWriter
                (@"LogFolder\Netflix_Analyzer.txt", true))
            {
                w.WriteLine(contents);
            }
        }
        //---------------------------- 이하 작업중 ----------------------------------------------

        //주차 출차용 Save
        public static void Save(string ps, string carNumber, string driverName, string phoneNumber, bool isRemove = false)
        {
            try
            {
                DBHelper.updateQuery
                    (ps, carNumber, driverName, phoneNumber, isRemove);
            }
            catch (Exception)
            {

            }
        }
        //주차 공간 추가 삭제용 Save
        public static bool Save(string command, string ps,
            out string contents)
        {
            DBHelper.selectQuery(ps);//해당 공간 유무 체크

            contents = "";
            if (command.Equals("insert"))
                return DBInsert(ps, ref contents);
            else
                return DBDelete(ps, ref contents);

        }
        private static bool DBInsert(string ps, ref string contents)
        {
            if (DBHelper.dt.Rows.Count == 0)
            {
                DBHelper.insertQuery(ps);
                contents = $"주차공간 {ps}이/가 추가됨";
                return true;
            }
            else
            {
                contents = $"해당 공간 이미 있음";
                return false;
            }
        }
        private static bool DBDelete(string ps, ref string contents)
        {
            if (DBHelper.dt.Rows.Count != 0)
            {
                DBHelper.deleteQuery(ps);
                contents = $"주차공간 {ps}이/가 삭제됨";
                return true;
            }
            else
            {
                contents = $"해당 공간 없음";
                return false;
            }
        }

    }
}
