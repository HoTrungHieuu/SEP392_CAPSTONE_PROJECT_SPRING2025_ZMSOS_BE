using BO.Models;
using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IMealFoodRepository : IGenericRepository<MealFood>
    {
        public Task<List<MealFood>?> GetListMealFoodByMealDayId(int mealDayId);
        public Task<MealFood?> AddMealFood(int mealDayId, MealFoodAdd key);
        public System.Threading.Tasks.Task DeleteMealFoodByMealDayId(int mealDayId);

    }
}
