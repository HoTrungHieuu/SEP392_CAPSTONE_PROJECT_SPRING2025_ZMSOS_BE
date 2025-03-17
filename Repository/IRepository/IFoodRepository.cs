using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IFoodRepository : IGenericRepository<Food>
    {
        public Task<List<Food>?> GetListFood();
        public Task<Food> AddFood(FoodAdd key);
        public Task<Food?> UpdateFood(FoodUpdate key);
        public FoodView ConvertFoodIntoFoodView(Food food);
    }
}
