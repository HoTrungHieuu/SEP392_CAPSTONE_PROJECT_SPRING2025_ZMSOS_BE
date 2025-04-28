using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TaskAddManual
    {
        public int? ScheduleId { get; set; }
        public int? TaskTypeId { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public TimeOnly? TimeStart { get; set; }
    }
}
