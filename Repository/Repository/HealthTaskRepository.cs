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

namespace Repository.Repository
{
    public class HealthTaskRepository : GenericRepository<TaskHealth>, IHealthTaskRepository
    {
        public HealthTaskRepository()
        {
        }
        public async Task<TaskHealth?> GetTaskHealthByTaskId(int taskId)
        {
            try
            {
                var healthTask = (await GetAllAsync()).FirstOrDefault(l => l.TaskId == taskId);
                return healthTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TaskHealth> AddHealthTask(int taskId)
        {
            try
            {
                TaskHealth healthTask = new()
                {
                    TaskId = taskId,
                };
                await CreateAsync(healthTask);
                return healthTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TaskHealth> UpdateHealthTask(int taskId, TaskHealthUpdate key)
        {
            try
            {
                var healthTask = await GetTaskHealthByTaskId(taskId);
                if (healthTask == null) return null;
                healthTask.AnimalCondition = key.AnimalCondition;
                healthTask.DetailInformation = key.DetailInformation;
                healthTask.SeverityLevel = key.SeverityLevel;
                await UpdateAsync(healthTask);
                return healthTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TaskHealView ConvertTaskHealthIntoTaskHealthView(TaskHealth taskHealth)
        {
            try
            {
                TaskHealView result = new TaskHealView();
                result.ConvertTaskHealthIntoTaskHealthView(taskHealth);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
