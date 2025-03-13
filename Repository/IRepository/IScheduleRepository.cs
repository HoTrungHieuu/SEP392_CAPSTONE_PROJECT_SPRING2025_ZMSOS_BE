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
        public System.Threading.Tasks.Task AddScheduleAuto(ScheduleAutoAdd key);
        public Task<Schedule?> UpdateSchedule(ScheduleUpdate key);
        public ScheduleView ConvertScheduleIntoScheduleView(Schedule schedule, UserView user);
    }
}
