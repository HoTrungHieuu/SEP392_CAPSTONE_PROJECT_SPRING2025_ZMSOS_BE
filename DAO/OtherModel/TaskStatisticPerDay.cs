using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TaskStatisticPerDay
    {
        public DateOnly Date { get; set; }
        public List<AccountTask>? AccountTasks { get; set; }
    }
}
