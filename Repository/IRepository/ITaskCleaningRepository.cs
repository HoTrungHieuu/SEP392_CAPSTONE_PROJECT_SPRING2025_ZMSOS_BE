using BO.Models;
using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ITaskCleaningRepository : IGenericRepository<CleaningTask>
    {
        public Task<CleaningTask?> GetTaskCleaningByAnimalAssignId(int animalAssignId);
        public Task<CleaningTask> AddTaskCleaning(int animalAssignId, TaskCleaningAdd key);
    }
}
