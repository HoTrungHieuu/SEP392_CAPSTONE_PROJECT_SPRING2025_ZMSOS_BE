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
        public string? TimeStartActual {  get; set; }
        public string? TimeStart { get; set; }
        public string? TimeFinish { get; set; }
        public string? UrlImage {  get; set; }
        public string? Status {  get; set; }
        public void ConvertTaskIntoTaskView(BO.Models.Task key,List<AnimalCageTask> animalCageTask, List<AnimalCageTaskCleaning> animalCageTaskCleaning, List<AnimalCageTaskNormal> animalCageTaskNormal, TaskTypeView taskType)
        {
            Id = key.Id;
            TaskName = key.TaskName;
            Description = key.Description;
            Note = key.Note;    
            TimeStartActual = key.TimeStartActual?.ToString("HH:mm:ss") ?? null;
            TimeStart = key.TimeStart?.ToString("HH:mm:ss") ?? null;
            TimeFinish = key.TimeFinish?.ToString("HH:mm:ss") ?? null;
            UrlImage = key.UrlImage;
            Status = key.Status;
            AnimalCageTask = animalCageTask;
            AnimalCageTaskCleaning = animalCageTaskCleaning;
            AnimalCageTaskNormal = animalCageTaskNormal;
            TaskType = taskType;
        }
    }
}
