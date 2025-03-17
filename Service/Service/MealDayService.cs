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
    public class MealDayService : IMealDayService
    {
        public IMealDayRepository repo;
        public IMealFoodRepository mealFoodRepo;
        public IFoodRepository foodRepo;
        public IObjectViewService objectViewService;
        public MealDayService(IMealDayRepository repo, IObjectViewService objectViewService, IMealFoodRepository mealFoodRepo, IFoodRepository foodRepo)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
            this.mealFoodRepo = mealFoodRepo;
            this.foodRepo = foodRepo;
        }
        public async Task<ServiceResult> GetListMealDay()
        {
            try
            {
                var mealDays = await repo.GetAllAsync();
                if (mealDays == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListMealDayView(mealDays);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "MealDays",
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
        public async Task<ServiceResult> GetListMealDayByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var mealDays = await repo.GetListMealDayByAnimalTypeId(animalTypeId);
                if (mealDays == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListMealDayView(mealDays);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "MealDays",
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
        public async Task<ServiceResult> GeMealDayById(int id)
        {
            try
            {
                var mealDay = repo.GetById(id);
                if (mealDay == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetMealDayView(mealDay);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "MealDay",
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
        public async Task<ServiceResult> AddMealDay(MealDayAdd key)
        {
            try
            {
                var mealDay = await repo.AddMealDay(key);
                double totalCalo = 0;
                foreach (var foodAdd in key.FoodsAdd)
                {
                    var mealFood = await mealFoodRepo.AddMealFood(mealDay.Id, foodAdd);
                    if(mealFood?.Quantitative != null)
                    {
                        totalCalo += (double)mealFood.Quantitative * (double)(foodRepo.GetById(mealFood.FoodId)).CaloPerGram;
                    }
                }
                mealDay.TotalCalo = totalCalo;
                await repo.UpdateAsync(mealDay);
                var result = await objectViewService.GetMealDayView(mealDay);
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
        public async Task<ServiceResult> UpdateMealDay(MealDayUpdate key)
        {
            try
            {
                var mealDay = await repo.UpdateMealDay(key);
                if (mealDay == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                await mealFoodRepo.DeleteMealFoodByMealDayId(mealDay.Id);
                double totalCalo = 0;
                foreach (var foodAdd in key.FoodsAdd)
                {
                    var mealFood = await mealFoodRepo.AddMealFood(mealDay.Id, foodAdd);
                    if (mealFood?.Quantitative != null)
                    {
                        totalCalo += (double)mealFood.Quantitative * (double)(foodRepo.GetById(mealFood.FoodId)).CaloPerGram;
                    }
                }
                mealDay.TotalCalo = totalCalo;
                await repo.UpdateAsync(mealDay);
                var result = await objectViewService.GetMealDayView(mealDay);
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
