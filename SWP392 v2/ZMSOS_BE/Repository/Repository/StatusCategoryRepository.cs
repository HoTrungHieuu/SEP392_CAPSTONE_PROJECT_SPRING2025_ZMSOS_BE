using BO.Models;
using DAO.AddModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class StatusCategoryRepository : GenericRepository<StatusCategory>, IStatusCategoryRepository
    {
        public StatusCategoryRepository()
        {
        }
        public async Task<List<StatusCategory>?> GetListStatusCategoryByStatusId(int statusId)
        {
            try
            {
                var statusCategorys = (await GetAllAsync()).FindAll(l => l.StatusId == statusId);
                return statusCategorys;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<StatusCategory>?> GetListStatusCategoryByCategoryId(int categoryId)
        {
            try
            {
                var statusCategorys = (await GetAllAsync()).FindAll(l => l.CategoryId == categoryId);
                return statusCategorys;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<StatusCategory> AddStatusCategory(int statusId, int categoryId)
        {
            try
            {
                StatusCategory statusCategory = new()
                {
                    StatusId = statusId,
                    CategoryId = categoryId
                };
                await CreateAsync(statusCategory);
                return statusCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<StatusCategory?> RemoveStatusCategory(int statusId, int categoryId)
        {
            try
            {
                var statusCategory = (await GetAllAsync()).FirstOrDefault(l => l.StatusId == statusId && l.CategoryId == categoryId);
                if(statusCategory != null)
                {
                    await RemoveAsync(statusCategory);
                }
                return statusCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}