using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class TeamService : ITeamService
    {
        public ITeamRepository repo;
        public ILeaderAssignRepository leaderRepo;
        public IMemberAssignRepository memberRepo;
        public IAccountRepository accountRepo;
        public IObjectViewService objectViewService;
        public TeamService(ITeamRepository repo, ILeaderAssignRepository leaderRepo, IMemberAssignRepository memberRepo, IObjectViewService objectViewService, IAccountRepository accountRepo)
        {
            this.repo = repo;
            this.leaderRepo = leaderRepo;
            this.memberRepo = memberRepo;
            this.objectViewService = objectViewService;
            this.accountRepo = accountRepo;
        }
        public async Task<ServiceResult> GetListTeam()
        {
            try
            {
                var teams = await repo.GetListTeam();
                if (teams == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListTeamView(teams);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Teams",
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
        public async Task<ServiceResult> GetTeamById(int id)
        {
            try
            {
                var team = repo.GetById(id);
                if (team == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                TeamDetailView result = new()
                {
                    Team = await objectViewService.GetTeamView(team),
                    Leader = await objectViewService.GetLeaderAssignView(await leaderRepo.GetLeaderAssignByTeamId(team.Id)),
                    Members = await objectViewService.GetListMemberAssignView(await memberRepo.GetListMemberAssignByTeamId(team.Id)),
                };
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Team",
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
        public async Task<ServiceResult> GetTeamByAccountId(int accountId)
        {
            try
            {
                var account = accountRepo.GetById(accountId);
                if (account == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Account Not Found!",
                    };
                }
                var team = repo.GetById((await memberRepo.GetMemberAssignByAccountId(accountId)).TeamId);
                if (team == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                TeamDetailView result = new()
                {
                    Team = await objectViewService.GetTeamView(team),
                    Leader = await objectViewService.GetLeaderAssignView(await leaderRepo.GetLeaderAssignByTeamId(team.Id)),
                    Members = await objectViewService.GetListMemberAssignView(await memberRepo.GetListMemberAssignByTeamId(team.Id)),
                };
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Team",
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
        public async Task<ServiceResult> AddTeam(TeamAdd key)
        {
            try
            {
                if (key.ZooAreaId == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Zoo Area Not Null",
                    };
                }
                if(await repo.GetTeamByZooAreaId((int)key.ZooAreaId) != null)
                {
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Zoo Area had team",
                    };
                } 
                var team = await repo.AddTeam(key);
                var result = await objectViewService.GetTeamView(team);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
                    Data= result
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
        public async Task<ServiceResult> UpdateTeam(TeamUpdate key)
        {
            try
            {
                var team = await repo.UpdateTeam(key);
                if (team == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                var result = await objectViewService.GetTeamView(team);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Success",
                    Data = team
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
        public async Task<ServiceResult> GetUnassignedLeaderAccounts()
        {
            try
            {
                var accounts = (await accountRepo.GetAllAsync()).FindAll(l=>l.RoleId == 3);
                if (accounts == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                List<Account> accountResult = new();
                foreach(var account in accounts)
                {
                    var leaderAssign = await leaderRepo.GetLeaderAssignByAccountId(account.Id);
                    if(leaderAssign == null)
                    {
                        accountResult.Add(account);
                    }
                }
                var result = await objectViewService.GetListAccountView(accountResult);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "List Account",
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
        public async Task<ServiceResult> GetUnassignedStaffAccounts()
        {
            try
            {
                var accounts = (await accountRepo.GetAllAsync()).FindAll(l => l.RoleId == 4);
                if (accounts == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                List<Account> accountResult = new();
                foreach (var account in accounts)
                {
                    var memberAssign = await memberRepo.GetMemberAssignByAccountId(account.Id);
                    if (memberAssign == null)
                    {
                        accountResult.Add(account);
                    }
                }
                var result = await objectViewService.GetListAccountView(accountResult);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "List Account",
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
        public async Task<ServiceResult> GetLeaderByTeamId(int teamId)
        {
            try
            {
                var leader = await leaderRepo.GetLeaderAssignByTeamId(teamId);
                if (leader == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetLeaderAssignView(leader);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Leader",
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
        public async Task<ServiceResult> AddLeaderAssign(LeaderAssignAdd key)
        {
            try
            {
                var team = repo.GetById(key.TeamId);
                if (team == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Team Not Found"
                    };
                }
                var leader = await leaderRepo.GetLeaderAssignByTeamId((int)key.TeamId);
                if(leader != null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Leader Is Exist"
                    };
                }
                leader = await leaderRepo.AddLeaderAssign(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success"
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
        public async Task<ServiceResult> RemoveLeader(int accountId, int teamId)
        {
            try
            {
                var team = repo.GetById(teamId);
                if (team == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Team Not Found"
                    };
                }
                var leader = await leaderRepo.RemoveLeaderAssign(teamId,accountId);
                if (leader == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Leader Not In Team"
                    };
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Remove Success",
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
        public async Task<ServiceResult> GetListMemberByTeamId(int teamId)
        {
            try
            {
                var members = await memberRepo.GetListMemberAssignByTeamId(teamId);
                if (members == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListMemberAssignView(members);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Members",
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
        public async Task<ServiceResult> AddMemberAssign(MemberAssignAdd key)
        {
            try
            {
                var team = repo.GetById(key.TeamId);
                if (team == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Team Not Found"
                    };
                }
                if(team.CurrentQuantity >= team.MaxQuantity)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Team is full"
                    };
                }
                if(team.MaxQuantity - team.CurrentQuantity < key.AccountIds.Count)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Not Enough Position"
                    };
                }
                List<int> unsuccessId = new List<int>();
                foreach (int id in key.AccountIds)
                {
                    var member = await memberRepo.AddMemberAssign(key.TeamId, id);
                    if (member == null)
                    {
                        unsuccessId.Add(id);
                    }
                    else
                    {
                        team.CurrentQuantity += 1;
                    }
                }
                await repo.UpdateAsync(team);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success With Id UnSuccess",
                    Data = unsuccessId
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
        public async Task<ServiceResult> RemoveMember(int accountId, int teamId)
        {
            try
            {
                var team = repo.GetById(teamId);
                if (team == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Team Not Found"
                    };
                }
                var member = await memberRepo.RemoveMemberAssign(teamId, accountId);
                if (member == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Member Not In Team"
                    };
                }
                team.CurrentQuantity -= 1;
                await repo.UpdateAsync(team);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Remove Success",
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
