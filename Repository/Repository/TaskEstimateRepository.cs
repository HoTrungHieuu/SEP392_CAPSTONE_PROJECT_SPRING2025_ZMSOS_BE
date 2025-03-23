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
    public class TaskEstimateRepository : GenericRepository<TaskEstimate>, ITaskEstimateRepository
    {
        public TaskEstimateRepository()
        {
        }
        public async Task<List<TaskEstimate>?> GetListTaskEstimate()
        {
            try
            {
                var taskEstimates = await GetAllAsync();
                return taskEstimates;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<TaskEstimate>?> GetListTaskEstimateByTaskTypeId(int taskTypeId)
        {
            try
            {
                var taskEstimates = (await GetAllAsync()).FindAll(l=>l.TaskTypeId == taskTypeId);
                return taskEstimates;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<TaskEstimate>?> GetListTaskEstimateByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var taskEstimates = (await GetAllAsync()).FindAll(l => l.AnimalTypeId == animalTypeId);
                return taskEstimates;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TaskEstimate> AddTaskEstimate(TaskEstimateAdd key)
        {
            try
            {
                TaskEstimate taskEstimate = new()
                {
                    TaskTypeId = key.TaskTypeId,
                    AnimalTypeId = key.AnimalTypeId,
                    TimeEstimate = key.TimeEstimate,
                };
                await CreateAsync(taskEstimate);
                return taskEstimate;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TaskEstimate?> UpdateTaskEstimate(TaskEstimateUpdate key)
        {
            try
            {
                TaskEstimate taskEstimate = GetById(key.Id);
                if (taskEstimate == null)
                {
                    return null;
                }
                taskEstimate.TaskTypeId = key.TaskTypeId;
                taskEstimate.AnimalTypeId = key.AnimalTypeId;
                taskEstimate.TimeEstimate = key.TimeEstimate;
                await UpdateAsync(taskEstimate);
                return taskEstimate;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TaskEstimateView ConvertTaskEstimateIntoTaskEstimateView(TaskEstimate taskEstimate, TaskTypeView taskType, AnimalTypeView animalType)
        {
            try
            {
                TaskEstimateView result = new TaskEstimateView();
                result.ConvertTaskEstimateIntoTaskEstimateView(taskEstimate,taskType,animalType);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
