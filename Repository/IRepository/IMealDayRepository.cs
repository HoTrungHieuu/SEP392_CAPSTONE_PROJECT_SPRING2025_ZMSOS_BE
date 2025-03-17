using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IMealDayRepository : IGenericRepository<MealDay>
    {
        public Task<List<MealDay>?> GetListMealDayByAnimalTypeId(int animalTypeId);
        public Task<MealDay?> AddMealDay(MealDayAdd key);
        public Task<MealDay?> UpdateMealDay(MealDayUpdate key);
        public MealDayView ConvertMealDayIntoMealDayView(MealDay mealDay, AnimalTypeView animalType, List<MealFoodView>? foods);
    }
}
