using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IStatusCategoryRepository : IGenericRepository<StatusCategory>
    {
        public Task<List<StatusCategory>?> GetListStatusCategoryByStatusId(int statusId);
        public Task<List<StatusCategory>?> GetListStatusCategoryByCategoryId(int categoryId);
        public Task<StatusCategory> AddStatusCategory(int statusId, int categoryId);
        public Task<StatusCategory?> RemoveStatusCategory(int statusId, int categoryId);
    }
}
