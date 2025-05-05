using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class LeaderStatistic
    {
        public int TotalStaff {  get; set; }
        public int TotalCage {  get; set; }
        public int TotalAnimalIndividual { get; set; }
        public int TotalAnimalFlock {  get; set; }
        public List<StaffScheduleStatistic>? ScheduleStatistic { get; set; }
    }
}
