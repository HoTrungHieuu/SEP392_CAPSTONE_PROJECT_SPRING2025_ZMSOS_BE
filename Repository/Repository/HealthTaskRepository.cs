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
        public async Task<TaskHealth?> GetTaskHealthByAnimalAssignId(int animalAssignId)
        {
            try
            {
                var healthTask = (await GetAllAsync()).FirstOrDefault(l => l.AnimalAssignId == animalAssignId);
                return healthTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TaskHealth> AddHealthTask(int animalAssignId)
        {
            try
            {
                TaskHealth healthTask = new()
                {
                    AnimalAssignId = animalAssignId,
                };
                await CreateAsync(healthTask);
                return healthTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TaskHealth> UpdateHealthTask(TaskHealthUpdate key)
        {
            try
            {
                var healthTask = GetById(key.Id);
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
        public TaskHealthView ConvertTaskHealthIntoTaskHealthView(TaskHealth taskHealth)
        {
            try
            {
                TaskHealthView result = new TaskHealthView();
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
