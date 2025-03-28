using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TimeInterval
    {
        public List<TimeOnly>? Times { get; set; }
        public TimeSpan? Day_Interval { get; set; }
    }
}
