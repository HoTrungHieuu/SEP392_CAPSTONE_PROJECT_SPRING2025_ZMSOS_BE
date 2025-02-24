using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ITaskEstimateRepository : IGenericRepository<TaskEstimate>
    {
        public Task<List<TaskEstimate>?> GetListTaskEstimate();
        public Task<List<TaskEstimate>?> GetListTaskEstimateByTaskTypeId(int taskTypeId);
        public Task<List<TaskEstimate>?> GetListTaskEstimateByAnimalTypeId(int animalTypeId);
        public Task<TaskEstimate> AddTaskEstimate(TaskEstimateAdd key);
        public Task<TaskEstimate?> UpdateTaskEstimate(TaskEstimateUpdate key);
        public List<TaskEstimateView> ConvertListTaskEstimateIntoListTaskEstimateView(List<TaskEstimate> taskEstimates, List<TaskTypeView> taskTypes, List<AnimalTypeView> animalTypes);
        public TaskEstimateView ConvertTaskEstimateIntoTaskEstimateView(TaskEstimate taskEstimate, TaskTypeView taskType, AnimalTypeView animalType);
    }
}
