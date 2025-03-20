using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IFoodService
    {
        public Task<ServiceResult> GetListFood();
        public Task<ServiceResult> GeFoodById(int id);
        public Task<ServiceResult> AddFood(FoodAdd key);
        public Task<ServiceResult> UpdateFood(FoodUpdate key);
        public Task<ServiceResult> DeleteFood(int foodId);

    }
}
