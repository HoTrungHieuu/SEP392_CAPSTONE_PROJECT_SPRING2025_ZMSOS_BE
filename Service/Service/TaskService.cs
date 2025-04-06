using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nest;
using NPOI.SS.Formula.Functions;
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
        public ITaskCleaningRepository taskCleaningRepo;
        public IAnimalRepository animalRepo;
        public IMealDayRepository mealDayRepo;
        public ICleaningOptionRepository cleaningOptionRepo;
        public IObjectViewService objectViewService;
        public IHealthTaskRepository healthTaskRepo;
        public IAccountRepository accountRepo;
        public ICageRepository cageRepo;
        public IMemberAssignRepository memberAssignRepo;
        public ITeamRepository teamRepo;
        
        public TaskService(ITaskRepository repo, IAnimalCageRepository animalCageRepo, IAnimalAssignRepository animalAssignRepo, IObjectViewService objectViewService, ITaskMealRepository taskMealRepo, IAnimalRepository animalRepo, IMealDayRepository mealDayRepo, IScheduleRepository scheduleRepo, ITaskCleaningRepository taskCleaningRepo, ICleaningOptionRepository cleaningOptionRepo, IHealthTaskRepository healthTaskRepo,IAccountRepository accountRepo, ICageRepository cageRepo, IMemberAssignRepository memberAssignRepo,ITeamRepository teamRepo)
        {
            this.repo = repo;
            this.animalCageRepo = animalCageRepo;
            this.animalAssignRepo = animalAssignRepo;
            this.objectViewService = objectViewService;
            this.taskMealRepo = taskMealRepo;
            this.animalRepo = animalRepo;
            this.mealDayRepo = mealDayRepo;
            this.scheduleRepo = scheduleRepo;
            this.taskCleaningRepo = taskCleaningRepo;
            this.cleaningOptionRepo = cleaningOptionRepo;
            this.healthTaskRepo = healthTaskRepo;
            this.accountRepo = accountRepo;
            this.cageRepo = cageRepo;
            this.memberAssignRepo = memberAssignRepo;
            this.teamRepo = teamRepo;
        }
        public async Task<ServiceResult> GetListTaskByDateByTeamId(int teamId, DateOnly fromDate, DateOnly toDate)
        {
            try
            {
                if(fromDate > toDate)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Date invalid"
                    };
                }
                var members = await memberAssignRepo.GetListMemberAssignByTeamId(teamId);
                List<TaskStatisticPerDay> taskStatisticPerDays = new List<TaskStatisticPerDay>();
                for (DateOnly date = fromDate; date <= toDate; date = date.AddDays(1))
                {
                    TaskStatisticPerDay taskStatisticPerDay = new()
                    {
                        Date = date,
                        AccountTasks = new()
                    };
                    foreach(var member in members)
                    {
                        var schedules = await scheduleRepo.GetListScheduleByAccountIdByDate((int)member.MemberId, date, date);
                        if (schedules.Count > 0)
                        {
                            var tasks = await repo.GetListTaskByScheduleId(schedules[0].Id);
                            foreach(var task in tasks)
                            {
                                var taskAccount = taskStatisticPerDay.AccountTasks.FirstOrDefault(l => l.Account.Id == member.MemberId);
                                if (taskAccount == null)
                                {

                                    taskStatisticPerDay.AccountTasks.Add(new()
                                    {
                                        Account = await objectViewService.GetAccountView(accountRepo.GetById(member.MemberId)),
                                        TaskQuantity = new()
                                        {
                                            TotalTaskNumber = 1,
                                            TotalTaskMeal = (task?.TaskTypeId == 1) ? 1 : 0,
                                            TotalTaskCleaning = (task?.TaskTypeId == 2) ? 1 : 0,
                                            TotalTaskHealth = (task?.TaskTypeId == 3) ? 1 : 0,
                                        },
                                    });
                                }
                                else
                                {
                                    taskAccount.TaskQuantity.TotalTaskNumber += 1;
                                    taskAccount.TaskQuantity.TotalTaskMeal += (task?.TaskTypeId == 1) ? 1 : 0;
                                    taskAccount.TaskQuantity.TotalTaskCleaning += (task?.TaskTypeId == 2) ? 1 : 0;
                                    taskAccount.TaskQuantity.TotalTaskHealth += (task?.TaskTypeId == 3) ? 1 : 0;
                                }
                            }
                            
                        }
                    }
                    taskStatisticPerDays.Add(taskStatisticPerDay);
                }

                return new ServiceResult
                {
                    Status = 200,
                    Message = "Tasks",
                    Data = taskStatisticPerDays
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
        public async Task<ServiceResult> GetListTaskByDateByAccountId(int accountId, DateOnly fromDate, DateOnly toDate)
        {
            try
            {
                if (fromDate > toDate)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Date invalid"
                    };
                }
                var schedules = await scheduleRepo.GetListScheduleByAccountIdByDate(accountId,fromDate,toDate);
                List<TaskAccountPerDay> taskAccountPerDays = new List<TaskAccountPerDay>();
                foreach (var schedule in schedules)
                {
                    var tasks = await repo.GetListTaskByScheduleId(schedule.Id);
                    taskAccountPerDays.Add(new()
                    {
                        Date = schedule?.Date,
                        Tasks = await objectViewService.GetListTaskView(tasks)
                    });
                }

                return new ServiceResult
                {
                    Status = 200,
                    Message = "Tasks",
                    Data = taskAccountPerDays
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
        public async Task<ServiceResult> GetListTaskAnimalByDateByTeamId(int teamId, DateOnly fromDate, DateOnly toDate)
        {
            try
            {
                if (fromDate > toDate)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Date invalid"
                    };
                }
                var members = await memberAssignRepo.GetListMemberAssignByTeamId(teamId);
                List<TaskAnimalDay> taskAnimalDays = new List<TaskAnimalDay>();
                for (DateOnly date = fromDate; date <= toDate; date = date.AddDays(1))
                {
                    TaskAnimalDay taskAnimalDay = new()
                    {
                        Date = date,
                        AnimalTask = new()
                    };
                    
                    foreach (var member in members)
                    {
                        var schedules = await scheduleRepo.GetListScheduleByAccountIdByDate((int)member.MemberId, date, date);
                        if (schedules.Count > 0)
                        {
                            var tasks = await repo.GetListTaskByScheduleId(schedules[0].Id);
                            foreach(var task in tasks)
                            {
                                var animalAssigns = await animalAssignRepo.GetListAnimalAssignByTaskId(task.Id);
                                foreach(var animalAssign in animalAssigns)
                                {
                                    var animalCage = animalCageRepo.GetById(animalAssign.AnimalCageId);
                                    var animal = await objectViewService.GetAnimalView(animalRepo.GetById(animalCage.AnimalId));
                                    var taskAnimal = taskAnimalDay.AnimalTask.FirstOrDefault(l => l.Animal.Id == animal.Id);
                                    if (taskAnimal == null)
                                    {

                                        taskAnimalDay.AnimalTask.Add(new()
                                        {
                                            Animal = animal,
                                            TaskQuantity = new()
                                            {
                                                TotalTaskNumber = 1,
                                                TotalTaskMeal = (task?.TaskTypeId == 1) ? 1 : 0,
                                                TotalTaskCleaning = (task?.TaskTypeId == 2) ? 1 : 0,
                                                TotalTaskHealth = (task?.TaskTypeId == 3) ? 1 : 0,
                                            },
                                        });
                                    }
                                    else
                                    {
                                        taskAnimal.TaskQuantity.TotalTaskNumber += 1;
                                        taskAnimal.TaskQuantity.TotalTaskMeal += (task?.TaskTypeId == 1) ? 1 : 0;
                                        taskAnimal.TaskQuantity.TotalTaskCleaning += (task?.TaskTypeId == 2) ? 1 : 0;
                                        taskAnimal.TaskQuantity.TotalTaskHealth += (task?.TaskTypeId == 3) ? 1 : 0;
                                    }
                                }
                            }
                        }
                        
                    }
                    taskAnimalDays.Add(taskAnimalDay);
                }

                return new ServiceResult
                {
                    Status = 200,
                    Message = "Tasks",
                    Data = taskAnimalDays
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
                var task = repo.GetById(key.Id);
                if (task == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                if(task.Status != "In Progress")
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Can not Update when Task Started or Finished"
                    };
                }
                task = await repo.UpdateTask(key);
                
                
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
        public async Task<ServiceResult> StartTask(int id)
        {
            try
            {
                var task = repo.GetById(id);
                if (task == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                if(task.Status != "Not Start")
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Task Started or Finished Already"
                    };
                }
                task = await repo.StartTask(id);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Start Success",
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
        public async Task<ServiceResult> UpdateTaskStaff(TaskStaffUpdate key)
        {
            try
            {
                var task = repo.GetById(key.Id);
                if (task == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                if (task.Status != "In Progress")
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Task Started or Finished"
                    };
                }
                task = await repo.UpdateTaskStaff(key);
                if (task.TaskTypeId == 3 && key.TeakHealths !=null)
                {
                    foreach(var taskHealth in key.TeakHealths)
                    {
                        await healthTaskRepo.UpdateHealthTask(taskHealth);
                    }
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
        public async Task<ServiceResult> ClearTaskStaff(ClearTask key)
        {
            try
            {
                var schedules = await scheduleRepo.GetListScheduleByAccountIdByDate(key.AccountId, key.FromDate, key.ToDate);
                foreach(var schedule in schedules)
                {
                    var tasks = await repo.GetListTaskByScheduleId(schedule.Id);
                    foreach(var task in tasks)
                    {
                        if(task.Status == "Not Start")
                        {
                            task.Status = "Deleted";
                        }
                        await repo.UpdateAsync(task);
                    }
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Clear Success",
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
                List<int> cagesId = new List<int>();
                foreach(var item in key.AnimalTasksId)
                {
                    cagesId.Add(item.CageId);
                }
               
                if(!(await CheckAutoValidate(key.FromDate, key.ToDate, key.AccountIds)))
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Staffs needs to have a schedule during this time to create the task",
                    };
                }

                var isTaskDuplicateStaff = (await CheckTaskDuplicateStaff(key.FromDate, key.ToDate, key.AccountIds, 1));
                if (!isTaskDuplicateStaff.Item1)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = $"Staff with id {isTaskDuplicateStaff.Item2} had meal task in {isTaskDuplicateStaff.Item3}",
                    };
                }

                for (DateOnly date = key.FromDate; date <= key.ToDate; date = date.AddDays(1))
                {
                    foreach (var cage in key.AnimalTasksId)
                    {
                        foreach (var animal in cage.AnimalMealIds)
                        {
                            if (!(await CheckTaskDuplicate(date, animal.AnimalId, 3)))
                            {
                                return new ServiceResult
                                {
                                    Status = 400,
                                    Message = $"Animal with Id {animal.AnimalId} in Cage Id {cage.CageId} had task Meal in date {date}",
                                };
                            }
                        }
                    }
                }

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
                                TaskTypeName = "Meal",
                                TaskName = "Cho Ăn",
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
                            if(sTemp.Count != 0) ssTemp.Add(sTemp);
                        }
                        if (date.Day % 2 == 1)
                        {
                            ssTemp.Reverse();
                        }
                        int count = 0;
                        foreach (var item1 in ttTemp)
                        {
                            if (count >= ssTemp.Count) count = 0;
                            foreach (var item2 in item1)
                            {
                                item2.Item1.ScheduleId = ssTemp[count][0].Id;
                                await repo.UpdateAsync(item2.Item1);
                            }
                            count++;
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
        public async Task<ServiceResult> AddTaskCleaningAutomatic(AnimalTaskCleaningSchedule key)
        {
            try
            {
                List<int> cagesId = new List<int>();
                foreach (var item in key.AnimalTaskCleaningsId)
                {
                    cagesId.Add(item.CageId);
                }
                
                if (!(await CheckAutoValidate(key.FromDate, key.ToDate, key.AccountIds)))
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "User needs to have a schedule during this time to create the task",
                    };
                }

                var isTaskDuplicateStaff = (await CheckTaskDuplicateStaff(key.FromDate, key.ToDate, key.AccountIds, 2));
                if (!isTaskDuplicateStaff.Item1)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = $"Staff with id {isTaskDuplicateStaff.Item2} had cleaning task in {isTaskDuplicateStaff.Item3}",
                    };
                }

                for (DateOnly date = key.FromDate; date <= key.ToDate; date = date.AddDays(1))
                {
                    foreach (var cage in key.AnimalTaskCleaningsId)
                    {
                        foreach (var animal in cage.AnimalCleaningIds)
                        {
                            if (!(await CheckTaskDuplicate(date, animal.AnimalId, 2)))
                            {
                                return new ServiceResult
                                {
                                    Status = 400,
                                    Message = $"Animal with Id {animal.AnimalId} in Cage Id {cage.CageId} had task Cleaning in date {date}",
                                };
                            }
                        }
                    }
                }

                List<List<(BO.Models.Task, DateOnly)>> tt = new();
                foreach (var item1 in key.AnimalTaskCleaningsId)
                {
                    List<(BO.Models.Task, DateOnly)> t = new();
                    List<(List<DateTime>, int, CleaningOption?)> datess = new();
                    foreach (var item2 in item1.AnimalCleaningIds)
                    {
                        CleaningOption? cleaningOption = cleaningOptionRepo.GetById(item2.TaskCleaning.CleaningOptionId);
                        datess.Add((GetListDateTimeDayCleaning(key.FromDate, key.ToDate, item2.TimeInterval), item2.AnimalId, cleaningOption));
                    }
                    for (int i = 0; i < datess.Count; i++)
                    {
                        foreach (var item2 in datess[i].Item1)
                        {
                            List<int> animalIdCurrent = new();
                            animalIdCurrent.Add(datess[i].Item2);
                            for (int j = i + 1; j < datess.Count; j++)
                            {
                                if (datess[j].Item1.Contains(item2))
                                {
                                    animalIdCurrent.Add(datess[j].Item2);
                                    datess[j].Item1.Remove(item2);
                                }
                            }
                            List<AnimalCleaningId> keyAdd1 = new();
                            foreach (var item3 in animalIdCurrent)
                            {
                                keyAdd1.Add(new()
                                {
                                    AnimalId = item3,
                                    TaskCleaning = new()
                                    {
                                        CleaningOptionId = datess.FirstOrDefault(l => l.Item2 == item3).Item3.Id,
                                    }
                                });
                            }
                            AnimalCageTaskCleaningId keyAdd2 = new()
                            {
                                CageId = item1.CageId,
                                AnimalCleaningIds = keyAdd1
                            };
                            List<AnimalCageTaskCleaningId> keyAdd2s = new();
                            keyAdd2s.Add(keyAdd2);
                            var task = await AddTaskSimply(new()
                            {
                                TaskTypeId = 2,
                                TaskTypeName = "Cleaning",
                                TaskName = "Dọn vệ sinh",
                                TimeStart = TimeOnly.FromDateTime(item2),
                                AnimalTaskCleaningsId = keyAdd2s
                            });
                            t.Add((task, DateOnly.FromDateTime(item2)));
                        }
                    }
                    tt.Add(t);
                }
                List<List<Schedule>> ss = new();
                foreach (var item in key.AccountIds)
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
                            if (sTemp.Count != 0) ssTemp.Add(sTemp);
                        }
                        if (date.Day % 2 == 1)
                        {
                            ssTemp.Reverse();
                        }
                        int count = 0;
                        foreach (var item1 in ttTemp)
                        {
                            if (count >= ssTemp.Count) count = 0;
                            foreach (var item2 in item1)
                            {
                                item2.Item1.ScheduleId = ssTemp[count][0].Id;
                                await repo.UpdateAsync(item2.Item1);
                            }
                            count++;
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
        public async Task<ServiceResult> AddTaskHealthAutomatic(AnimalTaskNormalScheldule key)
        {
            try
            {
                List<int> cagesId = new List<int>();
                foreach (var item in key.AnimalTaskNormalsId)
                {
                    cagesId.Add(item.CageId);
                }
                
                if (!(await CheckAutoValidate(key.FromDate, key.ToDate, key.AccountIds)))
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "User needs to have a schedule during this time to create the task",
                    };
                }
                var isTaskDuplicateStaff = (await CheckTaskDuplicateStaff(key.FromDate, key.ToDate, key.AccountIds, 3));
                if (!isTaskDuplicateStaff.Item1)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = $"Staff with id {isTaskDuplicateStaff.Item2} had health task in {isTaskDuplicateStaff.Item3}",
                    };
                }

                for (DateOnly date = key.FromDate; date <= key.ToDate; date = date.AddDays(1))
                {
                    foreach (var cage in key.AnimalTaskNormalsId)
                    {
                        foreach(var animal in cage.AnimalIds)
                        {
                            if (!(await CheckTaskDuplicate(date, animal.AnimalId, 3)))
                            {
                                return new ServiceResult
                                {
                                    Status = 400,
                                    Message = $"Animal with Id {animal.AnimalId} in Cage Id {cage.CageId} had task Health in date {date}",
                                };
                            }
                        }
                    }
                }

                List<List<(BO.Models.Task, DateOnly)>> tt = new();
                foreach (var item1 in key.AnimalTaskNormalsId)
                {
                    List<(BO.Models.Task, DateOnly)> t = new();
                    List<(List<DateTime>, int)> datess = new();
                    foreach (var item2 in item1.AnimalIds)
                    {
                        datess.Add((GetListDateTimeDayCleaning(key.FromDate, key.ToDate, item2.TimeInterval), item2.AnimalId));
                    }
                    for (int i = 0; i < datess.Count; i++)
                    {
                        foreach (var item2 in datess[i].Item1)
                        {
                            List<int> animalIdCurrent = new();
                            animalIdCurrent.Add(datess[i].Item2);
                            for (int j = i + 1; j < datess.Count; j++)
                            {
                                if (datess[j].Item1.Contains(item2))
                                {
                                    animalIdCurrent.Add(datess[j].Item2);
                                    datess[j].Item1.Remove(item2);
                                }
                            }
                            List<AnimalNormalId> keyAdd1 = new();
                            foreach (var item3 in animalIdCurrent)
                            {
                                keyAdd1.Add(new()
                                {
                                    AnimalId = item3,
                                });
                            }
                            AnimalCageTaskNormalId keyAdd2 = new()
                            {
                                CageId = item1.CageId,
                                AnimalIds = keyAdd1
                            };
                            List<AnimalCageTaskNormalId> keyAdd2s = new();
                            keyAdd2s.Add(keyAdd2);
                            var task = await AddTaskSimply(new()
                            {
                                TaskTypeId = 3,
                                TaskTypeName = "Health",
                                TaskName = "Chăm sóc",
                                TimeStart = TimeOnly.FromDateTime(item2),
                                AnimalTaskNormalsId = keyAdd2s
                            });
                            t.Add((task, DateOnly.FromDateTime(item2)));
                        }
                    }
                    tt.Add(t);
                }
                List<List<Schedule>> ss = new();
                foreach (var item in key.AccountIds)
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
                            if (sTemp.Count != 0) ssTemp.Add(sTemp);
                        }
                        if(date.Day % 2 == 1)
                        {
                            ssTemp.Reverse();
                        }
                        int count = 0;
                        foreach (var item1 in ttTemp)
                        {
                            if (count >= ssTemp.Count) count = 0;
                            foreach (var item2 in item1)
                            {
                                
                                item2.Item1.ScheduleId = ssTemp[count][0].Id;
                                await repo.UpdateAsync(item2.Item1);
                            }
                            count++;
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
                bool isEccessDate = false;
                while(timeStart <= (TimeOnly)key.TimeEndInDay)
                {
                    result.Add(date.ToDateTime(timeStart));
                    var newTimeSpan = timeStart.ToTimeSpan() + new TimeSpan(hours: periodOfTime.Hours, 0, 0);
                    if (newTimeSpan.Ticks < 0 || newTimeSpan.Ticks > TimeOnly.MaxValue.Ticks)
                    {
                        isEccessDate = true;
                        break; 
                    }
                    timeStart = TimeOnly.FromTimeSpan(newTimeSpan);
                    if (periodOfTime.Days > 0)
                    {
                        break;
                    }
                }
                if(timeStart > (TimeOnly)key.TimeEndInDay || isEccessDate)
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
        private List<DateTime> GetListDateTimeDayCleaning(DateOnly fromDate, DateOnly toDate, TimeInterval key)
        {
            List<DateTime> result = new List<DateTime>();
            int day = key.Day_Interval.Value.Days;
            for (DateOnly date = fromDate; date <= toDate; date = date.AddDays(day))
            {
                foreach(var time in key.Times)
                {
                    result.Add(date.ToDateTime(time));
                }
            }
            return result;
        }
        private async System.Threading.Tasks.Task<BO.Models.Task> AddTaskSimply(TaskAdd key)
        {
            try
            {
                var task = await repo.AddTask(key);
                if(key.TaskTypeName == "Meal")
                {
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
                }
                else if(key.TaskTypeName == "Cleaning")
                {
                    foreach (var item1 in key.AnimalTaskCleaningsId)
                    {
                        List<(int?, TaskCleaningAdd?)> animalCageIds = new();
                        foreach (var item2 in item1.AnimalCleaningIds)
                        {
                            animalCageIds.Add((await animalCageRepo.GetAnimalCageIdByCageIdAndAnimalId(item1.CageId, item2.AnimalId), item2.TaskCleaning));
                        }
                        foreach (var item3 in animalCageIds)
                        {
                            if (item3.Item1 != null)
                            {
                                var animalAssign = await animalAssignRepo.AddAnimalAssign(task.Id, (int)item3.Item1);
                                await taskCleaningRepo.AddTaskCleaning(animalAssign.Id, item3.Item2);
                            }
                        }
                    }
                }
                else if (key.TaskTypeName == "Health")
                {
                    foreach (var item1 in key.AnimalTaskNormalsId)
                    {
                        List<int?> animalCageIds = new();
                        foreach (var item2 in item1.AnimalIds)
                        {
                            animalCageIds.Add(await animalCageRepo.GetAnimalCageIdByCageIdAndAnimalId(item1.CageId, item2.AnimalId));
                        }
                        foreach (var item3 in animalCageIds)
                        {
                            if (item3 != null)
                            {
                                var animalAssign = await animalAssignRepo.AddAnimalAssign(task.Id, (int)item3);
                                await healthTaskRepo.AddHealthTask(animalAssign.Id);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item1 in key.AnimalTaskNormalsId)
                    {
                        List<int?> animalCageIds = new();
                        foreach (var item2 in item1.AnimalIds)
                        {
                            animalCageIds.Add(await animalCageRepo.GetAnimalCageIdByCageIdAndAnimalId(item1.CageId, item2.AnimalId));
                        }
                        foreach (var item3 in animalCageIds)
                        {
                            if (item3 != null)
                            {
                                var animalAssign = await animalAssignRepo.AddAnimalAssign(task.Id, (int)item3);
                            }
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
        private async Task<bool> CheckAutoValidate(DateOnly fromDate, DateOnly toDate, List<int> listStaffId)
        {
            List<Account> accounts = new List<Account>();
            foreach (var item in listStaffId)
            {
                accounts.Add(accountRepo.GetById(item));
            }
            List<Schedule> schedules = new List<Schedule>();
            foreach (var item in accounts)
            {
                schedules.AddRange(await scheduleRepo.GetListScheduleByAccountId(item.Id));
            }
            for (DateOnly date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                if(schedules.FirstOrDefault(l=>l.Date == date) == null) 
                    return false;
            }
            return true;
        }
        private async Task<(bool,int?,DateOnly?)> CheckTaskDuplicateStaff(DateOnly fromDate, DateOnly toDate, List<int> listStaffId, int taskTypeId)
        {
            List<Account> accounts = new List<Account>();
            foreach (var item in listStaffId)
            {
                accounts.Add(accountRepo.GetById(item));
            }
            List<(Schedule,int)> schedules = new List<(Schedule, int)>();
            foreach (var item in accounts)
            {
                var schedulesTemp = await scheduleRepo.GetListScheduleByAccountIdByDate(item.Id,fromDate,toDate);
                foreach(var scheduleTemp in schedulesTemp)
                {
                    schedules.Add((scheduleTemp, item.Id));
                }
            }
            foreach(var schedule in schedules)
            {
                if ((await repo.GetListTaskByScheduleId(schedule.Item1.Id)).FindAll(l => l.TaskTypeId == taskTypeId).Count > 0)
                {
                    return (false,schedule.Item2,schedule.Item1.Date);
                }
            }
            return (true,null,null);
        }
        private async Task<bool> CheckTaskDuplicate(DateOnly date, int animalId, int taskTypeId)
        {
            AnimalCage animalCage = await animalCageRepo.GetAnimalCageCurrentByAnimalId(animalId);
            List<Schedule> schedules = (await scheduleRepo.GetAllAsync()).FindAll(l => l.Date == date);
            foreach(var schedule in schedules)
            {
                var tasks = (await repo.GetListTaskByScheduleId(schedule.Id)).FindAll(l=>l.TaskTypeId == taskTypeId);
                foreach(var task in tasks)
                {
                    var animalAssigns = await animalAssignRepo.GetListAnimalAssignByTaskId(task.Id);
                    var animalAssign = animalAssigns.FirstOrDefault(l => l.AnimalCageId == animalCage.Id);
                    if(animalAssign != null) return false;
                }
            }
            return true;
        }
    }
}
