using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repository
{
    public class TaskRepository : GenericRepository<BO.Models.Task>, ITaskRepository
    {
        public TaskRepository()
        {
        }
        public async Task<List<BO.Models.Task>?> GetListTaskByScheduleId(int scheduleId)
        {
            try
            {
                var tasks = (await GetAllAsync()).FindAll(l => l.ScheduleId == scheduleId).FindAll(l=>l.Status != "Deleted");
                return tasks;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<BO.Models.Task> AddTask(TaskAdd key)
        {
            try
            {
                BO.Models.Task task = new()
                {
                    TaskTypeId = key.TaskTypeId,
                    ScheduleId = key.ScheduleId,
                    TaskName = key.TaskName,
                    Description = key.Description,
                    TimeStart = key.TimeStart,
                    TimeFinish = null,
                    Status = "Not Start",
                };
                await CreateAsync(task);
                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<BO.Models.Task?> UpdateTask(TaskUpdate key)
        {
            try
            {
                BO.Models.Task task = GetById(key.Id);
                if (task == null)
                {
                    return null;
                }
                task.ScheduleId = key.ScheduleId;
                task.TaskName = key.TaskName;
                task.Description = key.Description;
                task.TimeStart = key.TimeStart;
                await UpdateAsync(task);
                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<BO.Models.Task?> UpdateTaskStaff(TaskStaffUpdate key)
        {
            try
            {
                BO.Models.Task task = GetById(key.Id);
                if (task == null)
                {
                    return null;
                }
                task.Note = key.Note;
                task.Status = "Finish";
                task.TimeFinish = TimeOnly.FromDateTime(DateTime.Now);
                await UpdateAsync(task);
                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<BO.Models.Task?> StartTask(int id)
        {
            try
            {
                BO.Models.Task task = GetById(id);
                if (task == null)
                {
                    return null;
                }
                task.Status = "In Progressing";
                await UpdateAsync(task);
                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TaskView ConvertTaskIntoTaskView(BO.Models.Task task, List<AnimalCageTask> animalCageTasks, List<AnimalCageTaskCleaning> animalCageTaskCleanings, List<AnimalCageTaskNormal> animalCageTaskNormals, TaskTypeView taskType)
        {
            try
            {
                TaskView result = new TaskView();
                result.ConvertTaskIntoTaskView(task, animalCageTasks,animalCageTaskCleanings,animalCageTaskNormals, taskType);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
