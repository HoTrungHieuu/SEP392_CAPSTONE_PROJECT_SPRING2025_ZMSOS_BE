using BO.Models;
using DAO.AddModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TaskCleaningRepository : GenericRepository<CleaningTask>, ITaskCleaningRepository
    {
        public TaskCleaningRepository()
        {
        }
        public async Task<CleaningTask?> GetTaskCleaningByAnimalAssignId(int animalAssignId)
        {
            try
            {
                var taskCleaning = (await GetAllAsync()).FirstOrDefault(l => l.AnimalAssignId == animalAssignId);
                return taskCleaning;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<CleaningTask>?> GetListTaskCleaningByCleaningOptionId(int cleaningOptionId)
        {
            try
            {
                var taskCleanings = (await GetAllAsync()).FindAll(l => l.AnimalAssignId == cleaningOptionId);
                return taskCleanings;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CleaningTask> AddTaskCleaning(int animalAssignId, TaskCleaningAdd key)
        {
            try
            {
                CleaningTask taskCleaning = new()
                {
                    AnimalAssignId = animalAssignId,
                    CleaningOptionId = key.CleaningOptionId
                };
                await CreateAsync(taskCleaning);
                return taskCleaning;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
