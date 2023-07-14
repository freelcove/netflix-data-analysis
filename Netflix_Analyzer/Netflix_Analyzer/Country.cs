using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_Analyzer
{
    public class Country
    {
        public int id { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public int population { get; set; }
        public int gdp { get; set; }
        public int gdp_per_capita { get; set; }
    }
}
