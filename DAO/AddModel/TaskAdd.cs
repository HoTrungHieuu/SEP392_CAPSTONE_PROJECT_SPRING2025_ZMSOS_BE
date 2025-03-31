using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class TaskAdd
    {
        public int? ScheduleId { get; set; }
        public int? TaskTypeId { get; set; }
        public string? TaskTypeName {  get; set; }
        public string? TaskName {  get; set; }
        public string? Description {  get; set; }
        public TimeOnly? TimeStart { get; set; }
        public List<AnimalCageTaskNormalId>? AnimalTaskNormalsId { get; set; }
        public List<AnimalCageTaskId>? AnimalTasksId { get; set; }
        public List<AnimalCageTaskCleaningId>? AnimalTaskCleaningsId { get; set; }
    }
}
