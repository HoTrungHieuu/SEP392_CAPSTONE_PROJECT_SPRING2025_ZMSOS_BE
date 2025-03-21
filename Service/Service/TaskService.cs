using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public IScheduleRepository scheduleRepo;
        public IAnimalCageRepository animalCageRepo;
        public IAnimalAssignRepository animalAssignRepo;
        public ITaskMealRepository taskMealRepo;
        public IAnimalRepository animalRepo;
        public IMealDayRepository mealDayRepo;
        public IObjectViewService objectViewService;
        public TaskService(ITaskRepository repo, IAnimalCageRepository animalCageRepo, IAnimalAssignRepository animalAssignRepo, IObjectViewService objectViewService, ITaskMealRepository taskMealRepo, IAnimalRepository animalRepo, IMealDayRepository mealDayRepo, IScheduleRepository scheduleRepo)
        {
            this.repo = repo;
            this.animalCageRepo = animalCageRepo;
            this.animalAssignRepo = animalAssignRepo;
            this.objectViewService = objectViewService;
            this.taskMealRepo = taskMealRepo;
            this.animalRepo = animalRepo;
            this.mealDayRepo = mealDayRepo;
            this.scheduleRepo = scheduleRepo;
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
                await AddTaskSimply(key);
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
                List<List<(BO.Models.Task,DateOnly)>> tt = new();
                foreach(var item1 in key.AnimalTasksId)
                {
                    List<(BO.Models.Task, DateOnly)> t = new();
                    List<(List<DateTime>,float,int,MealDay?)> datess = new();
                    foreach (var item2 in item1.AnimalMealIds)
                    {
                        (MealDay?,float) mealDay = (mealDayRepo.GetById(item2.TaskMeal.MealDayId),item2.TaskMeal.Percent);
                        datess.Add((GetListDateTimeDayMeal(key.FromDate, key.ToDate, mealDay.Item1), mealDay.Item2, item2.AnimalId, mealDay.Item1));
                    }
                    for(int i = 0; i< datess.Count;i++)
                    {
                        foreach(var item2 in datess[i].Item1)
                        {
                            List<int> animalIdCurrent = new();
                            animalIdCurrent.Add(datess[i].Item3);
                            for (int j = i+1;j < datess.Count; j++)
                            {
                                if (datess[j].Item1.Contains(item2))
                                {
                                    animalIdCurrent.Add(datess[j].Item3);
                                    datess[j].Item1.Remove(item2);
                                }
                            }
                            List<AnimalMealId> keyAdd1 = new();
                            foreach(var item3 in animalIdCurrent)
                            {
                                keyAdd1.Add(new()
                                {
                                    AnimalId = item3,
                                    TaskMeal = new()
                                    {
                                        MealDayId = datess.FirstOrDefault(l=>l.Item3==item3).Item4.Id,
                                        Percent = datess.FirstOrDefault(l => l.Item3 == item3).Item2
                                    }
                                });
                            }
                            AnimalCageTaskId keyAdd2 = new()
                            {
                                CageId = item1.CageId,
                                AnimalMealIds = keyAdd1
                            };
                            List<AnimalCageTaskId> keyAdd2s = new();
                            keyAdd2s.Add(keyAdd2);
                            var task = await AddTaskSimply(new()
                            {
                                TaskTypeId = 1,
                                TimeStart = TimeOnly.FromDateTime(item2),
                                AnimalTasksId = keyAdd2s
                            });
                            t.Add((task, DateOnly.FromDateTime(item2)));
                        }
                    }
                    tt.Add(t);
                }
                List<List<Schedule>> ss = new();
                foreach(var item in key.AccountIds)
                {
                    ss.Add(await scheduleRepo.GetListScheduleByAccountId(item));
                }
                for (DateOnly date = key.FromDate; date <= key.ToDate; date = date.AddDays(1))
                {

                    List<List<(BO.Models.Task, DateOnly)>> ttTemp = new();
                    foreach (var item1 in tt)
                    {
                        var tTemp = item1.FindAll(l => l.Item2 == date);
                        ttTemp.Add(tTemp);
                    }
                    if (ttTemp.Count > 0)
                    {
                        List<List<Schedule>> ssTemp = new();
                        foreach (var item1 in ss)
                        {
                            var sTemp = item1.FindAll(l => l.Date == date);
                            ssTemp.Add(sTemp);
                        }
                        int count = 0;
                        foreach (var item1 in ttTemp)
                        {
                            foreach (var item2 in item1)
                            {
                                if (count >= ssTemp.Count) count = 0;
                                item2.Item1.ScheduleId = ssTemp[count][0].Id;
                                await repo.UpdateAsync(item2.Item1);
                                count++;
                            }
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
        private async System.Threading.Tasks.Task<BO.Models.Task> AddTaskSimply(TaskAdd key)
        {
            try
            {
                var task = await repo.AddTask(key);
                foreach (var item1 in key.AnimalTasksId)
                {
                    List<(int?, TaskMealAdd?)> animalCageIds = new();
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
                return task;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
