using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositoyr
{
    public interface ITaskRepository : IGenericRepository<BO.Models.Task>
    {
        public Task<List<BO.Models.Task>?> GetListTaskByScheduleId(int scheduleId);
        public Task<BO.Models.Task> AddTask(TaskAdd key);
        public Task<BO.Models.Task?> UpdateTask(TaskUpdate key);
        public Task<BO.Models.Task?> TranferTask(int id, int scheduleId);
        public Task<BO.Models.Task?> UpdateTaskStaff(TaskStaffUpdate key);
        public Task<BO.Models.Task?> StartTask(int id);
        public TaskView ConvertTaskIntoTaskView(BO.Models.Task task, List<AnimalCageTask> animalCageTasks, List<AnimalCageTaskCleaning> animalCageTaskCleanings, List<AnimalCageTaskNormal> animalCageTaskNormals, TaskTypeView taskType);
    }
}
