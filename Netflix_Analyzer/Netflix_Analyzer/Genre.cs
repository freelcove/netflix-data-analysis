using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_Analyzer
{
    public class Genre:IComparable<Genre>
    {
        public static List<string> columns { get; } = new List<string>() { "id", "name" };
        public int id {  get; set; }
        public string name { get; set; }

        int IComparable<Genre>.CompareTo(Genre other)
        {
            return name.CompareTo(other.name);
        }
    }
}
