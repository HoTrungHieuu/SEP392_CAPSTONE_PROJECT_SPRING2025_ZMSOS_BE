using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
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
        public AnimalService(IAnimalRepository repo, IAnimalTypeRepository typeRepo, IAnimalCageRepository animalCageRepo, ICageRepository cageRepo)
        {
            this.repo = repo;
            this.typeRepo = typeRepo;
            this.animalCageRepo = animalCageRepo;
            this.cageRepo = cageRepo;
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

                List<AnimalTypeView> animalTypes = new();
                for (int i = 0; i < animals.Count; i++)
                {
                    AnimalTypeView animalType = new();
                    animalType.ConvertAnimalTypeIntoAnimalTypeView(typeRepo.GetById((int)animals[i].AnimalTypeId));
                    animalTypes.Add(animalType);
                }

                var result = repo.ConvertListAnimalIntoListAnimalView(animals, animalTypes);
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

                List<AnimalTypeView> animalTypes = new();
                for (int i = 0; i < animals.Count; i++)
                {
                    AnimalTypeView animalType = new();
                    animalType.ConvertAnimalTypeIntoAnimalTypeView(typeRepo.GetById((int)animals[i].AnimalTypeId));
                    animalTypes.Add(animalType);
                }

                var result = repo.ConvertListAnimalIntoListAnimalView(animals, animalTypes);
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
                var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cageId);
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

                List<AnimalTypeView> animalTypes = new();
                for (int i = 0; i < animals.Count; i++)
                {
                    AnimalTypeView animalType = new();
                    animalType.ConvertAnimalTypeIntoAnimalTypeView(typeRepo.GetById((int)animals[i].AnimalTypeId));
                    animalTypes.Add(animalType);
                }

                var result = repo.ConvertListAnimalIntoListAnimalView(animals, animalTypes);
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

                AnimalTypeView animalType = new();
                animalType = typeRepo.ConvertAnimalTypeIntoAnimalTypeView(typeRepo.GetById((int)animal.AnimalTypeId));

                var result = repo.ConvertAnimalIntoAnimalView(animal, animalType);
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
                var animal = await repo.AddAnimal(key);
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
        public async Task<ServiceResult> AddAnimalCage(List<int> animalIds, int cageId)
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
                List<int> unsuccessId = new List<int>();
                foreach (int id in animalIds)
                {
                    var animalCage = await animalCageRepo.AddAnimalCage(id, cageId);
                    if(animalCage == null)
                    {
                        unsuccessId.Add(id);
                    }
                }
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
                var animalCage = await animalCageRepo.RemoveAnimalCage(animalId, cageId);
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
