using DAO.AddModel;
using DAO.UpdateModel;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class FoodService : IFoodService
    {
        public IFoodRepository repo;
        public IObjectViewService objectViewService;
        public FoodService(IFoodRepository repo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListFood()
        {
            try
            {
                var foods = await repo.GetListFood();
                if (foods == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListFoodView(foods);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Foods",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GeFoodById(int id)
        {
            try
            {
                var food = repo.GetById(id);
                if (food == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetFoodView(food);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Food",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddFood(FoodAdd key)
        {
            try
            {
                var food = await repo.AddFood(key);
                var result = await objectViewService.GetFoodView(food);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> UpdateFood(FoodUpdate key)
        {
            try
            {
                var food = await repo.UpdateFood(key);
                if (food == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                var result = await objectViewService.GetFoodView(food);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
