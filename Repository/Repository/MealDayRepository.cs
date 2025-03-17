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
        public async Task<List<MealDay>?> GetListMealDayByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var mealDays = (await GetAllAsync()).FindAll(l => l.AnimalTypeId == animalTypeId);
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
                    PeriodOfTime = key.PeriodOfTime,
                    TimeStartInDay = key.TimeStartInDay,
                    TimeEndInDay = key.TimeEndInDay,
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
                mealDay.PeriodOfTime = key.PeriodOfTime;
                mealDay.TimeStartInDay = key.TimeStartInDay;
                mealDay.TimeEndInDay = key.TimeEndInDay;
                await UpdateAsync(mealDay);
                return mealDay;
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
