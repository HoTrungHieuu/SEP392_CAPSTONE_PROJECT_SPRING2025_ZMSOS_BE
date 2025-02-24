using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IApplicationService
    {
        public Task<ServiceResult> GetListApplicationBySenderId(int senderId);
        public Task<ServiceResult> GetListApplicationByRecieverId(int recieverId);
        public Task<ServiceResult> GetApplicationById(int id);
        public Task<ServiceResult> AddApplication(ApplicationAdd key);

    }
}
