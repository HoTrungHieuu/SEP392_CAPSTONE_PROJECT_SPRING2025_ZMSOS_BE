using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TaskAccountPerDay
    {
        public DateOnly? Date {  get; set; }
        public List<TaskView>? Tasks { get; set; }
    }
}
