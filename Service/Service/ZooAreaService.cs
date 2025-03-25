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
    public class ZooAreaService : IZooAreaService
    {
        public IZooAreaRepository repo;
        public IObjectViewService objectViewService;
        public ITeamRepository teamRepo;
        public ZooAreaService(IZooAreaRepository repo, IObjectViewService objectViewService, ITeamRepository teamRepo)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
            this.teamRepo = teamRepo;
        }
        public async Task<ServiceResult> GetListZooArea()
        {
            try
            {
                var zooAreas = await repo.GetListZooArea();
                if (zooAreas == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListZooAreaView(zooAreas);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Zoo Areas",
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
        public async Task<ServiceResult> GetListZooAreaUnassign()
        {
            try
            {
                var zooAreas = await repo.GetListZooArea();
                if (zooAreas == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                List<ZooArea> listTemp = new();
                foreach(var zooArea in zooAreas)
                {
                    if(await teamRepo.GetTeamByZooAreaId(zooArea.Id) == null)
                    {
                        listTemp.Add(zooArea);
                    }
                }
                var result = await objectViewService.GetListZooAreaView(listTemp);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Zoo Areas",
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
        public async Task<ServiceResult> GetListZooAreaSearching(ZooAreaSearch<ZooAreaView> key)
        {
            try
            {
                var zooAreas = await repo.GetListZooArea();
                if (zooAreas == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListZooAreaView(zooAreas);
                if (key.Name != null)
                {
                    result = result.FindAll(l => l.Name == key.Name);
                }
                if (key.Sorting?.PropertySort == "Name")
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
                    Paging<ZooAreaView> paging = new();
                    totalNumberPaging = paging.MaxPageNumber(result, key.Paging.PageSize);
                    result = paging.PagingList(result, key.Paging.PageSize, key.Paging.PageNumber);           
                }
                if (totalNumberPaging == null) totalNumberPaging = 1;
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Zoo Areas",
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
        public async Task<ServiceResult> GetZooAreaById(int id)
        {
            try
            {
                var zooArea = repo.GetById(id);
                if (zooArea == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetZooAreaView(zooArea);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Zoo Area",
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
        public async Task<ServiceResult> AddZooArea(ZooAreaAdd key)
        {
            try
            {
                var zooArea = await repo.AddZooArea(key);
                var result = await objectViewService.GetZooAreaView(zooArea);
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
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> UpdateZooArea(ZooAreaUpdate key)
        {
            try
            {
                var zooArea = await repo.UpdateZooArea(key);
                if (zooArea == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                var result = await objectViewService.GetZooAreaView(zooArea);
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
        public async Task<ServiceResult> DeleteZooArea(int id)
        {
            try
            {
                var zooArea = repo.GetById(id);
                if (zooArea == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                await repo.RemoveAsync(zooArea);
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
