using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ITaskTypeRepository : IGenericRepository<TaskType>
    {
        public Task<List<TaskType>?> GetListTaskType();
        public Task<TaskType> AddTaskType(TaskTypeAdd key);
        public Task<TaskType?> UpdateTaskType(TaskTypeUpdate key);
        public TaskTypeView ConvertTaskTypeIntoTaskTypeView(TaskType taskType, StatusView? status);
    }
}
