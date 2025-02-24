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
    public class AnimalTypeService : IAnimalTypeService
    {
        public IAnimalTypeRepository repo;
        public AnimalTypeService(IAnimalTypeRepository repo)
        {
            this.repo = repo;
        }
        public async Task<ServiceResult> GetListAnimalType()
        {
            try
            {
                var animalTypes = await repo.GetListAnimalType();
                if (animalTypes == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = repo.ConvertListAnimalTypeIntoListAnimalTypeView(animalTypes);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "AnimalTypes",
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
        public async Task<ServiceResult> GetAnimalTypeById(int id)
        {
            try
            {
                var animalType = repo.GetById(id);
                if (animalType == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = repo.ConvertAnimalTypeIntoAnimalTypeView(animalType);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "AnimalType",
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
        public async Task<ServiceResult> AddAnimalType(AnimalTypeAdd key)
        {
            try
            {
                var animalType = await repo.AddAnimalType(key);
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
        public async Task<ServiceResult> UpdateAnimalType(AnimalTypeUpdate key)
        {
            try
            {
                var animalType = await repo.UpdateAnimalType(key);
                if (animalType == null)
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
    }
}
