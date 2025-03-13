using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class ScheduleAutoAdd
    {
        public int? AccountId {  get; set; }
        public DateOnly? FromDate {  get; set; }
        public DateOnly? ToDate { get; set; }
        public Day? DayOfWeek { get; set; }
        public List<DateOnly>? DateExclution { get; set; }
    }
}
