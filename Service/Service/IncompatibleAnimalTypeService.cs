using BO.Models;
using DAO.AddModel;
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
        public IObjectViewService objectViewService;
        public IncompatibleAnimalTypeService(IIncompatibleAnimalTypeRepository repo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
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
                    int temp = key.AnimalTypeId1;
                    key.AnimalTypeId1 = key.AnimalTypeId2;
                    key.AnimalTypeId2 = temp;
                }
                if(await repo.CheckIncompatibleAnimalType(key.AnimalTypeId1, key.AnimalTypeId2))
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
    }
}
