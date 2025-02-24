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
        public Task<Schedule> AddSchedule(ScheduleAdd key);
        public Task<Schedule?> UpdateSchedule(ScheduleUpdate key);
        public List<ScheduleView> ConvertListScheduleIntoListScheduleView(List<Schedule> schedules, List<UserView> users);
        public ScheduleView ConvertScheduleIntoScheduleView(Schedule schedule, UserView user);
    }
}
