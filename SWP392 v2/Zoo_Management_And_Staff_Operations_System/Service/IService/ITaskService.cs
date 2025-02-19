using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ITaskService
    {
        public Task<ServiceResult> GetListTaskByScheduleId(int scheduleId);
        public Task<ServiceResult> GetTaskById(int id);
        public Task<ServiceResult> AddTask(TaskAdd key);
        public Task<ServiceResult> UpdateTask(TaskUpdate key);
    }
}
