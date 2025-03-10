using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class StatusService : IStatusService
    {
        public IStatusRepository repo;
        public ICategoryRepository categoryRepo;
        public IStatusCategoryRepository statusCategoryRepo;
        public IObjectViewService objectViewService;
        public StatusService(IStatusRepository repo, ICategoryRepository categoryRepo, IStatusCategoryRepository statusCategoryRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.categoryRepo = categoryRepo;
            this.statusCategoryRepo = statusCategoryRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListStatus()
        {
            try
            {
                var statuss = await repo.GetAllAsync();
                if (statuss == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListStatusView(statuss);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Statuss",
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
        public async Task<ServiceResult> GetListStatusByCategoryId(int categoryId)
        {
            try
            {
                var statusCategorys = (await statusCategoryRepo.GetAllAsync()).FindAll(l=>l.CategoryId == categoryId);
                List<Status> statuss = new List<Status>();
                foreach (var statusCategory in statusCategorys)
                {
                    statuss.Add(repo.GetById(statusCategory.StatusId));
                }
                if (statuss == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListStatusView(statuss);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Statuss",
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
        public async Task<ServiceResult> GetListCategory()
        {
            try
            {
                var categorys = await categoryRepo.GetAllAsync();
                if (categorys == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListCategoryView(categorys);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Categorys",
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
        public async Task<ServiceResult> GetListCategoryByStatusId(int statusId)
        {
            try
            {
                var statusCategorys = (await statusCategoryRepo.GetAllAsync()).FindAll(l => l.StatusId == statusId);
                List<Category> categorys = new List<Category>();
                foreach (var statusCategory in statusCategorys)
                {
                    categorys.Add(categoryRepo.GetById(statusCategory.CategoryId));
                }
                if (categorys == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListCategoryView(categorys);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Categorys",
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
        public async Task<ServiceResult> AddStatus(StatusAdd key)
        {
            try
            {
                var status = await repo.AddStatus(key);
                var result = await objectViewService.GetStatusView(status);
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
        public async Task<ServiceResult> UpdateStatus(StatusUpdate key)
        {
            try
            {
                var status = await repo.UpdateStatus(key);
                if (status == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                var result = await objectViewService.GetStatusView(status);
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
        public async Task<ServiceResult> AddCategory(CategoryAdd key)
        {
            try
            {
                var category = await categoryRepo.AddCategory(key);
                var result = await objectViewService.GetCategoryView(category);
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
        public async Task<ServiceResult> UpdateCategory(CategoryUpdate key)
        {
            try
            {
                var category = await categoryRepo.UpdateCategory(key);
                if (category == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                var result = await objectViewService.GetCategoryView(category);
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
        public async Task<ServiceResult> AddStatusCategory(int statusId, int categoryId)
        {
            try
            {
                var status = repo.GetById(statusId);
                if(status == null)
                {
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Status Not Found",
                    };
                }
                var category = categoryRepo.GetById(categoryId);
                if (category == null)
                {
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Category Not Found",
                    };
                }
                var statusCategory = await statusCategoryRepo.AddStatusCategory(statusId, categoryId);
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
        public async Task<ServiceResult> DeleteStatusCategory(int statusId, int categoryId)
        {
            try
            {
                var status = repo.GetById(statusId);
                if (status == null)
                {
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Status Not Found",
                    };
                }
                var category = categoryRepo.GetById(categoryId);
                if (category == null)
                {
                    return new ServiceResult
                    {
                        Status = 200,
                        Message = "Category Not Found",
                    };
                }
                var statusCategory = await statusCategoryRepo.RemoveStatusCategory(statusId, categoryId);
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
