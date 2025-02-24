using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAO.ViewModel
{
    public class TaskView
    {
        public int Id { get; set; }
        public string TaskName {  get; set; }
        public string Description {  get; set; }
        public string Note {  get; set; }
        public TimeOnly? TimeStart { get; set; }
        public TimeOnly? TimeEnd { get; set; }
        public string Status {  get; set; }
        public void ConvertTaskIntoTaskView(BO.Models.Task key)
        {
            Id = key.Id;
            TaskName = key.TaskName;
            Description = key.Description;
            Note = key.Note;    
            TimeStart = key.TimeStart;
            TimeEnd = key.TimeEnd;
            Status = key.Status;
        }
    }
}
