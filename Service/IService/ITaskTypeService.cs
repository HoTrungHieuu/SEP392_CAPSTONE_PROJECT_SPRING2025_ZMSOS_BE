using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ITaskTypeService
    {
        public Task<ServiceResult> GetListTaskType();
        public Task<ServiceResult> GetTaskTypeById(int id);
        public Task<ServiceResult> AddTaskType(TaskTypeAdd key);
        public Task<ServiceResult> UpdateTaskType(TaskTypeUpdate key);
    }
}
