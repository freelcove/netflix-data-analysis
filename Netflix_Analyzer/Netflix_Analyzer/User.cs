using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_Analyzer
{
    public class User
    {
        public int id { get; set; }
        public int subcription_type { get; set; }
        public DateTime join_date { get; set; }
        public DateTime last_payment_date { get; set; }
        public int country { get; set; }
        public int gender { get; set; } 
        public int device { get; set; }
        public DateTime birth_date { get; set; }
        public int preferred_genre { get; set; }
        public int avrage_watch_time { get; set; }
    }
}
