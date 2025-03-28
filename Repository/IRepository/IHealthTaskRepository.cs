using BO.Models;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IHealthTaskRepository : IGenericRepository<TaskHealth>
    {
        public Task<TaskHealth?> GetTaskHealthByTaskId(int taskId);
        public Task<TaskHealth> AddHealthTask(int taskId);
        public Task<TaskHealth> UpdateHealthTask(int taskId, TaskHealthUpdate key);
        public TaskHealView ConvertTaskHealthIntoTaskHealthView(TaskHealth taskHealth);
    }
}
