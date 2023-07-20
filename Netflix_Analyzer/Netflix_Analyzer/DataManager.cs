using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace Netflix_Analyzer
{
    public class DataManager
    {
        public static List<User> Users = new List<User>();
        public static List<Country> Countries = new List<Country>();
        public static List<Country> CountriesSort = new List<Country>();
        public static List<Device> Devices = new List<Device>();
        public static List<Device> DevicesSort = new List<Device>();
        public static List<Gender> Genders = new List<Gender>();
        public static List<Genre> Genres = new List<Genre>();
        public static List<Genre> GenresSort = new List<Genre>();
        public static List<Subscription_Type> Subscription_Types = new List<Subscription_Type>();
        public static DataTable UsersDT = new DataTable();
        public static DataTable CountriesDT = new DataTable();
        public static DataTable DevicesDT = new DataTable();
        public static DataTable GendersDT = new DataTable();
        public static DataTable GenresDT = new DataTable();
        public static DataTable Subscription_TypesDT = new DataTable();
        public static Dictionary<int,Country> CountriesDIC = new Dictionary<int, Country>();
        public static Dictionary<int,Device> DevicesDIC = new Dictionary<int, Device>();
        public static Dictionary<int,Gender> GendersDIC = new Dictionary<int, Gender>();
        public static Dictionary<int,Genre> GenresDIC = new Dictionary<int, Genre>();
        public static Dictionary<int,Subscription_Type> Subscription_TypesDIC = new Dictionary<int, Subscription_Type>();

        public static List<string> Tables = new List<string>() { "Countries", "Devices", "Genders", "Genres", "Subscription_Types", "Users" };
        public static List<int> ColumnCount = new List<int>() { 0, 0, 0, 0, 0, 0 };

        static DataManager()
        {
            LoadDT();
            Load();
            ListSort();
        }

        private static void ListSort() 
        {
            foreach(Country item in Countries )
            {
                CountriesSort.Add(item);
            }
            CountriesSort.Sort();
            foreach(Device item in Devices)
            {
                DevicesSort.Add(item);
            }
            DevicesSort.Sort();
            foreach(Genre item in Genres)
            {
                GenresSort.Add(item);
            }
            GenresSort.Sort();
        }

        //전체 테이블 불러오기(DT)
        public static void LoadDT()
        {
            for (int i = 0; i<Tables.Count; i++)
            {
                ColumnCount[i] = DBHelper.countColumn(Tables[i]);
            }
            LoadCountriesDT();
            LoadDevicesDT();
            LoadGendersDT();
            LoadGenresDT();
            LoadSubscriptionDT();
            LoadUsersDT();
        }
        //전체 테이블 불러오기(List)
        public static void Load()
        {
            LoadCountries();
            LoadDevices();
            LoadGenders();
            LoadGenres();
            LoadSubscription();
            //LoadUsers();
        }

        //선택 테이블 부르기(DT)
        public static void LoadDT(string table, int selectId = -1, string filter = "=")
        {
            switch (table)
            {
                case "Countries":
                    LoadCountriesDT(selectId, filter);
                    break;
                case "Devices":
                    LoadDevicesDT(selectId, filter);
                    break;
                case "Genders":
                    LoadGendersDT(selectId, filter);
                    break;
                case "Genres":
                    LoadGenresDT(selectId, filter);
                    break;
                case "Subscription_Types":
                    LoadSubscriptionDT(selectId, filter);
                    break;
                case "Users":
                    LoadUsersDT(selectId, filter);
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show(table);
                    break;
            }
        }
        //선택 테이블 부르기(List)
        public static void Load(string table, int selectId = -1, string filter = "=")
        {
            switch (table)
            {
                case "Countries":
                    LoadCountries(selectId, filter);
                    break;
                case "Devices":
                    LoadDevices(selectId, filter);
                    break;
                case "Genders":
                    LoadGenders(selectId, filter);
                    break;
                case "Genres":
                    LoadGenres(selectId, filter);
                    break;
                case "Subscription_Types":
                    LoadSubscription(selectId, filter);
                    break;
                case "Users":
                    LoadUsers(selectId, filter);
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show(table);
                    break;
            }
        }


        //Users 테이블 데이터 불러오기(DT)
        private static void LoadUsersDT(int selectId = -1, string filter = "=")
        {
            try
            {
                UsersDT.Clear();
                if (selectId != -1)
                {
                    UsersDT = DBHelper.selectQueryDT("Users", selectId, filter);
                }
                else
                {
                    UsersDT = DBHelper.selectQueryDT("Users");
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_UsersDT");
            }
        }

        //Subscription_Types 테이블 데이터 불러오기(DT)
        private static void LoadSubscriptionDT(int selectId = -1, string filter = "=")
        {
            try
            {
                Subscription_TypesDT.Clear();
                if (selectId != -1)
                {
                    Subscription_TypesDT = DBHelper.selectQueryDT("Subscription_Types", selectId, filter);
                }
                else
                {
                    Subscription_TypesDT = DBHelper.selectQueryDT("Subscription_Types");
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Subscription_TypesDT");
            }
        }

        //Genres 테이블 데이터 불러오기(DT)
        private static void LoadGenresDT(int selectId = -1, string filter = "=")
        {
            try
            {
                GenresDT.Clear();
                if (selectId != -1)
                {
                    GenresDT = DBHelper.selectQueryDT("Genres", selectId, filter);
                }
                else
                {
                    GenresDT = DBHelper.selectQueryDT("Genres");
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_GenresDT");
            }
        }

        //Genders 테이블 데이터 불러오기(DT)
        private static void LoadGendersDT(int selectId = -1, string filter = "=")
        {
            try
            {
                GendersDT.Clear();
                if (selectId != -1)
                {
                    GendersDT = DBHelper.selectQueryDT("Genders", selectId, filter);
                }
                else
                {
                    GendersDT = DBHelper.selectQueryDT("Genders");
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_GendersDT");
            }
        }

        //Devices 테이블 데이터 불러오기(DT)
        private static void LoadDevicesDT(int selectId = -1, string filter = "=")
        {
            try
            {
                DevicesDT.Clear();
                if (selectId != -1)
                {
                    DevicesDT = DBHelper.selectQueryDT("Devices", selectId, filter);
                }
                else
                {
                    DevicesDT = DBHelper.selectQueryDT("Devices");
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_DevicesDT");
            }
        }

        //Countries 테이블 데이터 불러오기(DT)
        private static void LoadCountriesDT(int selectId = -1, string filter = "=")
        {
            try
            {
                CountriesDT.Clear();
                if (selectId != -1)
                {
                    CountriesDT = DBHelper.selectQueryDT("Countries", selectId, filter);
                }
                else
                {
                    CountriesDT = DBHelper.selectQueryDT("Countries");
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_CountriesDT");
            }
        }

        //Users 테이블 데이터 불러오기(List)
        private static void LoadUsers(int selectId = -1, string filter = "=")
        {
            try
            {
                int count = 0;
                if (selectId != -1)
                {
                    DBHelper.selectQuery("Users", selectId, filter);
                }
                else
                {
                    DBHelper.selectQuery("Users");
                }
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
                    string join_date = Convert.ToDateTime(item["join_date"]).ToString("yyyy-MM-dd");
                    string last_payment_date = Convert.ToDateTime(item["last_payment_date"]).ToString("yyyy-MM-dd");
                    string birth_date = Convert.ToDateTime(item["birth_date"]).ToString("yyyy-MM-dd");
                    /*
                    DateTime date1 = DateTime.Parse(item["join_date"].ToString());
                    DateTime date2 = DateTime.Parse(item["last_payment_date"].ToString());
                    DateTime date3 = DateTime.Parse(item["birth_date"].ToString());

                    string join_date = date1.ToString("yyyy-MM-dd");
                    string last_payment_date = date2.ToString("yyyy-MM-dd");
                    string birth_date = date3.ToString("yyyy-MM-dd");
                    */
                    Console.WriteLine(join_date);
                    user.id = id;
                    user.join_date = DateTime.ParseExact(join_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    user.last_payment_date = DateTime.ParseExact(last_payment_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    user.birth_date = DateTime.ParseExact(birth_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    //Console.WriteLine(user.join_date);
                    user.subcription_type = subscription;
                    user.country = country;
                    user.gender = gender;
                    user.device = device;
                    user.preferred_genre = genre;
                    user.average_watch_time = average_watch_time;
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
                printLog(e.StackTrace + "load_Users");
            }
        }

        private static void LoadSubscription(int selectId = -1, string filter = "=")
        {
            try
            {
                if (selectId != -1)
                {
                    DBHelper.selectQuery("Subscription_Types", selectId, filter);
                }
                else
                {
                    DBHelper.selectQuery("Subscription_Types");
                }

                Subscription_Types.Clear();
                Subscription_TypesDIC.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    Subscription_Type subscription = new Subscription_Type();
                    int.TryParse(item["id"].ToString(), out int id);
                    subscription.id = id;
                    subscription.name = item["name"].ToString();
                    Subscription_Types.Add(subscription);
                    Subscription_TypesDIC.Add(subscription.id, subscription);

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Subscription_Types");
            }
        }

        private static void LoadGenres(int selectId = -1, string filter = "=")
        {
            try
            {
                if (selectId != -1)
                {
                    DBHelper.selectQuery("Genres", selectId, filter);
                }
                else
                {
                    DBHelper.selectQuery("Genres");
                }

                Genres.Clear();
                GenresDIC.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    Genre genre = new Genre();
                    int.TryParse(item["id"].ToString(), out int id);
                    genre.id = id;
                    genre.name = item["name"].ToString();
                    Genres.Add(genre);
                    GenresDIC.Add(genre.id, genre);

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Genres");
            }
        }

        private static void LoadGenders(int selectId = -1, string filter = "=")
        {
            try
            {
                if (selectId != -1)
                {
                    DBHelper.selectQuery("Genders", selectId, filter);
                }
                else
                {
                    DBHelper.selectQuery("Genders");
                }

                Genders.Clear();
                GendersDIC.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    Gender gender = new Gender();
                    int.TryParse(item["id"].ToString(), out int id);
                    gender.id = id;
                    gender.name = item["name"].ToString();
                    Genders.Add(gender);
                    GendersDIC.Add(gender.id, gender);

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Genders");
            }
        }

        private static void LoadDevices(int selectId = -1, string filter = "=")
        {
            try
            {
                if (selectId != -1)
                {
                    DBHelper.selectQuery("Devices", selectId, filter);
                }
                else
                {
                    DBHelper.selectQuery("Devices");
                }

                Devices.Clear();
                DevicesDIC.Clear();
                foreach (DataRow item in DBHelper.dt.Rows)
                {
                    Device device = new Device();
                    int.TryParse(item["id"].ToString(), out int id);
                    device.id = id;
                    device.name = item["name"].ToString();
                    Devices.Add(device);
                    DevicesDIC.Add(device.id, device);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                printLog(e.StackTrace + "load_Devices");
            }
        }

        private static void LoadCountries(int selectId = -1, string filter = "=")
        {
            try
            {
                if (selectId != -1)
                {
                    DBHelper.selectQuery("Countries", selectId, filter);
                }
                else
                {
                    DBHelper.selectQuery("Countries");
                }

                Countries.Clear();
                CountriesDIC.Clear();
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
                    CountriesDIC.Add(country.id, country);
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

        public static bool insertDB(string table , Dictionary<string, object> data , out string contents)
        {
            int.TryParse(data["id"].ToString(), out int id);

            DBHelper.selectQuery(table, id);//해당 공간 유무 체크
            contents = "";
            return insertQuery(table, data, ref contents);

        }

        public static bool insertQuery(string table,  Dictionary<string, object> data , ref string contents)
        {
            if (DBHelper.dt.Rows.Count == 0)
            {
                DBHelper.insertQuery(table, data);
                contents = $"{table}에 추가되었습니다.";
                return true;
            }
            else
            {
                contents = $"{table}에 이미 있는 아이디입니다.";
                return false;
            }
        }

        public static bool deleteDB(string table, int id, out string contents)
        {

            DBHelper.selectQuery(table, id);//해당 공간 유무 체크
            contents = "";
            return deleteQuery(table, id, ref contents);

        }

        public static bool deleteQuery(string table, int id, ref string contents)
        {
            if (DBHelper.dt.Rows.Count != 0)
            {
                DBHelper.deleteQuery(table, id);
                contents = $"{table}에 id = {id}인 데이터가 삭제되었습니다.";
                return true;
            }
            else
            {
                contents = $"{table}에 id = {id}인 데이터는 없는 데이터입니다.";
                return false;
            }
        }

        public static void updateDB(string table, Dictionary<string, object> data, out string contents)
        {
            DBHelper.updateQuery(table, data);
            contents = $"{table}에 id = {data["id"]}의 데이터가 수정되었습니다.";
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
