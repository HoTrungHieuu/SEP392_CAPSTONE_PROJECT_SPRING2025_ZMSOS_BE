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
    public class MealFoodRepository : GenericRepository<MealFood>, IMealFoodRepository
    {
        public async Task<List<MealFood>?> GetListMealFoodByMealDayId(int mealDayId)
        {
            try
            {
                var mealFoods = (await GetAllAsync()).FindAll(l => l.MealDayId == mealDayId);
                if (mealFoods == null) return null;
                return mealFoods;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<MealFood?> AddMealFood(int mealDayId,MealFoodAdd key)
        {
            try
            {
                MealFood mealFood = new()
                {
                    MealDayId = mealDayId,
                    FoodId = key.FoodId,
                    Quantitative = key.Quantitative,
                };
                await CreateAsync(mealFood);
                return mealFood;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task DeleteMealFoodByMealDayId(int mealDayId)
        {
            try
            {
                var mealFoods = (await GetAllAsync()).FindAll(l => l.MealDayId == mealDayId);
                foreach(var item in mealFoods)
                {
                    await RemoveAsync(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
