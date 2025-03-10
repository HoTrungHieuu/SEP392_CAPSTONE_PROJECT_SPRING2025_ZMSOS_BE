using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IApplicationTypeService
    {
        public Task<ServiceResult> GetListApplicationType();
        public Task<ServiceResult> GetApplicationTypeById(int id);
        public Task<ServiceResult> AddApplicationType(ApplicationTypeAdd key);
    }
}
