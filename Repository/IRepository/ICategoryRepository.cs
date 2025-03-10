using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<Category> AddCategory(CategoryAdd key);
        public Task<Category?> UpdateCategory(CategoryUpdate key);
        public CategoryView ConvertCategoryIntoCategoryView(Category category);
    }
}
