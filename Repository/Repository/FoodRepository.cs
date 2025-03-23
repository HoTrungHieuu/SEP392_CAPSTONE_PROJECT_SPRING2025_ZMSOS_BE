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
    public class FoodRepository : GenericRepository<Food>, IFoodRepository
    {
        public FoodRepository()
        {
        }
        public async Task<List<Food>?> GetListFood()
        {
            try
            {
                var foods = await GetAllAsync();
                return foods;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Food> AddFood(FoodAdd key)
        {
            try
            {
                Food food = new()
                {
                    Name = key.Name,
                    Decription = key.Description,
                    CaloPerGram = key.CaloPerGram,
                    CreatedDate = DateTime.Now,
                };
                await CreateAsync(food);
                return food;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Food?> UpdateFood(FoodUpdate key)
        {
            try
            {
                var food = GetById(key.Id);
                if (food == null) return null;
                food.Name = key.Name;
                food.Decription = key.Description;
                food.CaloPerGram = key.CaloPerGram;
                food.UpdatedDate = DateTime.Now;
                await UpdateAsync(food);
                return food;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public FoodView ConvertFoodIntoFoodView(Food food)
        {
            try
            {
                FoodView result = new FoodView();
                result.ConvertFoodIntoFoodView(food);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
