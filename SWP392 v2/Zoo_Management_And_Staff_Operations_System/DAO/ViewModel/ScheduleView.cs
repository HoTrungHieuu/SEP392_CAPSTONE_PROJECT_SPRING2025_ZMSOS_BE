using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class ScheduleView
    {
        public int Id { get; set; }
        public UserView User { get; set; }
        public DateOnly? Date { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public void ConvertSchedualIntoSchedualView(Schedule key, UserView user)
        {
            Id = key.Id;
            User = user;
            Date = key.Date;
            Note = key.Note;
            Status = key.Status;
        }
    }
}
