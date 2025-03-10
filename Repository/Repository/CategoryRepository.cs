using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository()
        {
        }
        public async Task<Category> AddCategory(CategoryAdd key)
        {
            try
            {
                Category category = new()
                {
                    Name = key.TableName,
                };
                await CreateAsync(category);
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Category?> UpdateCategory(CategoryUpdate key)
        {
            try
            {
                var category = GetById(key.Id);
                if (category == null) return null;
                category.Name = key.TableName;
                await UpdateAsync(category);
                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CategoryView ConvertCategoryIntoCategoryView(Category category)
        {
            try
            {
                CategoryView result = new CategoryView();
                result.ConvertCategoryIntoCategoryView(category);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
