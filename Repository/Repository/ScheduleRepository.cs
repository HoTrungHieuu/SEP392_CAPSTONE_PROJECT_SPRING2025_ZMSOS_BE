using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
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
        public async Task<List<Schedule>?> GetListSchedule()
        {
            try
            {
                var schedules = (await GetAllAsync()).FindAll(l => l.Status != "Deleted");
                return schedules;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Schedule>?> GetListScheduleByAccountId(int accountId)
        {
            try
            {
                var schedules = (await GetListSchedule())?.FindAll(l => l.AccountId == accountId);
                return schedules;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Schedule>?> GetListScheduleByAccountIdByDate(int accountId, DateOnly fromDate, DateOnly toDate)
        {
            try
            {
                var schedules = (await GetListSchedule())?.FindAll(l => l.AccountId == accountId && l.Date>= fromDate && l.Date<=toDate);
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
                var schedule = (await GetListScheduleByAccountId((int)key.AccountId)).FirstOrDefault(l => l.Date == key.Date);
                if (schedule != null) return null;
                schedule = new()
                {
                    AccountId = key.AccountId,
                    Date = key.Date,
                    Note = key.Note,
                    CreatedDate = VietNamTime.GetVietNamTime(),
                    Status = "Pending"
                };
                await CreateAsync(schedule);
                return schedule;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task AddScheduleAuto(ScheduleAutoAdd key)
        {
            try
            {
                for (DateOnly date = (DateOnly)key.FromDate; date <= key.ToDate; date = date.AddDays(1))
                {
                    if(Day.CheckDateOfWeek(date, key.DayOfWeek) && key.DateExclution!=null && !key.DateExclution.Contains(date))
                    {
                        await AddSchedule(new ScheduleAdd()
                        {
                            AccountId = key.AccountId,
                            Date = date,
                            Note = null,
                        });
                    }
                }
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
                schedule.Note = key.Note;
                schedule.UpdatedDate = VietNamTime.GetVietNamTime();
                schedule.Status = key.Status;
                await UpdateAsync(schedule);
                return schedule;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Schedule?> TranferSchedule(int id, int accountId)
        {
            try
            {
                Schedule schedule = GetById(id);
                if (schedule == null)
                {
                    return null;
                }
                schedule.AccountId = accountId;
                await UpdateAsync(schedule);
                return schedule;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task DeleteSchedule(int scheduleId)
        {
            try
            {
                var schedule = GetById(scheduleId);
                if (schedule == null) return;
                await RemoveAsync(schedule);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task DisableSchedule(int scheduleId)
        {
            try
            {
                var schedule = GetById(scheduleId);
                if (schedule == null) return;
                if(schedule.Status != "Finished")
                {
                    schedule.Status = "Deleted";
                    await UpdateAsync(schedule);
                }
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
