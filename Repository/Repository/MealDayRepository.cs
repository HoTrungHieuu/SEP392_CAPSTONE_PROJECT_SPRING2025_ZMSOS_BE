using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
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
    public class MealDayRepository : GenericRepository<MealDay>, IMealDayRepository
    {
        public MealDayRepository()
        {
        }
        public async Task<List<MealDay>?> GetListMealDay()
        {
            try
            {
                var mealDays = (await GetAllAsync()).FindAll(l=>l.Status != "Deleted");
                mealDays = mealDays.OrderByDescending(l => l.CreatedDate).ToList();
                if (mealDays == null) return null;
                return mealDays;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<MealDay>?> GetListMealDayByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var mealDays = (await GetListMealDay()).FindAll(l => l.AnimalTypeId == animalTypeId);
                if (mealDays == null) return null;
                return mealDays;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<MealDay?> AddMealDay(MealDayAdd key)
        {
            try
            {
                MealDay mealDay = new()
                {
                    AnimalTypeId = key.AnimalTypeId,
                    Name = key.Name,
                    PeriodOfTime = key.PeriodOfTime.ToString(),
                    TimeStartInDay = key.TimeStartInDay,
                    TimeEndInDay = key.TimeEndInDay,
                    CreatedDate = DateTime.Now,
                    Status = "Active"
                };
                await CreateAsync(mealDay);
                return mealDay;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<MealDay?> UpdateMealDay(MealDayUpdate key)
        {
            try
            {
                var mealDay = GetById(key.Id);
                if (mealDay == null)
                {
                    return null;
                }
                mealDay.Name = key.Name;
                mealDay.PeriodOfTime = key.PeriodOfTime.ToString();
                mealDay.TimeStartInDay = key.TimeStartInDay;
                mealDay.TimeEndInDay = key.TimeEndInDay;
                mealDay.UpdatedDate = DateTime.Now;
                await UpdateAsync(mealDay);
                return mealDay;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> DisableMealDay(int id)
        {
            try
            {
                var mealDay = GetById(id);
                if (mealDay == null)
                {
                    return 0;
                }
                mealDay.Status = "Deleted";
                var row = await UpdateAsync(mealDay);
                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MealDayView ConvertMealDayIntoMealDayView(MealDay mealDay, AnimalTypeView animalType, List<MealFoodView>? foods)
        {
            try
            {
                MealDayView result = new MealDayView();
                result.ConvertMealDayIntoMealDayView(mealDay, animalType, foods);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
