using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Service.Service
{
    public class ApplicationService : IApplicationService
    {
        public IApplicationRepository repo;
        public IApplicationTypeRepository applicationTypeRepo;
        public IUserRepository userRepo;
        public IObjectViewService objectViewService;
        public ApplicationService(IApplicationRepository repo, IApplicationTypeRepository applicationTypeRepo, IUserRepository userRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.applicationTypeRepo = applicationTypeRepo;
            this.userRepo = userRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListApplicationBySenderId(int senderId)
        {
            try
            {
                var applications = await repo.GetListApplcationBySenderId(senderId);
                if (applications == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListApplicationView(applications);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Applications",
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
        public async Task<ServiceResult> GetListApplicationByRecieverId(int recieverId)
        {
            try
            {
                var applications = await repo.GetListApplcationByRecieverId(recieverId);
                if (applications == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListApplicationView(applications);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Applications",
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
        public async Task<ServiceResult> GetApplicationById(int id)
        {
            try
            {
                var application = repo.GetById(id);
                if (application == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetApplicationView(application);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Application",
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
        public async Task<ServiceResult> AddApplication(ApplicationAdd key)
        {
            try
            {
                var application = await repo.AddApplication(key);
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
