using DAO.AddModel;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ApplicationTypeService : IApplicationTypeService
    {
        public IApplicationTypeRepository repo;
        public IObjectViewService objectViewService;
        public ApplicationTypeService(IApplicationTypeRepository repo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListApplicationType()
        {
            try
            {
                var applicationTypes = await repo.GetListApplcationType();
                if (applicationTypes == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListApplicationTypeView(applicationTypes);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "ApplicationTypes",
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
        public async Task<ServiceResult> GetApplicationTypeById(int id)
        {
            try
            {
                var applicationType = repo.GetById(id);
                if (applicationType == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetApplicationTypeView(applicationType);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "ApplicationType",
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
        public async Task<ServiceResult> AddApplicationType(ApplicationTypeAdd key)
        {
            try
            {
                await repo.AddApplicationType(key);
                
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
