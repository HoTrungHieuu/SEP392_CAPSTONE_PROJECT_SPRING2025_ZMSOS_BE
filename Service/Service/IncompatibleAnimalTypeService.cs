using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
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
    public class IncompatibleAnimalTypeService : IIncompatibleAnimalTypeService
    {
        public IIncompatibleAnimalTypeRepository repo;
        public IAnimalTypeRepository animalTypeRepo;
        public IObjectViewService objectViewService;
        public IncompatibleAnimalTypeService(IIncompatibleAnimalTypeRepository repo, IObjectViewService objectViewService, IAnimalTypeRepository animalTypeRepo)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
            this.animalTypeRepo = animalTypeRepo;
        }
        public async Task<ServiceResult> GetListIncompatibleAnimalType()
        {
            try
            {
                var incompatibleAnimalTypes = await repo.GetListIncompatibleAnimalType();
                if (incompatibleAnimalTypes == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListIncompatibleAnimalTypeView(incompatibleAnimalTypes);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "IncompatibleAnimalTypes",
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
        public async Task<ServiceResult> GetListIncompatibleAnimalTypeSpecial()
        {
            try
            {
                var animalTypes = await animalTypeRepo.GetListAnimalType();
                List<IncompatibleTypeViewSpecial> result = new();
                foreach(var animalType in animalTypes)
                {
                    var incompatibles = await repo.GetListIncompatibleAnimalTypeByAnimalTypeId(animalType.Id);
                    List<AnimalType> animalTypesTemp = new();
                    foreach(var incompatible in incompatibles)
                    {
                        int id = 0;
                        if(incompatible.AnimalTypeId1 == animalType.Id)
                        {
                            id = (int)incompatible.AnimalTypeId2;
                        }
                        else
                        {
                            id = (int)incompatible.AnimalTypeId1;
                        }
                        animalTypesTemp.Add(animalTypeRepo.GetById(id));
                    }
                    result.Add(new()
                    {
                        AnimalType1 = await objectViewService.GetAnimalTypeView(animalType),
                        AnimalTypes2 = await objectViewService.GetListAnimalTypeView(animalTypesTemp)
                    });
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "IncompatibleAnimalTypes",
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
        public async Task<ServiceResult> GetIncompatibleAnimalTypeById(int id)
        {
            try
            {
                var incompatibleAnimalType = repo.GetById(id);
                if (incompatibleAnimalType == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetIncompatibleAnimalTypeView(incompatibleAnimalType);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "IncompatibleAnimalType",
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
        public async Task<ServiceResult> AddIncompatibleAnimalType(IncompatibleAnimalTypeAdd key)
        {
            try
            {
                if(key.AnimalTypeId1 == key.AnimalTypeId2)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "id1 and id2 are same!",
                    };
                }
                if(key.AnimalTypeId1 > key.AnimalTypeId2)
                {
                    int? temp = key.AnimalTypeId1;
                    key.AnimalTypeId1 = key.AnimalTypeId2;
                    key.AnimalTypeId2 = temp;
                }
                if(await repo.CheckIncompatibleAnimalType((int)key.AnimalTypeId1, (int)key.AnimalTypeId2))
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Existed!",
                    };
                }

                var incompatibleAnimalType = await repo.AddIncompatibleAnimalType(key);
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
        public async Task<ServiceResult> AddListIncompatibleAnimalType(IncompatibleTypeAddList key)
        {
            try
            {
                List<int> unsuccessIds = new List<int>();
                foreach(int id in key.AnimalTypeIds2)
                {
                    if (key.AnimalTypeId1 == id)
                    {
                       unsuccessIds.Add(id);
                       continue;
                    }
                    int idTemp1 = 0;
                    int idTemp2 = 0;
                    if (key.AnimalTypeId1 > id)
                    {
                        idTemp1 = id;
                        idTemp2 = (int)key.AnimalTypeId1;
                    }
                    else
                    {
                        idTemp1 = (int)key.AnimalTypeId1;
                        idTemp2 = id;
                    }
                    if (await repo.CheckIncompatibleAnimalType((int)idTemp1, (int)idTemp2))
                    {
                        unsuccessIds.Add(id);
                        continue;
                    }
                    await repo.AddIncompatibleAnimalType(new()
                    {
                        AnimalTypeId1 = idTemp1,
                        AnimalTypeId2 = idTemp2,
                        Reason = key.Reason,
                    });
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Add Success with id unsuccess {string.Join(", ", unsuccessIds)}",
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
