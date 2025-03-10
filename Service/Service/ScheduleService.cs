using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ScheduleService : IScheduleService
    {
        public IScheduleRepository repo;
        public IUserRepository userRepo;
        public IAccountRepository accountRepo;
        public IObjectViewService objectViewService;
        public ScheduleService(IScheduleRepository repo, IUserRepository userRepo,IAccountRepository accountRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.accountRepo = accountRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListScheduleByAccountId(int accountId)
        {
            try
            {
                var schedules = await repo.GetListScheduleByAccountId(accountId);
                if (schedules == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListScheduleView(schedules);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Schedules",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GetScheduleById(int id)
        {
            try
            {
                var schedule = repo.GetById(id);
                if (schedule == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                UserView user = new();
                user.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)schedule.AccountId));
                var result = await objectViewService.GetScheduleView(schedule);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Schedule",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddSchedule(ScheduleAdd key)
        {
            try
            {
                var schedule = await repo.AddSchedule(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddScheduleAuto(ScheduleAutoAdd key)
        {
            try
            {
                if(key.FromDate > key.ToDate)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Date is Invalid",
                    };
                }
                var account = accountRepo.GetById(key.AccountId);
                if(account == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Account Not Exist",
                    };
                }
                await repo.AddScheduleAuto(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> UpdateSchedule(ScheduleUpdate key)
        {
            try
            {
                var schedule = await repo.UpdateSchedule(key);
                if (schedule == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
