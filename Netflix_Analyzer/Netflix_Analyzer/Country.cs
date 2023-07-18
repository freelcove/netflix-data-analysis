using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_Analyzer
{
    public class Country
    {
        public static List<string> columns { get; } = new List<string>() { "id", "name", "region", "population", "gdp(per 1,000)", "gdp_per_capita" };

        public int id { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public int population { get; set; }
        public int gdp { get; set; }
        public int gdp_per_capita { get; set; }
    }
}
