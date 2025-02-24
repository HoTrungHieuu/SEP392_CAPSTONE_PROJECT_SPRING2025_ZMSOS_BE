using BO.Models;
using DAO.AddModel;
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
                var tasks = (await GetAllAsync()).FindAll(l => l.ScheduleId == scheduleId);
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
                    ScheduleId = key.ScheduleId,
                    TaskName = key.TaskName,
                    Description = key.Description,
                    Note = key.Note,
                    TimeStart = key.TimeStart,
                    TimeEnd = key.TimeEnd,
                    Status = ""
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
                task.TaskName = key.TaskName;
                task.Description = key.Description;
                task.Note = key.Note;
                task.TimeStart = key.TimeStart;
                task.TimeEnd = key.TimeEnd;
                await UpdateAsync(task);
                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<TaskView> ConvertListTaskIntoListTaskView(List<BO.Models.Task> tasks)
        {
            try
            {
                List<TaskView> result = new();
                foreach (var task in tasks)
                {
                    result.Add(ConvertTaskIntoTaskView(task));
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TaskView ConvertTaskIntoTaskView(BO.Models.Task task)
        {
            try
            {
                TaskView result = new TaskView();
                result.ConvertTaskIntoTaskView(task);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
