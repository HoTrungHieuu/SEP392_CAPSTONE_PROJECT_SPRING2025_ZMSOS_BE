using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Microsoft.Identity.Client;
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
    public class CageService : ICageService
    {
        public ICageRepository repo;
        public IZooAreaRepository areaRepo;
        public IObjectViewService objectViewService;
        public IAnimalCageRepository animalCageRepo;
        public IAnimalRepository animalRepo;
        public IIncompatibleAnimalTypeRepository incompatibleAnimalTypeRepo;
        public IZooAreaRepository zooAreaRepo;
        public ITeamRepository teamRepo;
        public ILeaderAssignRepository leaderAssignRepo;
        public IAccountRepository accountRepo;
        public INotificationRepository notificationRepo;
        private readonly WebSocketHandler wsHandler;
        public CageService(ICageRepository repo, IZooAreaRepository areaRepo, IObjectViewService objectViewService, IAnimalCageRepository animalCageRepo, IAnimalRepository animalRepo, IIncompatibleAnimalTypeRepository incompatibleAnimalTypeRepo, IZooAreaRepository zooAreaRepo, ITeamRepository teamRepo, ILeaderAssignRepository leaderAssignRepo, IAccountRepository accountRepo, INotificationRepository notificationRepo, WebSocketHandler wsHandler)
        {
            this.repo = repo;
            this.areaRepo = areaRepo;
            this.objectViewService = objectViewService;
            this.animalCageRepo = animalCageRepo;
            this.animalRepo = animalRepo;
            this.incompatibleAnimalTypeRepo = incompatibleAnimalTypeRepo;
            this.zooAreaRepo = zooAreaRepo;
            this.teamRepo = teamRepo;
            this.leaderAssignRepo = leaderAssignRepo;
            this.accountRepo = accountRepo;
            this.notificationRepo = notificationRepo;
            this.wsHandler = wsHandler;
        }
        public async Task<ServiceResult> GetListCage()
        {
            try
            {
                var cages = await repo.GetListCage();
                if (cages == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListCageView(cages);
                foreach (var item in result)
                {
                    var animalCage = await animalCageRepo.GetListAnimalCageHistoryByCageId((int)item.Id);
                    item.HistoryCount = animalCage.Count;
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Cages",
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
        public async Task<ServiceResult> GetListCageSuitable(int animalId)
        {
            try
            {
                var animalResult = animalRepo.GetById(animalId);
                if (animalResult == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Animal Not Found",
                    };
                }
                var cages = await repo.GetListCage();
                List<Cage> cageResult = new List<Cage>();
                foreach(var cage in cages)
                {
                    var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cage.Id);
                    List<Animal> animals = new List<Animal>();
                    foreach (var animalCage in animalCages)
                    {
                        animals.Add(animalRepo.GetById(animalCage.AnimalId));
                    }
                    bool check = false;
                    foreach (var animal in animals)
                    {
                        if (await incompatibleAnimalTypeRepo.CheckIncompatibleAnimalType((int)animal.AnimalTypeId, (int)animalResult.AnimalTypeId))
                        {
                            check = true;
                            break;
                        }
                    }
                    if (!check)
                    {
                        cageResult.Add(cage);
                    }

                }
                var result = await objectViewService.GetListCageView(cageResult);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Cages",
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
        public async Task<ServiceResult> GetListCageSearching(CageSearch<CageView> key)
        {
            try
            {
                var cages = await repo.GetListCage();
                if (cages == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListCageView(cages);
                if (key.ZooAreaId != null)
                {
                    result = result.FindAll(l => l.ZooArea.Id == key.ZooAreaId);
                }
                if (key.Name != null)
                {
                    result = result.FindAll(l => l.Name == key.Name);
                }
                if (key.Sorting?.PropertySort == "ZooAreaId")
                {
                    if (key.Sorting.IsAsc)
                    {
                        result.OrderBy(l => l.ZooArea.Id);
                    }
                    else
                    {
                        result.OrderByDescending(l => l.ZooArea.Id);
                    }
                }
                else if (key.Sorting?.PropertySort == "Name")
                {
                    if (key.Sorting.IsAsc)
                    {
                        result.OrderBy(l => l.Name);
                    }
                    else
                    {
                        result.OrderByDescending(l => l.Name);
                    }
                }
                int? totalNumberPaging = null;
                if (key.Paging != null)
                {
                    Paging<CageView> paging = new();
                    totalNumberPaging = paging.MaxPageNumber(result, key.Paging.PageSize);
                    result = paging.PagingList(result, key.Paging.PageSize, key.Paging.PageNumber);   
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
        public async Task<ServiceResult> GetListCageByZooAreaId(int zooAreaId)
        {
            try
            {
                var zooArea = areaRepo.GetById(zooAreaId);
                if (zooArea == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "ZooArea Not Found!",
                    };
                }
                var cages = await repo.GetListCageByAreaId(zooAreaId);
                if (cages == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListCageView(cages);
                foreach (var item in result)
                {
                    var animalCage = await animalCageRepo.GetListAnimalCageHistoryByCageId((int)item.Id);
                    item.HistoryCount = animalCage.Count;
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Cages",
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
        public async Task<ServiceResult> GetCageById(int id)
        {
            try
            {
                var cage = repo.GetById(id);
                if (cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetCageView(cage);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Cages",
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
        public async Task<ServiceResult> GetCageHistoryById(int id)
        {
            try
            {
                var animalCages = await animalCageRepo.GetListAnimalCageHistoryByCageId(id);
                List<CageHistory> result = new();
                foreach (var animalCage in animalCages)
                {
                    result.Add(new()
                    {
                        Animal = await objectViewService.GetAnimalView(animalRepo.GetById(animalCage.AnimalId)),
                        FromDate = animalCage?.FromDate,
                        ToDate = animalCage?.ToDate,
                    });
                }

                return new ServiceResult
                {
                    Status = 200,
                    Message = $"{result.Count} Total History",
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
        public async Task<ServiceResult> AddCage(CageAdd key)
        {
            try
            {
                var cages = (await repo.GetListCage()).FindAll(l => l.Name.ToLower() == key.Name.ToLower());
                if (cages.Count > 0)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Cage Name is Exist",
                    };
                }
                var cage = await repo.AddCage(key);
                var result = await objectViewService.GetCageView(cage);

                var zooArea = zooAreaRepo.GetById(cage.ZooAreaId);
                if(zooArea != null)
                {
                    var team = await teamRepo.GetTeamByZooAreaId(zooArea.Id);
                    if(team != null)
                    {
                        var leaderAssign = await leaderAssignRepo.GetLeaderAssignByTeamId(team.Id);
                        if(leaderAssign != null)
                        {
                            var account = accountRepo.GetById(leaderAssign.LeaderId);
                            if(account != null)
                            {
                                await notificationRepo.AddNotification(new()
                                {
                                    AccountId = account.Id,
                                    Content = $"Chuồng {cage.Name} đã được thêm vào khu vực của bạn"
                                });
                                await wsHandler.SendMessageAsync(account.Id);
                            }
                        }
                    }
                }
                

                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
                    Data = cage
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
        public async Task<ServiceResult> UpdateCage(CageUpdate key)
        {
            try
            {
                var cages = (await repo.GetListCage()).FindAll(l => l.Id != key.Id && l.Name.ToLower() == key.Name.ToLower());
                if (cages.Count > 0)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Cage Name is Exist",
                    };
                }
                var cage = await repo.UpdateCage(key);
                if (cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                var result = await objectViewService.GetCageView(cage);

                var zooArea = zooAreaRepo.GetById(cage.ZooAreaId);
                if (zooArea != null)
                {
                    var team = await teamRepo.GetTeamByZooAreaId(zooArea.Id);
                    if (team != null)
                    {
                        var leaderAssign = await leaderAssignRepo.GetLeaderAssignByTeamId(team.Id);
                        if (leaderAssign != null)
                        {
                            var account = accountRepo.GetById(leaderAssign.LeaderId);
                            if (account != null)
                            {
                                await notificationRepo.AddNotification(new()
                                {
                                    AccountId = account.Id,
                                    Content = $"Chuồng {cage.Name} đã được thêm vào khu vực của bạn"
                                });
                                await wsHandler.SendMessageAsync(account.Id);
                            }
                        }
                    }
                }             

                return new ServiceResult
                {   
                    Status = 200,
                    Message = "Update Success",
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
        public async Task<ServiceResult> DisableCage(List<int> cageIds)
        {
            try
            {
                cageIds = cageIds.Distinct().ToList();
                List<int> unsucessIds = new List<int>();
                foreach(int cageId in cageIds)
                {
                    var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cageId);
                    List<Animal> animals = new List<Animal>();
                    foreach (var animalCage in animalCages)
                    {
                        animals.Add(animalRepo.GetById(animalCage.AnimalId));
                    }
                    if(animals.Count > 0)
                    {
                        unsucessIds.Add(cageId);
                    }
                    else
                    {
                        if((await repo.DisableCage(cageId))== 0)
                            unsucessIds.Add(cageId);
                    }
                }
                
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Disable Success with id unsuccess {unsucessIds}",
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
        public async Task<ServiceResult> DeleteCage(int id)
        {
            try
            {
                var cage = repo.GetById(id);
                if (cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                await repo.RemoveAsync(cage);
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
