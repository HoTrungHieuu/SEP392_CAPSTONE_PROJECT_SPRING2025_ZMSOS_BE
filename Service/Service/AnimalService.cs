using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Nest;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AnimalService :IAnimalService
    {
        public IAnimalRepository repo;
        public IAnimalTypeRepository typeRepo;
        public ICageRepository cageRepo;
        public IAnimalCageRepository animalCageRepo;
        public IFlockRepository flockRepo;
        public IIndividualRepository individualRepo;
        public IAnimalImageRepository animalImageRepo;
        public IIncompatibleAnimalTypeRepository incompatibleAnimalTypeRepo;
        public IObjectViewService objectViewService;
        public AnimalService(IAnimalRepository repo, IAnimalTypeRepository typeRepo, 
            IAnimalCageRepository animalCageRepo, ICageRepository cageRepo, IObjectViewService objectViewService, 
            IFlockRepository flockRepo, IIndividualRepository individualRepo, 
            IAnimalImageRepository animalImageRepo,
            IIncompatibleAnimalTypeRepository incompatibleAnimalTypeRepo)
        {
            this.repo = repo;
            this.typeRepo = typeRepo;
            this.animalCageRepo = animalCageRepo;
            this.cageRepo = cageRepo;
            this.flockRepo = flockRepo;
            this.individualRepo = individualRepo;
            this.animalImageRepo = animalImageRepo;
            this.objectViewService = objectViewService;
            this.incompatibleAnimalTypeRepo = incompatibleAnimalTypeRepo;
        }
        public async Task<ServiceResult> GetListAnimal()
        {
            try
            {
                var animals = await repo.GetListAnimal();
                if (animals == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListAnimalView(animals);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Animals",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.Message.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GetListAnimalSearching(AnimalSearch<AnimalView> key)
        {
            try
            {
                var animals = await repo.GetListAnimal();
                if (animals == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListAnimalView(animals);
                if (key.AnimalTypeId != null)
                {
                    result = result.FindAll(l => l.AnimalType?.Id == key.AnimalTypeId);
                }
                if(key.Classify == "Flock")
                {
                    result = result.FindAll(l => l.Classify == key.Classify);
                }
                else if (key.Classify == "Individual")
                {
                    result = result.FindAll(l => l.Classify == key.Classify);
                    if (key.Sorting?.PropertySort == "Age")
                    {
                        if (key.Sorting.IsAsc)
                        {
                            result.OrderBy(l => l.Individual?.Age);
                        }
                        else
                        {
                            result.OrderByDescending(l => l.Individual?.Age);
                        }
                    }
                }
                if(key.Sorting?.PropertySort == "Id")
                {
                    if (key.Sorting.IsAsc)
                    {
                        result.OrderBy(l => l.Id);
                    }
                    else
                    {
                        result.OrderByDescending(l => l.Id);
                    }
                }
                int? totalNumberPaging = null;
                if (key.Paging != null)
                {
                    Paging<AnimalView> paging = new();
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
        public async Task<ServiceResult> GetListAnimalByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var type = typeRepo.GetById(animalTypeId);
                if (type == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var animals = await repo.GetListAnimalByAnimalTypeId(animalTypeId);
                if (animals == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListAnimalView(animals);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Animals",
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
        public async Task<ServiceResult> GetListAnimalByCageId(int cageId)
        {
            try
            {
                var cage = cageRepo.GetById(cageId);
                if(cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Cage Not Found!",
                    };
                }
                var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cageId);
                if(animalCages == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                List<Animal> animals = new List<Animal>();
                foreach(var animalCage in animalCages)
                {
                    animals.Add(repo.GetById(animalCage.AnimalId));
                }
                if (animals == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListAnimalView(animals);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Animals",
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
        public async Task<ServiceResult> GetAnimalById(int id)
        {
            try
            {
                var animal = repo.GetById(id);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetAnimalView(animal);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Animal",
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
        public async Task<ServiceResult> AddAnimal(AnimalAdd key)
        {
            try
            {
                if(key.Classify != "Individual" && key.Classify != "Flock")
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "classify is invalid",
                    };
                }
                var animal = await repo.AddAnimal(key);
                if(animal.Classify == "Flock")
                {
                    await flockRepo.AddFlock(animal.Id ,key.Flock);
                }
                else if(animal.Classify == "Individual")
                {
                    await individualRepo.AddIndividual(animal.Id, key.Individual);
                }
                await animalImageRepo.AddAnimalImageByAnimalId(animal.Id, key.UrlImages);
                var result = await objectViewService.GetAnimalView(animal);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.Message.ToString(),
                };
            }
        }
        public async Task<ServiceResult> UpdateAnimal(AnimalUpdate key)
        {
            try
            {
                var animal = await repo.UpdateAnimal(key);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                if (animal.Classify == "Flock")
                {
                    await flockRepo.UpdateFlock(animal.Id, key.Flock);
                }
                else if (animal.Classify == "Individual")
                {
                    await individualRepo.UpdateIndividual(animal.Id, key.Individual);
                }
                await animalImageRepo.DeleteAnimalImageByAnimalId(animal.Id);
                await animalImageRepo.AddAnimalImageByAnimalId(animal.Id, key.UrlImages);
                var result = await objectViewService.GetAnimalView(animal);
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
        public async Task<ServiceResult> DeleteAnimal(int animalId)
        {
            try
            {
                var animal = repo.GetById(animalId);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                if (animal.Classify == "Flock")
                {
                    var flock = await flockRepo.GetFlockByAnimalId(animalId);
                    await flockRepo.RemoveAsync(flock);
                }
                else if (animal.Classify == "Individual")
                {
                    var individual = await individualRepo.GetIndividualByAnimalId(animalId);
                    await individualRepo.RemoveAsync(individual);
                }
                await animalImageRepo.DeleteAnimalImageByAnimalId(animal.Id);
                await repo.RemoveAsync(animal);
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
                    Message = ex.Message.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddAnimalCage(int animalId, int cageId)
        {
            try
            {
                var cage = cageRepo.GetById(cageId);
                if(cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Cage Not Found"
                    };
                }
                if(cage.Classify == "Individual" && cage.CurrentQuantity >= cage.MaxQuantity)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Cage is Max"
                    };
                }

                var animal = repo.GetById(animalId);
                if(cage.Classify != animal.Classify && cage.Classify == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Not Same Classify"
                    };
                }

                if(animal.Classify == "Individual")
                {
                    var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cageId);
                    bool incompatible = false;
                    foreach (var animalCageTemp in animalCages)
                    {
                        var animal1 = repo.GetById(animalCageTemp.AnimalId);
                        if ((await incompatibleAnimalTypeRepo.CheckIncompatibleAnimalType((int)animal1.AnimalTypeId, (int)animal.AnimalTypeId)) == true)
                        {
                            incompatible = true;
                            break;
                        }
                    }
                    if (incompatible == false)
                    {
                        var animalCage = await animalCageRepo.AddAnimalCage(animalId, cageId);
                        if (animalCage == null)
                        {
                            return new ServiceResult
                            {
                                Status = 200,
                                Message = "Add Fail",
                            };
                        }
                        cage.CurrentQuantity += 1;
                    }
                    else
                    {
                        return new ServiceResult
                        {
                            Status = 400,
                            Message = "Cage Has Animal Incompatible",
                        };
                    }
                    await cageRepo.UpdateAsync(cage);
                }
                else if(animal.Classify == "Flock")
                {
                    var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cageId);
                    if(animalCages?.Count > 0)
                    {
                        return new ServiceResult
                        {
                            Status = 400,
                            Message = "Cage Had Animal",
                        };
                    }
                    var animalCage = await animalCageRepo.AddAnimalCage(animalId, cageId);
                    if (animalCage == null)
                    {
                        return new ServiceResult
                        {
                            Status = 200,
                            Message = "Add Fail",
                        };
                    }
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
        public async Task<ServiceResult> RemoveAnimalCage(int animalId, int cageId)
        {
            try
            {
                var cage = cageRepo.GetById(cageId);
                if (cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,   
                        Message = "Cage Not Found"
                    };
                }
                var animal = repo.GetById(animalId);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Animal Not Found"
                    };
                }
                var animalCage = await animalCageRepo.RemoveAnimalCage(animalId, cageId);
                if (animalCage == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Animal Not In Cage"
                    };
                }
                if(cage.Classify == "Individual")
                {
                    cage.CurrentQuantity -= 1;
                    await cageRepo.UpdateAsync(cage);
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
        public async Task<ServiceResult> ReplaceAnimalCage(int animalId, int cageId)
        {
            try
            {
                var cage = cageRepo.GetById(cageId);
                if (cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Cage Not Found"
                    };
                }
                var animal = repo.GetById(animalId);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Animal Not Found"
                    };
                }
                var animalCage = await animalCageRepo.GetAnimalCageCurrentByAnimalId(animalId);
                animalCage = await animalCageRepo.RemoveAnimalCage(animalId, (int)animalCage.CageId);
                if (animalCage == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Animal Not In Cage"
                    };
                }
                animalCage = await animalCageRepo.AddAnimalCage(animalId, cageId);
                if (animalCage == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Fail"
                    };
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Success",
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
