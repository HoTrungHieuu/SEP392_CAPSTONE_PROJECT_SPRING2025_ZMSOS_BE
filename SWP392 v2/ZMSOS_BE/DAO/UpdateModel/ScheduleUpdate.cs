using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class ScheduleUpdate
    {
        public int Id { get; set; }
        public DateOnly Date {  get; set; }
        public string Note {  get; set; }
    }
}
