using BO.Models;
using DAO.AddModel;
using DAO.DeleteModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Microsoft.Identity.Client;
using Nest;
using Repository.IRepository;
using Repository.IRepositoyr;
using Service.IService;
using Service.Other;
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
        public IMemberAssignRepository memberAssignRepo;
        public ITeamRepository teamRepo;
        public INotificationRepository notificationRepo;
        private readonly WebSocketHandler wsHandler;
        public ScheduleService(IScheduleRepository repo, IUserRepository userRepo,IAccountRepository accountRepo, IObjectViewService objectViewService, IMemberAssignRepository memberAssignRepo, ITeamRepository teamRepo,INotificationRepository notificationRepo, WebSocketHandler wsHandler)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.accountRepo = accountRepo;
            this.objectViewService = objectViewService;
            this.memberAssignRepo = memberAssignRepo;
            this.teamRepo = teamRepo;
            this.notificationRepo = notificationRepo;
            this.wsHandler = wsHandler;
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
        public async Task<ServiceResult> GetListScheduleByAccountIdByDate(int accountId, DateOnly fromDate, DateOnly toDate)
        {
            try
            {
                if(fromDate >toDate)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Date invalid!",
                    };
                }
                var schedules = await repo.GetListScheduleByAccountIdByDate(accountId,fromDate,toDate);
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
        public async Task<ServiceResult> GetListScheduleByTeamIdByDate(int teamId, DateOnly fromDate, DateOnly toDate)
        {
            try
            {
                if (fromDate > toDate)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Date invalid!",
                    };
                }
                var team = teamRepo.GetById(teamId);
                if (team == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Team Not Found!",
                    };
                }
                var members = await memberAssignRepo.GetListMemberAssignByTeamId(teamId);
                List<AccountSchedule> result = new();
                foreach(var member in members)
                {
                    var account = accountRepo.GetById(member.MemberId);
                    var schedules = await repo.GetListScheduleByAccountIdByDate((int)member.MemberId, fromDate, toDate);
                    result.Add(new()
                    {
                        Account = await objectViewService.GetAccountView(account),
                        Schedules = await objectViewService.GetListScheduleView(schedules)
                    });
                }
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
        public async Task<ServiceResult> GetListScheduleLeaderByDate(DateOnly fromDate, DateOnly toDate)
        {
            try
            {
                if (fromDate > toDate)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Date invalid!",
                    };
                }
                var accounts = await accountRepo.GetListAccountLeader();
                List<AccountSchedule> result = new();
                foreach (var account in accounts)
                {
                    var schedules = await repo.GetListScheduleByAccountIdByDate((int)account.Id, fromDate, toDate);
                    result.Add(new()
                    {
                        Account = await objectViewService.GetAccountView(account),
                        Schedules = await objectViewService.GetListScheduleView(schedules)
                    });
                }
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
        public async Task<ServiceResult> GetListAccountSuitableTranfer(int teamId,int scheduleId)
        {
            try
            {
                var team = teamRepo.GetById(teamId);
                if(team == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Team Not Found!",
                    };
                }
                var schedule = repo.GetById(scheduleId);
                if (schedule == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Schedule Not Found!",
                    };
                }
                var members = (await memberAssignRepo.GetListMemberAssignByTeamId(team.Id)).FindAll(l=>l.MemberId != schedule.AccountId);
                List<Account> accounts = new();
                foreach(var member in members)
                {
                    var schedules = await repo.GetListScheduleByAccountIdByDate((int)member.MemberId, (DateOnly)schedule.Date, (DateOnly)schedule.Date);
                    if (schedules.Count == 0)
                    {
                        accounts.Add(accountRepo.GetById(member.MemberId));
                    }
                }
                var result = await objectViewService.GetListAccountView(accounts);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Accounts",
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
                await notificationRepo.AddNotification(new NotificationAdd()
                {
                    AccountId = (int)key.AccountId,
                    Content = $"Bạn đã nhận được lịch đi làm vào {key.Date}"
                });
                await wsHandler.SendMessageAsync((int)key.AccountId);
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
                await notificationRepo.AddNotification(new NotificationAdd()
                {
                    AccountId = (int)key.AccountId,
                    Content = $"Bạn đã nhận được lịch đi làm từ {key.FromDate} đến {key.ToDate}"
                });
                await wsHandler.SendMessageAsync((int)key.AccountId);
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
                var schedule = repo.GetById(key.Id);
                if (schedule == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                if(schedule.Date > DateOnly.FromDateTime(VietNamTime.GetVietNamTime()))
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Schedule is in furture can not update"
                    };
                }
                if (schedule.Status != null && schedule.Status != "Pending")
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Schedule is Absent or Present can not update"
                    };
                }
                schedule = await repo.UpdateSchedule(key);
                
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
        public async Task<ServiceResult> TranferSchedule(ScheduleTranfer key)
        {
            try
            {
                var account = accountRepo.GetById(key.AccountId);
                if (account == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Account Not Found"
                    };
                }
                var schedule = repo.GetById(key.Id);
                if(schedule == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Schedule Not Found"
                    };
                }
                if(schedule.Status == "Finished")
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Schedule Finished"
                    };
                }
                var schedules = await repo.GetListScheduleByAccountIdByDate(key.AccountId, (DateOnly)schedule.Date, (DateOnly)schedule.Date);
                if(schedules.Count > 0)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = $"Account Had Schedule in {schedule.Date}"
                    };
                }
                await repo.TranferSchedule(key.Id, key.AccountId);
                
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Tranfer Success",
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
        public async Task<ServiceResult> DeleteSchedule(ScheduleDelete key)
        {
            try
            {
                if(key.FromDate> key.ToDate)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "from date and to date invalid",
                    };
                }
                var schedules = (await repo.GetListScheduleByAccountId(key.AccountId)).FindAll(l => l.Date >= key.FromDate && l.Date <= key.ToDate);
                foreach(var schedule in schedules)
                {
                    await repo.DeleteSchedule(schedule.Id);
                }
                
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Delete Success",
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
        public async Task<ServiceResult> DisableSchedule(ScheduleDelete key)
        {
            try
            {
                if (key.FromDate > key.ToDate)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "from date and to date invalid",
                    };
                }
                var schedules = await repo.GetListScheduleByAccountIdByDate(key.AccountId,key.FromDate,key.ToDate);
                foreach (var schedule in schedules)
                {
                    await repo.DisableSchedule(schedule.Id);
                }

                return new ServiceResult
                {
                    Status = 200,
                    Message = "Delete Success",
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
