using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class ScheduleAdd
    {
        public int AccountId {  get; set; }
        public DateOnly Date {  get; set; }
        public string Note {  get; set; }
    }
}
