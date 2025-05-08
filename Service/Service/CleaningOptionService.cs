using BO.Models;
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
    public class CleaningOptionService : ICleaningOptionService
    {
        public ICleaningOptionRepository repo;
        public ICleaningProcessRepository cleaningProcessRepo;
        public IUrlProcessRepository urlProcessRepo;
        public IObjectViewService objectViewService;
        public ITaskCleaningRepository taskCleaningRepo;
        public CleaningOptionService(ICleaningOptionRepository repo, ICleaningProcessRepository cleaningProcessRepo, IUrlProcessRepository urlProcessRepo, IObjectViewService objectViewService, ITaskCleaningRepository taskCleaningRepo)
        {
            this.repo = repo;
            this.cleaningProcessRepo = cleaningProcessRepo;
            this.urlProcessRepo = urlProcessRepo;
            this.objectViewService = objectViewService;
            this.taskCleaningRepo = taskCleaningRepo;
        }
        public async Task<ServiceResult> GetListCleaningOption()
        {
            try
            {
                var cleaningOptions = await repo.GetAllAsync();
                if (cleaningOptions == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListCleaninOptionView(cleaningOptions);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "CleaningOptions",
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
        public async Task<ServiceResult> GetListCleaningOptionByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var cleaningOptions = await repo.GetListCleaningOptionByAnimalTypeId(animalTypeId);
                if (cleaningOptions == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListCleaninOptionView(cleaningOptions);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "CleaningOptions",
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
        public async Task<ServiceResult> GetCleaningOptionById(int id)
        {
            try
            {
                var cleaningOption = repo.GetById(id);
                if (cleaningOption == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetCleaningOptionView(cleaningOption);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "CleaningOption",
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
        public async Task<ServiceResult> AddCleaningOption(CleaningOptionAdd key)
        {
            try
            {
                var cleaningOptions = (await repo.GetListCleaningOption()).FindAll(l => l.Name.ToLower() == key.Name.ToLower());
                if (cleaningOptions.Count > 0)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Cleaning Option Name is Exist",
                    };
                }
                var cleaningOption = await repo.AddCleaningOption(key);
                int stepNumber = 0;
                foreach(var item1 in key.CleaningProcesss)
                {
                    stepNumber++;
                    var cleaningProcess = await cleaningProcessRepo.AddCleaningProcess(cleaningOption.Id,stepNumber,item1);
                    foreach(var item2 in item1.UrlProcesss)
                    {
                        await urlProcessRepo.AddUrlProcess(cleaningProcess.Id, item2);
                    }
                }
                var result = await objectViewService.GetCleaningOptionView(cleaningOption);
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
        public async Task<ServiceResult> DisableCleaningOption(List<int> cleaningOptinoIds)
        {
            try
            {
                cleaningOptinoIds = cleaningOptinoIds.Distinct().ToList();
                List<int> unsucessIds = new List<int>();
                foreach (int cleaningOptinoId in cleaningOptinoIds)
                {
                    var cleaningTasks = await taskCleaningRepo.GetListTaskCleaningByCleaningOptionId(cleaningOptinoId);
                    if (cleaningTasks?.Count > 0)
                    {
                       
                    }
                    else
                    {
                        if((await repo.DisableCleaningOption(cleaningOptinoId)) == 0)
                            unsucessIds.Add(cleaningOptinoId);
                    }
                }

                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Disable Success with id unsuccess {string.Join(", ", unsucessIds)}",
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
        /*public async Task<ServiceResult> UpdateFood(FoodUpdate key)
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
        public async Task<ServiceResult> DeleteFood(int foodId)
        {
            try
            {
                var food = repo.GetById(foodId);
                if (food == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                await repo.RemoveAsync(food);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Delete Success",
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
        }*/
    }
}
