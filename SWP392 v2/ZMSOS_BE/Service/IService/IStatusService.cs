using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IStatusService
    {
        public Task<ServiceResult> GetListStatus();
        public Task<ServiceResult> GetListStatusByCategoryId(int categoryId);
        public Task<ServiceResult> GetListCategory();
        public Task<ServiceResult> GetListCategoryByStatusId(int statusId);
        public Task<ServiceResult> AddStatus(StatusAdd key);
        public Task<ServiceResult> UpdateStatus(StatusUpdate key);
        public Task<ServiceResult> AddCategory(CategoryAdd key);
        public Task<ServiceResult> UpdateCategory(CategoryUpdate key);
        public Task<ServiceResult> AddStatusCategory(int statusId, int categoryId);
        public Task<ServiceResult> DeleteStatusCategory(int statusId, int categoryId);
    }
}
