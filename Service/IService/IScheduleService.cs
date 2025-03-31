using DAO.AddModel;
using DAO.DeleteModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IScheduleService
    {
        public Task<ServiceResult> GetListScheduleByAccountId(int accountId);
        public Task<ServiceResult> GetScheduleById(int id);
        public Task<ServiceResult> AddSchedule(ScheduleAdd key);
        public Task<ServiceResult> AddScheduleAuto(ScheduleAutoAdd key);
        public Task<ServiceResult> UpdateSchedule(ScheduleUpdate key);
        public Task<ServiceResult> DeleteSchedule(ScheduleDelete key);
    }
}
