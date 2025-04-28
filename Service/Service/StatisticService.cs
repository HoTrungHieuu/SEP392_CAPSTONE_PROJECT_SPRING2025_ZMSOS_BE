using BO.Models;
using DAO.OtherModel;
using NPOI.SS.Formula.Functions;
using Repository.IRepository;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class StatisticService: IStatisticService
    {
        public IAccountRepository accountRepo;
        public ITeamRepository teamRepo;
        public IAnimalTypeRepository animalTypeRepo;
        public IAnimalRepository animalRepo;
        public IFlockRepository flockRepo;
        public ILeaderAssignRepository leaderAssignRepo;
        public IMemberAssignRepository memberAssignRepo;
        public IZooAreaRepository zooAreaRepo;
        public IUserRepository userRepo;
        public ICageRepository cageRepo;
        public IScheduleRepository scheduleRepo;
        public IObjectViewService objectViewService;
        public StatisticService(IAccountRepository accountRepo, ITeamRepository teamRepo, IAnimalTypeRepository animalTypeRepo, IAnimalRepository animalRepo, IFlockRepository flockRepo, ILeaderAssignRepository leaderAssignRepo, IMemberAssignRepository memberAssignRepo, IZooAreaRepository zooAreaRepo, IUserRepository userRepo, ICageRepository cageRepo, IScheduleRepository scheduleRepo, IObjectViewService objectViewService)
        {
            this.accountRepo = accountRepo;
            this.teamRepo = teamRepo;
            this.animalTypeRepo = animalTypeRepo;
            this.animalRepo = animalRepo;
            this.flockRepo = flockRepo;
            this.leaderAssignRepo = leaderAssignRepo;
            this.memberAssignRepo = memberAssignRepo;
            this.zooAreaRepo = zooAreaRepo;
            this.userRepo = userRepo;
            this.cageRepo = cageRepo;
            this.scheduleRepo = scheduleRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetStatistic()
        {
            try
            {
                Statistic statistic = new()
                {
                    AnimalStatistics = new(),
                    TeamStatistics = new(),
                    ZooAreaStatistics = new(),
                };
                statistic.TotalLeader = (await accountRepo.GetListAccountLeader()).Count;
                statistic.TotalStaff = (await accountRepo.GetListAccountStaff()).Count;
                var animalTypes = await animalTypeRepo.GetListAnimalType();
                statistic.TotalAnimalType = animalTypes.Count;
                foreach( var animalType in animalTypes)
                {
                    var animals = await animalRepo.GetListAnimalByAnimalTypeId(animalType.Id);
                    AnimalStatistic animalStatistic = new()
                    {
                        AnimalTypeName = animalType.VietnameseName,
                        TotalQuantity = 0
                    };
                    foreach(var animal in animals)
                    {
                        if(animal.Classify == "Individual")
                        {
                            animalStatistic.TotalQuantity += 1;
                        }
                        else
                        {
                            var flock = await flockRepo.GetFlockByAnimalId(animal.Id);
                            animalStatistic.TotalQuantity += (flock == null || flock.Quantity == null) ? 0 : (int)flock.Quantity;
                        }
                    }
                    statistic.AnimalStatistics.Add(animalStatistic);
                }
                var teams = await teamRepo.GetListTeam();
                statistic.TotalTeam = teams.Count;
                foreach(var team in teams)
                {
                    var leader = await leaderAssignRepo.GetLeaderAssignByTeamId(team.Id);
                    var staffs = await memberAssignRepo.GetListMemberAssignByTeamId(team.Id);
                    var zooArea = zooAreaRepo.GetById(team.ZooAreaId);
                    TeamStatistic teamStatistic = new()
                    {
                        TeamName = team.Name,
                        ZooAreaName = (zooArea == null) ? null : zooArea.Name,
                        LeaderName = (leader == null)? null : (await userRepo.GetUserByAccountId(accountRepo.GetById(leader.LeaderId).Id)).FullName,
                        TotalStaff = staffs.Count()
                    };
                    statistic.TeamStatistics.Add(teamStatistic);
                }
                var zooAreas = await zooAreaRepo.GetListZooArea();
                statistic.TotalZooArea = zooAreas.Count;
                foreach(var zooArea in zooAreas)
                {
                    var cages = await cageRepo.GetListCageByAreaId(zooArea.Id);
                    ZooAreaStatistic zooAreaStatistic = new()
                    {
                        Name = zooArea.Name,
                        TotalCage = cages.Count()
                    };
                    statistic.ZooAreaStatistics.Add(zooAreaStatistic);
                }

               
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Zoo Areas",
                    Data = statistic
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
        public async Task<ServiceResult> GetLeaderStatistic(int accountId,DateOnly fromDate, DateOnly toDate)
        {
            try
            {
                LeaderStatistic result = new();
                var account = accountRepo.GetById(accountId);
                if (account == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Account Not Found!",
                    };
                }

                var team = teamRepo.GetById((await leaderAssignRepo.GetLeaderAssignByAccountId(accountId))?.TeamId);
                if(team == null)
                {
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Statistic",
                        Data = result
                    };
                }

                var memberAssigns = await memberAssignRepo.GetListMemberAssignByTeamId(team.Id);
                foreach(var memberAssign in memberAssigns)
                {
                    var schedules = await scheduleRepo.GetListScheduleByAccountIdByDate((int)memberAssign.MemberId, fromDate, toDate);
                    result.ScheduleStatistic.Add(new()
                    {
                        Account = await objectViewService.GetAccountView(accountRepo.GetById((int)memberAssign.MemberId)),
                        TotalCurrentSchedule = schedules.FindAll(l => l.Status == "Finished").Count,
                        TotalSchedule = schedules.Count,
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
    }
}
