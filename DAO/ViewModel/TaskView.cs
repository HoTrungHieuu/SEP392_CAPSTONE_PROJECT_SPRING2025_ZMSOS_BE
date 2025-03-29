using BO.Models;
using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAO.ViewModel
{
    public class TaskView
    {
        public int Id { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AnimalCageTaskNormal>? AnimalCageTaskNormal { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AnimalCageTask>? AnimalCageTask {  get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AnimalCageTaskCleaning>? AnimalCageTaskCleaning { get; set; }
        public TaskTypeView? TaskType { get;set; }
        public TimeOnly? TimeStart { get; set; }
        public TimeOnly? TimeFinish { get; set; }
        public string? Status {  get; set; }
        public void ConvertTaskIntoTaskView(BO.Models.Task key,List<AnimalCageTask> animalCageTask, List<AnimalCageTaskCleaning> animalCageTaskCleaning, List<AnimalCageTaskNormal> animalCageTaskNormal, TaskTypeView taskType)
        {
            Id = key.Id;
            TaskName = key.TaskName;
            Description = key.Description;
            Note = key.Note;    
            TimeStart = key.TimeStart;
            TimeFinish = key.TimeFinish;
            Status = key.Status;
            AnimalCageTask = animalCageTask;
            AnimalCageTaskCleaning = animalCageTaskCleaning;
            AnimalCageTaskNormal = animalCageTaskNormal;
            TaskType = taskType;
        }
    }
}
