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
    public class CageService : ICageService
    {
        public ICageRepository repo;
        public IZooAreaRepository areaRepo;
        public IObjectViewService objectViewService;
        public CageService(ICageRepository repo, IZooAreaRepository areaRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.areaRepo = areaRepo;
            this.objectViewService = objectViewService;
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
        public async Task<ServiceResult> AddCage(CageAdd key)
        {
            try
            {
                var cage = await repo.AddCage(key);
                var result = await objectViewService.GetCageView(cage);
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
