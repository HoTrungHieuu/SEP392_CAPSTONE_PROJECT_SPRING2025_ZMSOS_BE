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
        public Task<TaskHealth?> GetTaskHealthByAnimalAssignId(int animalAssignId);
        public Task<TaskHealth> AddHealthTask(int animalAssignId);
        public Task<TaskHealth> UpdateHealthTask(int animalAssignId, TaskHealthUpdate key);
        public TaskHealthView ConvertTaskHealthIntoTaskHealthView(TaskHealth taskHealth);
    }
}
