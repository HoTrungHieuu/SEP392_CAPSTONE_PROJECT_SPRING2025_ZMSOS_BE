using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ITaskEstimateService
    {
        public Task<ServiceResult> GetListTaskEstimate();
        public Task<ServiceResult> GetListTaskEstimateByTaskTypeId(int taskTypeId);
        public Task<ServiceResult> GetListTaskEstimateByAnimalTypeId(int animalTypeId);
        public Task<ServiceResult> GetTaskEstimateById(int id);
        public Task<ServiceResult> AddTaskEstimate(TaskEstimateAdd key);
        public Task<ServiceResult> UpdateTaskEstimate(TaskEstimateUpdate key);
    }
}
