using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.SearchModel;
using DAO.ViewModel;
using Repository.IRepository;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Service.Service
{
    public class ApplicationService : IApplicationService
    {
        public IApplicationRepository repo;
        public IApplicationTypeRepository applicationTypeRepo;
        public IUserRepository userRepo;
        public IAccountRepository accountRepo;
        public ITeamRepository teamRepo;
        public IMemberAssignRepository memberAssignRepo;
        public ILeaderAssignRepository leaderAssignRepo;
        public IObjectViewService objectViewService;
        public ApplicationService(IApplicationRepository repo, IApplicationTypeRepository applicationTypeRepo,IAccountRepository accountRepo, ITeamRepository teamRepo, IMemberAssignRepository memberAssignRepo, ILeaderAssignRepository leaderAssignRepo, IUserRepository userRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.applicationTypeRepo = applicationTypeRepo;
            this.userRepo = userRepo;
            this.accountRepo = accountRepo;
            this.teamRepo = teamRepo;
            this.memberAssignRepo = memberAssignRepo;
            this.leaderAssignRepo = leaderAssignRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListApplicationBySenderId(int senderId)
        {
            try
            {
                var applications = await repo.GetListApplcationBySenderId(senderId);
                if (applications == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListApplicationView(applications);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Applications",
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
        public async Task<ServiceResult> GetListApplicationBySenderIdSearch(int senderId, ApplicationSearch<ApplicationView> key)
        {
            try
            {
                var applications = await repo.GetListApplcationBySenderId(senderId);
                if (applications == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListApplicationView(applications);
                int? totalNumberPaging = null;
                if (key.Paging != null)
                {
                    Paging<ApplicationView> paging = new();
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
        public async Task<ServiceResult> GetListApplicationByRecieverId(int recieverId)
        {
            try
            {
                var applications = await repo.GetListApplcationByRecieverId(recieverId);
                if (applications == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListApplicationView(applications);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Applications",
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
        public async Task<ServiceResult> GetListApplicationByRecieverIdSearch(int recieverId, ApplicationSearch<ApplicationView> key)
        {
            try
            {
                var applications = await repo.GetListApplcationByRecieverId(recieverId);
                if (applications == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListApplicationView(applications);
                int? totalNumberPaging = null;
                if (key.Paging != null)
                {
                    Paging<ApplicationView> paging = new();
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
        public async Task<ServiceResult> GetApplicationById(int id)
        {
            try
            {
                var application = repo.GetById(id);
                if (application == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetApplicationView(application);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Application",
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
        public async Task<ServiceResult> AddApplication(ApplicationAdd key)
        {
            try
            {
                var account = accountRepo.GetById(key.SenderId);
                if(account == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Sender Not Found",
                    };
                }
                int? recieverId = null;
                if(account.RoleId == 3)
                {
                    recieverId = (await accountRepo.GetAccountManager())?.Id;
                }
                else if(account.RoleId == 4)
                {
                    var member = await memberAssignRepo.GetMemberAssignByAccountId(key.SenderId);
                    var team = teamRepo.GetById(member.TeamId);
                    var leader = await leaderAssignRepo.GetLeaderAssignByTeamId(team.Id);
                    recieverId = leader?.LeaderId;
                }
                if(recieverId == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Reciever Not Found",
                    };
                }
                var application = await repo.AddApplication(key, (int)recieverId);
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
