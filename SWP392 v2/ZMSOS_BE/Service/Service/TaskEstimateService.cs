using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class TaskEstimateService : ITaskEstimateService
    {
        public ITaskEstimateRepository repo;
        public ITaskTypeRepository taskTypeRepo;
        public IAnimalTypeRepository animalTypeRepo;
        public IObjectViewService objectViewService;
        public TaskEstimateService(ITaskEstimateRepository repo, ITaskTypeRepository taskTypeRepo, IAnimalTypeRepository animalTypeRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.taskTypeRepo = taskTypeRepo;
            this.animalTypeRepo = animalTypeRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListTaskEstimate()
        {
            try
            {
                var taskEstimates = await repo.GetListTaskEstimate();
                if (taskEstimates == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListTaskEstimateView(taskEstimates);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Task Estimates",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GetListTaskEstimateByTaskTypeId(int taskTypeId)
        {
            try
            {
                var taskEstimates = await repo.GetListTaskEstimate();
                if (taskEstimates == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListTaskEstimateView(taskEstimates);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Task Estimates",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GetListTaskEstimateByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var taskEstimates = await repo.GetListTaskEstimateByAnimalTypeId(animalTypeId);
                if (taskEstimates == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListTaskEstimateView(taskEstimates);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Task Estimates",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GetTaskEstimateById(int id)
        {
            try
            {
                var taskEstimate = repo.GetById(id);
                if (taskEstimate == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetTaskEstimateView(taskEstimate);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Task Estimate",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddTaskEstimate(TaskEstimateAdd key)
        {
            try
            {
                var taskEstimate = await repo.AddTaskEstimate(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> UpdateTaskEstimate(TaskEstimateUpdate key)
        {
            try
            {
                var taskEstimate = await repo.UpdateTaskEstimate(key);
                if (taskEstimate == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
