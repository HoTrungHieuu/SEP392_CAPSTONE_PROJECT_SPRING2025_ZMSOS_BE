using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class StaffScheduleStatistic
    {
        public AccountView? Account { get; set; }
        public int TotalSchedule { get; set; }
        public int TotalPresentSchedule { get; set; }
        public int TotalAbsentSchedule { get; set; }
        public TaskStatusNumber TaskNumber { get; set; }
    }
}
