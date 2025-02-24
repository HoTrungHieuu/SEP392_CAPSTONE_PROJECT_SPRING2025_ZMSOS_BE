using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAO.ViewModel
{
    public class TaskEstimateView
    {
        public int Id { get; set; }
        public TaskTypeView TaskType { get; set; }
        public AnimalTypeView AnimalType { get; set; }
        public string TimeEstimate {  get; set; }
        public string Status {  get; set; }
        public void ConvertTaskEstimateIntoTaskEstimateView(TaskEstimate key, TaskTypeView taskType, AnimalTypeView animalType)
        {
            Id = key.Id;
            TaskType = taskType;
            AnimalType = animalType;
            TimeEstimate = key.TimeEstimate;
            Status = key.Status;
        }
    }
}
