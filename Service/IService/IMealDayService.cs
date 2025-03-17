using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IMealDayService
    {
        public Task<ServiceResult> GetListMealDay();
        public Task<ServiceResult> GetListMealDayByAnimalTypeId(int animalTypeId);
        public Task<ServiceResult> GeMealDayById(int id);
        public Task<ServiceResult> AddMealDay(MealDayAdd key);
        public Task<ServiceResult> UpdateMealDay(MealDayUpdate key);
    }
}
