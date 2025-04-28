using DAO.AddModel;
using DAO.DeleteModel;
using DAO.OtherModel;
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
        public Task<ServiceResult> GetListScheduleByAccountIdByDate(int accountId, DateOnly fromDate, DateOnly toDate);
        public Task<ServiceResult> GetListScheduleByTeamIdByDate(int teamId, DateOnly fromDate, DateOnly toDate);
        public Task<ServiceResult> GetListScheduleLeaderByDate(DateOnly fromDate, DateOnly toDate);
        public Task<ServiceResult> GetListAccountSuitableTranfer(int teamId, int scheduleId);
        public Task<ServiceResult> GetScheduleById(int id);
        public Task<ServiceResult> AddSchedule(ScheduleAdd key);
        public Task<ServiceResult> AddScheduleAuto(ScheduleAutoAdd key);
        public Task<ServiceResult> FinishSchedule(ScheduleUpdate key);
        public Task<ServiceResult> TranferSchedule(ScheduleTranfer key);
        public Task<ServiceResult> DeleteSchedule(ScheduleDelete key);
        public Task<ServiceResult> DisableSchedule(ScheduleDelete key);
    }
}
