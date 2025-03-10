using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
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
    public class TaskTypeService : ITaskTypeService
    {
        public ITaskTypeRepository repo;
        public IObjectViewService objectViewService;
        public TaskTypeService(ITaskTypeRepository repo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListTaskType()
        {
            try
            {
                var taskTypes = await repo.GetListTaskType();
                if (taskTypes == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListTaskTypeView(taskTypes);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Task Types",
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
        public async Task<ServiceResult> GetTaskTypeById(int id)
        {
            try
            {
                var taskType = repo.GetById(id);
                if (taskType == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetTaskTypeView(taskType);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Task Type",
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
        public async Task<ServiceResult> AddTaskType(TaskTypeAdd key)
        {
            try
            {
                var taskType = await repo.AddTaskType(key);
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
        public async Task<ServiceResult> UpdateTaskType(TaskTypeUpdate key)
        {
            try
            {
                var taskType = await repo.UpdateTaskType(key);
                if (taskType == null)
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
    }
}
