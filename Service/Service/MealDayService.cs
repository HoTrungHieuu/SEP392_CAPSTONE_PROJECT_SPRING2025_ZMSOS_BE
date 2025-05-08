using DAO.AddModel;
using DAO.UpdateModel;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO.Models;

namespace Service.Service
{
    public class MealDayService : IMealDayService
    {
        public IMealDayRepository repo;
        public IMealFoodRepository mealFoodRepo;
        public IAnimalTypeRepository animalTypeRepo;
        public IFoodRepository foodRepo;
        public ITaskMealRepository taskMealRepo;
        public IObjectViewService objectViewService;
        public MealDayService(IMealDayRepository repo, IObjectViewService objectViewService, IMealFoodRepository mealFoodRepo, IFoodRepository foodRepo, ITaskMealRepository taskMealRepo, IAnimalTypeRepository animalTypeRepo)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
            this.mealFoodRepo = mealFoodRepo;
            this.foodRepo = foodRepo;
            this.taskMealRepo = taskMealRepo;
            this.animalTypeRepo = animalTypeRepo;
        }
        public async Task<ServiceResult> GetListMealDay()
        {
            try
            {
                var mealDays = await repo.GetListMealDay();
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
                var mealDays = (await repo.GetListMealDay()).FindAll(l => l.Name.ToLower() == key.Name.ToLower());
                if (mealDays.Count > 0)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Meal Day Name is Exist",
                    };
                }
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
                var mealDays = (await repo.GetListMealDay()).FindAll(l => l.Id != key.Id && l.Name.ToLower() == key.Name.ToLower());
                if (mealDays.Count > 0)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Meal Day Name is Exist",
                    };
                }
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
        public async Task<ServiceResult> DisableMealDay(List<int> mealDayIds)
        {
            try
            {
                mealDayIds = mealDayIds.Distinct().ToList();
                List<int> unsucessIds = new List<int>();
                foreach (int mealDayId in mealDayIds)
                {
                    var mealTask = await taskMealRepo.GetListTaskMealByMealDayId(mealDayId);
                    if (mealTask?.Count > 0)
                    {
                        unsucessIds.Add(mealDayId);
                    }
                    else
                    {
                        if((await repo.DisableMealDay(mealDayId)) == 0)
                            unsucessIds.Add(mealDayId);

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
        public async Task<MemoryStream> ExportListMealDay()
        {
            try
            {

                var mealDays = await repo.GetListMealDay();
                var result = await objectViewService.GetListMealDayView(mealDays);

                var foods = await foodRepo.GetListFood();
                List<(Food, int)> foodPositions = new();

                using var package = new ExcelPackage();
                var sheet = package.Workbook.Worksheets.Add("Animals");

                // Định dạng header
                using (var headerRange = sheet.Cells["A1:R1"])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                }

                // Đặt tên header
                string[] headers = {
            "Id", "Animal_Type_Name", "Name", "Total_Calo",
            "Period_Of_Time", "Time_Start_In_Day", "Time_End_In_Day", "Status","Food_Name","Quantitative"
        };
               

                for (int i = 0; i < headers.Length; i++)
                {
                    sheet.Cells[1, i + 1].Value = headers[i];
                }
                int row = 2;
                foreach (var mealDay in result)
                {
                    foreach (var food in mealDay.Foods)
                    {
                        sheet.Cells[row, 1].Value = mealDay.Id.ToString();
                        sheet.Cells[row, 2].Value = mealDay?.AnimalType?.VietnameseName ?? "";
                        sheet.Cells[row, 3].Value = mealDay?.Name ?? "";
                        sheet.Cells[row, 4].Value = mealDay?.TotalCalo.ToString() ?? "0";
                        sheet.Cells[row, 5].Value = mealDay?.PeriodOfTime.ToString() ?? "";
                        sheet.Cells[row, 6].Value = mealDay?.TimeStartInDay.ToString() ?? "";
                        sheet.Cells[row, 7].Value = mealDay?.TimeEndInDay.ToString() ?? "";
                        sheet.Cells[row, 8].Value = mealDay?.Status.ToString() ?? "";
                        sheet.Cells[row, 9].Value = food?.Food.Name.ToString() ?? "";
                        sheet.Cells[row, 10].Value = food?.Quantitative.ToString() + "gram" ?? "";
                        row++;
                    }
                }
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                // Trả về MemoryStream
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ServiceResult> ImportMealDays(Stream stream)
        {
            using var excelPackage = new ExcelPackage(stream);

            var worksheet = excelPackage.Workbook.Worksheets[0];
            int row = 2;
            (string?, string, string, string, string, List<(string, string)>) stringCells = new(null, "", "", "", "", new());
            while (worksheet.Cells[row, 1].Value != null)
            {
                try
                {
                    string? animalTypeName = GetCellValue(worksheet.Cells[row, 1]);
                    string? name = GetCellValue(worksheet.Cells[row, 2]);
                    string? periodOfTime = GetCellValue(worksheet.Cells[row, 3]);
                    string? timeStartInDay = GetCellValue(worksheet.Cells[row, 4]);
                    string? timeEndInDay = GetCellValue(worksheet.Cells[row, 5]);
                    string? foodName = GetCellValue(worksheet.Cells[row, 6]);
                    string? quantitative = GetCellValue(worksheet.Cells[row, 7]);

                    if (stringCells.Item1 == null)
                    {
                        stringCells.Item1 = animalTypeName;
                        stringCells.Item2 = name;
                        stringCells.Item3 = periodOfTime;
                        stringCells.Item4 = timeStartInDay;
                        stringCells.Item5 = timeEndInDay;
                        stringCells.Item6.Add((foodName, quantitative));
                    }
                    else if(animalTypeName != null && stringCells.Item1 == animalTypeName && stringCells.Item2 == name)
                    {
                        stringCells.Item6.Add((foodName, quantitative));
                    }
                    else if(stringCells.Item1 != animalTypeName || stringCells.Item2 != name)
                    {
                        int? animalTypeId = null;
                        if (stringCells.Item1 != "")
                        {
                            var animalType = (await animalTypeRepo.GetAllAsync()).FirstOrDefault(l => l.VietnameseName == stringCells.Item1);
                            animalTypeId = animalType?.Id;
                        }
                        List<MealFoodAdd> foodAdds = new();
                        foreach(var stringCell in stringCells.Item6)
                        {
                            int? foodId = (await foodRepo.GetListFood()).FirstOrDefault(l=>l.Name == stringCell.Item1).Id;
                            foodAdds.Add(new()
                            {
                                FoodId = foodId,
                                Quantitative = float.Parse(stringCell.Item2.Replace("gram", ""))
                            });

                        }
                        MealDayAdd key = new()
                        {
                            AnimalTypeId = (int)animalTypeId,
                            Name = (stringCells.Item2 == ""|| stringCells.Item2 == null) ? null : stringCells.Item2,
                            PeriodOfTime = (stringCells.Item3 == "" || stringCells.Item3 == null) ? null : TimeSpan.Parse(stringCells.Item3),
                            TimeStartInDay = (stringCells.Item4 == "" || stringCells.Item4 == null) ? null : TimeOnly.Parse(stringCells.Item4),
                            TimeEndInDay = (stringCells.Item5 == "" || stringCells.Item5 == null) ? null : TimeOnly.Parse(stringCells.Item5),
                            FoodsAdd = foodAdds,
                        };
                        await AddMealDay(key);

                        stringCells.Item1 = animalTypeName;
                        stringCells.Item2 = name;
                        stringCells.Item3 = periodOfTime;
                        stringCells.Item4 = timeStartInDay;
                        stringCells.Item5 = timeEndInDay;
                        stringCells.Item6.Add((foodName, quantitative));
                    }
                }
                catch (Exception ex)
                {
                    return new ServiceResult()
                    {
                        Status = 501,
                        Message = ex.Message,
                    };
                }

                row++;
            }
            int? animalTypeIdLast = null;
            if (stringCells.Item1 != "")
            {
                var animalType = (await animalTypeRepo.GetAllAsync()).FirstOrDefault(l => l.VietnameseName == stringCells.Item1);
                animalTypeIdLast = animalType?.Id;
            }
            List<MealFoodAdd> foodAddsLast = new();
            foreach (var stringCell in stringCells.Item6)
            {
                int? foodId = (await foodRepo.GetListFood()).FirstOrDefault(l => l.Name == stringCell.Item1).Id;
                foodAddsLast.Add(new()
                {
                    FoodId = foodId,
                    Quantitative = float.Parse(stringCell.Item2.Replace("gram", ""))
                });

            }
            MealDayAdd keyLast = new()
            {
                AnimalTypeId = (int)animalTypeIdLast,
                Name = (stringCells.Item2 == "") ? null : stringCells.Item2,
                PeriodOfTime = (stringCells.Item3 == "") ? null : TimeSpan.Parse(stringCells.Item3),
                TimeStartInDay = (stringCells.Item4 == "") ? null : TimeOnly.Parse(stringCells.Item4),
                TimeEndInDay = (stringCells.Item5 == "") ? null : TimeOnly.Parse(stringCells.Item5),
                FoodsAdd = foodAddsLast,
            };
            await AddMealDay(keyLast);
            return new ServiceResult()
            {
                Status = 200,
                Message = "Import Success",
            };
        }
        private string? GetCellValue(ExcelRange cell)
        {
            return cell.Value?.ToString()?.Trim();
        }
    }
}
