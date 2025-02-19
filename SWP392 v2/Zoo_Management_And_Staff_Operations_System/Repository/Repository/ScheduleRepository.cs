using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository()
        {
        }
        public async Task<List<Schedule>?> GetListScheduleByAccountId(int accountId)
        {
            try
            {
                var schedules = (await GetAllAsync()).FindAll(l => l.AccountId == accountId);
                return schedules;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Schedule> AddSchedule(ScheduleAdd key)
        {
            try
            {
                Schedule schedule = new()
                {
                    AccountId = key.AccountId,
                    Date = key.Date,
                    Note = key.Note,
                    Status = ""
                };
                await CreateAsync(schedule);
                return schedule;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Schedule?> UpdateSchedule(ScheduleUpdate key)
        {
            try
            {
                Schedule schedule = GetById(key.Id);
                if(schedule == null )
                {
                    return null;
                }
                schedule.Date = key.Date;
                schedule.Note = key.Note;
                await UpdateAsync(schedule);
                return schedule;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ScheduleView> ConvertListScheduleIntoListScheduleView(List<Schedule> schedules, List<UserView> users)
        {
            try
            {
                List<ScheduleView> result = new();
                for (int i = 0; i < schedules.Count; i++)
                {
                    result.Add(ConvertScheduleIntoScheduleView(schedules[i], users[i]));
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ScheduleView ConvertScheduleIntoScheduleView(Schedule schedule, UserView user)
        {
            try
            {
                ScheduleView result = new ScheduleView();
                result.ConvertSchedualIntoSchedualView(schedule, user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
