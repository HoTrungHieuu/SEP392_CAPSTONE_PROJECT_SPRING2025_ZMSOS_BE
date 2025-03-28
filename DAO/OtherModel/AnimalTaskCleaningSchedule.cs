using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalTaskCleaningSchedule
    {
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public List<int>? AccountIds { get; set; }
        public List<AnimalCageTaskCleaningId>? AnimalTaskCleaningsId { get; set; }
    }
}
