using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TaskTypeRepository : GenericRepository<TaskType>, ITaskTypeRepository
    {
        public TaskTypeRepository()
        {
        }
        public async Task<List<TaskType>?> GetListTaskType()
        {
            try
            {
                var taskTypes = await GetAllAsync();
                return taskTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TaskType> AddTaskType(TaskTypeAdd key)
        {
            try
            {
                TaskType taskType = new()
                {
                    Name = key.Name,
                    Description = key.Description,
                };
                await CreateAsync(taskType);
                return taskType;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TaskType?> UpdateTaskType(TaskTypeUpdate key)
        {
            try
            {
                TaskType taskType = GetById(key.Id);
                if (taskType == null)
                {
                    return null;
                }
                taskType.Name = key.Name;
                taskType.Description = key.Description;
                await UpdateAsync(taskType);
                return taskType;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TaskTypeView ConvertTaskTypeIntoTaskTypeView(TaskType taskType)
        {
            try
            {
                TaskTypeView result = new TaskTypeView();
                result.ConvertTaskTypeIntoTaskTypeView(taskType);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
