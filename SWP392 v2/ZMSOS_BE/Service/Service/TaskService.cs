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
    public class TaskService : ITaskService
    {
        public ITaskRepository repo;
        public IAnimalCageRepository animalCageRepo;
        public IAnimalAssignRepository animalAssignRepo;
        public IObjectViewService objectViewService;
        public TaskService(ITaskRepository repo, IAnimalCageRepository animalCageRepo, IAnimalAssignRepository animalAssignRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.animalCageRepo = animalCageRepo;
            this.animalAssignRepo = animalAssignRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListTaskByScheduleId(int scheduleId)
        {
            try
            {
                var tasks = await repo.GetListTaskByScheduleId(scheduleId);
                if (tasks == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListTaskView(tasks);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Tasks",
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
        public async Task<ServiceResult> GetTaskById(int id)
        {
            try
            {
                var task = repo.GetById(id);
                if (task == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetTaskView(task);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Task",
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
        public async Task<ServiceResult> AddTask(TaskAdd key)
        {
            try
            {
                var task = await repo.AddTask(key);
                foreach (var item1 in key.AnimalTasksId)
                {
                    List<int?> animalCageIds = new();
                    if(item1.AnimalIds.Count == 0)
                    {
                        animalCageIds = await animalCageRepo.GetListAnimalCageIdByCageId(item1.CageId);
                    }
                    else
                    {
                        foreach (var item2 in item1.AnimalIds)
                        {
                            animalCageIds.Add(await animalCageRepo.GetAnimalCageIdByCageIdAndAnimalId(item1.CageId, item2));
                        }
                    }

                    foreach (var item3 in animalCageIds)
                    {
                        if (item3 != null)
                        {
                            await animalAssignRepo.AddAnimalAssign(task.Id, (int)item3);
                        }
                    }
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
        public async Task<ServiceResult> UpdateTask(TaskUpdate key)
        {
            try
            {
                var task = await repo.UpdateTask(key);
                if (task == null)
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
