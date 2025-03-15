using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.SearchModel;
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
        public IObjectViewService objectViewService;
        public AnimalTypeService(IAnimalTypeRepository repo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
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
                var result = await objectViewService.GetListAnimalTypeView(animalTypes);
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
        public async Task<ServiceResult> GetListAnimalTypeSearching(AnimalTypeSearch<AnimalTypeView> key)
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
                var result = await objectViewService.GetListAnimalTypeView(animalTypes);
                if (key.ScientificName != null)
                {
                    result = result.FindAll(l => l.ScientificName == key.ScientificName);
                }
                if (key.VietnameseName != null)
                {
                    result = result.FindAll(l => l.VietnameseName == key.VietnameseName);
                }
                if (key.EnglishName != null)
                {
                    result = result.FindAll(l => l.EnglishName == key.EnglishName);
                }
                if (key.Sorting?.PropertySort == "Id")
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
                else if(key.Sorting?.PropertySort == "ScientificName")
                {
                    if (key.Sorting.IsAsc)
                    {
                        result.OrderBy(l => l.ScientificName);
                    }
                    else
                    {
                        result.OrderByDescending(l => l.ScientificName);
                    }
                }
                else if (key.Sorting?.PropertySort == "VietnameseName")
                {
                    if (key.Sorting.IsAsc)
                    {
                        result.OrderBy(l => l.VietnameseName);
                    }
                    else
                    {
                        result.OrderByDescending(l => l.VietnameseName);
                    }
                }
                else if (key.Sorting?.PropertySort == "EnglishName")
                {
                    if (key.Sorting.IsAsc)
                    {
                        result.OrderBy(l => l.EnglishName);
                    }
                    else
                    {
                        result.OrderByDescending(l => l.EnglishName);
                    }
                }
                int? totalNumberPaging = null;
                if (key.Paging != null)
                {
                    Paging<AnimalTypeView> paging = new();
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
                var result = await objectViewService.GetAnimalTypeView(animalType);
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
                var result = await objectViewService.GetAnimalTypeView(animalType);
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
                var result = await objectViewService.GetAnimalTypeView(animalType);
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
        public async Task<ServiceResult> DeleteAnimalType(int id)
        {
            try
            {
                var animalType = repo.GetById(id);
                if (animalType == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                await repo.RemoveAsync(animalType);
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
                    Message = ex.ToString(),
                };
            }
        }
    }
}
