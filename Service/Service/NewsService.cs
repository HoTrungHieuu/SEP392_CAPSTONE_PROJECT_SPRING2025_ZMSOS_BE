using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class NewsService:INewsService
    {
        public INewsRepository repo;
        public IUserRepository userRepo;
        public IObjectViewService objectViewService;
        public NewsService(INewsRepository repo, IUserRepository userRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListNews()
        {
            try
            {
                var newss = await repo.GetListNews();
                if (newss == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListNewsView(newss);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Newss",
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
        public async Task<ServiceResult> GetNewsById(int id)
        {
            try
            {
                var news = repo.GetById(id);
                if (news == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetNewsView(news);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "News",
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
        public async Task<ServiceResult> AddNews(NewsAdd key)
        {
            try
            {
                var news = await repo.AddNews(key);
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
        public async Task<ServiceResult> UpdateNews(NewsUpdate key)
        {
            try
            {
                var news = await repo.UpdateNews(key);
                if(news == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found",
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
