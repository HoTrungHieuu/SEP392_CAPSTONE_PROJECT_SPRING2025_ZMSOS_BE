using BO.Models;
using DAO.AddModel;
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
        public List<TaskView> ConvertListTaskIntoListTaskView(List<BO.Models.Task> tasks);
        public TaskView ConvertTaskIntoTaskView(BO.Models.Task task);
    }
}
