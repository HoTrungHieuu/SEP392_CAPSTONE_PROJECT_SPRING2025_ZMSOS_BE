using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class Day
    {
        public bool IsMonday {  get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get;set; }
        public bool IsThursday { get;set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }
        public bool IsSunday {  get; set; }
        public static bool CheckDateOfWeek(DateOnly date, Day day)
        {
            if (day.IsSunday && date.DayOfWeek == DayOfWeek.Sunday) return true;
            if (day.IsMonday && date.DayOfWeek == DayOfWeek.Monday) return true;
            if (day.IsTuesday && date.DayOfWeek == DayOfWeek.Tuesday) return true;
            if (day.IsWednesday && date.DayOfWeek == DayOfWeek.Wednesday) return true;
            if (day.IsThursday && date.DayOfWeek == DayOfWeek.Thursday) return true;
            if (day.IsFriday && date.DayOfWeek == DayOfWeek.Friday) return true;
            if (day.IsSaturday && date.DayOfWeek == DayOfWeek.Saturday) return true;
            return false;
        }
    }
}
