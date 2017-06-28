
using Realms;
using System;

namespace Wtd.PCL.Models
{
    public class Job : RealmObject
    {
        [PrimaryKey]
        public string JodID { get; set; } = Guid.NewGuid().ToString();

        public DateTimeOffset Date { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        
        [Ignored]
        public string TypeImage { get
            {
                return string.Format("t{0}.png", Type);
            }
        }
    }
}
