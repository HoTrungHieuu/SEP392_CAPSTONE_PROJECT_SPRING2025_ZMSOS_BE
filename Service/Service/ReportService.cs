using DAO.AddModel;
using DAO.OtherModel;
using DAO.SearchModel;
using DAO.ViewModel;
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
    public class ReportService : IReportService
    {
        public IReportRepository repo;
        public IReportAttachmentRepository attachmentRepo;
        public IUserRepository userRepo;
        public IAccountRepository accountRepo;
        public ITeamRepository teamRepo;
        public ILeaderAssignRepository leaderAssignRepo;
        public IMemberAssignRepository memberAssignRepo;
        public INotificationRepository notificationRepo;
        private readonly WebSocketHandler wsHandler;
        public IObjectViewService objectViewService;
        public ReportService(IReportRepository repo, IReportAttachmentRepository attachmentRepo, IUserRepository userRepo,IAccountRepository accountRepo,ITeamRepository teamRepo, ILeaderAssignRepository leaderAssignRepo, IMemberAssignRepository memberAssignRepo, IObjectViewService objectViewService, INotificationRepository notificationRepo, WebSocketHandler wsHandler)
        {
            this.repo = repo;
            this.attachmentRepo = attachmentRepo;
            this.userRepo = userRepo;
            this.accountRepo = accountRepo;
            this.teamRepo = teamRepo;
            this.leaderAssignRepo = leaderAssignRepo;
            this.memberAssignRepo = memberAssignRepo;
            this.objectViewService = objectViewService;
            this.notificationRepo = notificationRepo;
            this.wsHandler = wsHandler;
        }
        public async Task<ServiceResult> GetListReportBySenderId(int senderId)
        {
            try
            {
                var reports = await repo.GetListReportBySenderId(senderId);
                if (reports == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                
                var result = await objectViewService.GetListReportView(reports);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Reports",
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
        public async Task<ServiceResult> GetListReportBySenderIdSearch(int senderId, ReportSearch<ReportView> key)
        {
            try
            {
                var reports = await repo.GetListReportBySenderId(senderId);
                if (reports == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListReportView(reports);
                int? totalNumberPaging = null;
                if (key.Paging != null)
                {
                    Paging<ReportView> paging = new();
                    result = paging.PagingList(result, key.Paging.PageSize, key.Paging.PageNumber);
                    totalNumberPaging = paging.MaxPageNumber(result, key.Paging.PageSize);
                }
                if (totalNumberPaging == null) totalNumberPaging = 1;
                return new ServiceResult
                {
                    Status = 200,
                    Message = totalNumberPaging.ToString(),
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
        public async Task<ServiceResult> GetListReportByRecieverId(int recieverId)
        {
            try
            {
                var reports = await repo.GetListReportBySenderId(recieverId);
                if (reports == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListReportView(reports);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Reports",
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
        public async Task<ServiceResult> GetListReportByRecieverIdSearch(int recieverId, ReportSearch<ReportView> key)
        {
            try
            {
                var reports = await repo.GetListReportByRecieverId(recieverId);
                if (reports == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListReportView(reports);
                int? totalNumberPaging = null;
                if (key.Paging != null)
                {
                    Paging<ReportView> paging = new();
                    result = paging.PagingList(result, key.Paging.PageSize, key.Paging.PageNumber);
                    totalNumberPaging = paging.MaxPageNumber(result, key.Paging.PageSize);
                }
                if (totalNumberPaging == null) totalNumberPaging = 1;
                return new ServiceResult
                {
                    Status = 200,
                    Message = totalNumberPaging.ToString(),
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
        public async Task<ServiceResult> GetReportById(int id)
        {
            try
            {
                var report = repo.GetById(id);
                if (report == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetReportView(report);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Report",
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
        public async Task<ServiceResult> AddReport(ReportAdd key)
        {
            try
            {
                var account = accountRepo.GetById(key.SenderId);
                if (account == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Sender Not Found",
                    };
                }
                int? recieverId = null;
                if (account.RoleId == 3)
                {
                    recieverId = (await accountRepo.GetAccountManager())?.Id;
                }
                else if (account.RoleId == 4)
                {
                    var member = await memberAssignRepo.GetMemberAssignByAccountId((int)key.SenderId);
                    if (member == null)
                    {
                        return new ServiceResult
                        {
                            Status = 400,
                            Message = "Sender has no team",
                        };
                    }
                    var team = teamRepo.GetById(member.TeamId);
                    var leader = await leaderAssignRepo.GetLeaderAssignByTeamId(team.Id);
                    if (leader == null)
                    {
                        return new ServiceResult
                        {
                            Status = 400,
                            Message = "Sender has no team leader",
                        };
                    }
                    recieverId = leader?.LeaderId;
                }
                if (recieverId == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Reciever Not Found",
                    };
                }
                var report = await repo.AddReport(key,(int)recieverId);
                await notificationRepo.AddNotification(new NotificationAdd()
                {
                    AccountId = (int)recieverId,
                    Content = $"Bạn đã nhận được báo cáo từ {accountRepo.GetById(recieverId).Email}"
                });
                await wsHandler.SendMessageAsync((int)recieverId);
                foreach (var item in key.UrlFile)
                {
                    await attachmentRepo.CreateAsync(new()
                    {
                        ReportId = report.Id,
                        FileUrl = item,
                    });
                }
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
    }
}
