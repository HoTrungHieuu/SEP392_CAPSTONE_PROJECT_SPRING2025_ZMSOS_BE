using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositoyr
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        public Task<List<Schedule>?> GetListScheduleByAccountId(int accountId);
        public Task<List<Schedule>?> GetListScheduleByAccountIdByDate(int accountId, DateOnly fromDate, DateOnly toDate);
        public Task<Schedule> AddSchedule(ScheduleAdd key);
        public System.Threading.Tasks.Task AddScheduleAuto(ScheduleAutoAdd key);
        public Task<Schedule?> UpdateSchedule(ScheduleUpdate key);
        public Task<Schedule?> TranferSchedule(int id, int accountId);
        public System.Threading.Tasks.Task DeleteSchedule(int scheduleId);
        public System.Threading.Tasks.Task DisableSchedule(int scheduleId);
        public ScheduleView ConvertScheduleIntoScheduleView(Schedule schedule, UserView user);
    }
}
