using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class TaskUpdate
    {
        public int Id { get; set; }
        public string TaskName {  get; set; }
        public string Description {  get; set; }
        public string Note {  get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
    }
}
