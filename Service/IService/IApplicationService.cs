using DAO.AddModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
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
        public Task<ServiceResult> GetListApplicationBySenderIdSearch(int senderId, ApplicationSearch<ApplicationView> key);
        public Task<ServiceResult> GetListApplicationByRecieverId(int recieverId);
        public Task<ServiceResult> GetListApplicationByRecieverIdSearch(int recieverId, ApplicationSearch<ApplicationView> key);
        public Task<ServiceResult> GetApplicationById(int id);
        public Task<ServiceResult> AddApplication(ApplicationAdd key);
        public Task<ServiceResult> UpdateApplication(ApplicationUpdate key);

    }
}
