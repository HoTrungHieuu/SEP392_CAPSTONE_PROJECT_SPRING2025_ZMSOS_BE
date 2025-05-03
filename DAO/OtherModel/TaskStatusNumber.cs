using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TaskStatusNumber
    {
        public int TotalTaskNumber { get; set; } = 0;
        public int TaskNotStart { get; set; } = 0;
        public int TaskFinished { get; set; } = 0;
    }
}
