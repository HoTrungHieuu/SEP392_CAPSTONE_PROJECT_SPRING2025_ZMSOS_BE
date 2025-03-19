using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
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
        public ITaskMealRepository taskMealRepo;
        public IObjectViewService objectViewService;
        public TaskService(ITaskRepository repo, IAnimalCageRepository animalCageRepo, IAnimalAssignRepository animalAssignRepo, IObjectViewService objectViewService, ITaskMealRepository taskMealRepo)
        {
            this.repo = repo;
            this.animalCageRepo = animalCageRepo;
            this.animalAssignRepo = animalAssignRepo;
            this.objectViewService = objectViewService;
            this.taskMealRepo = taskMealRepo;
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
                    List<(int?,TaskMealAdd?)> animalCageIds = new();
                    foreach (var item2 in item1.AnimalMealIds)
                    {
                        animalCageIds.Add((await animalCageRepo.GetAnimalCageIdByCageIdAndAnimalId(item1.CageId, item2.AnimalId), item2.TaskMeal));
                    }

                    foreach (var item3 in animalCageIds)
                    {
                        if (item3.Item1 != null)
                        {
                            var animalAssign = await animalAssignRepo.AddAnimalAssign(task.Id, (int)item3.Item1);
                            await taskMealRepo.AddTaskMeal(animalAssign.Id, item3.Item2);
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
        public async Task<ServiceResult> AddTaskAutomatic(AnimalTaskMealSchdule key)
        {
            try
            {
                List<List<System.Threading.Tasks.Task>> tt = new();
                foreach(var item1 in key.AnimalTasksId)
                {
                    List<System.Threading.Tasks.Task> t = new();

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
        private List<DateTime> GetListDateTimeDayMeal(DateOnly fromDate, DateOnly toDate, MealDay key)
        {
            List<DateTime> result = new List<DateTime>();
            TimeSpan periodOfTime = TimeSpan.Parse(key.PeriodOfTime);
            TimeOnly timeStart = (TimeOnly)key.TimeStartInDay;
            int count = 0;
            for (DateOnly date = fromDate; date <= toDate; date = date.AddDays(1 + count+periodOfTime.Days))
            {
                while(timeStart <= (TimeOnly)key.TimeEndInDay)
                {
                    result.Add(date.ToDateTime(timeStart));
                    timeStart = TimeOnly.FromTimeSpan(timeStart.ToTimeSpan() + new TimeSpan(hours: periodOfTime.Hours, 0 , 0));
                    if (periodOfTime.Days > 0)
                    {
                        break;
                    }
                }
                if(timeStart > (TimeOnly)key.TimeEndInDay)
                {
                    timeStart = (TimeOnly)key.TimeStartInDay;
                    count = 0;
                }
                else
                {
                    count = -1;
                }
            }
            return result;
        }
    }
}
