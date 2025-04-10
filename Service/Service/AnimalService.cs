using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Nest;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AnimalService :IAnimalService
    {
        public IAnimalRepository repo;
        public IAnimalTypeRepository typeRepo;
        public ICageRepository cageRepo;
        public IZooAreaRepository zooAreaRepo;
        public IAnimalCageRepository animalCageRepo;
        public IFlockRepository flockRepo;
        public IIndividualRepository individualRepo;
        public IAnimalImageRepository animalImageRepo;
        public IIncompatibleAnimalTypeRepository incompatibleAnimalTypeRepo;
        public IObjectViewService objectViewService;
        public IAnimalTypeRepository animalTypeRepo;
        public AnimalService(IAnimalRepository repo, IAnimalTypeRepository typeRepo, 
            IAnimalCageRepository animalCageRepo, ICageRepository cageRepo, IObjectViewService objectViewService, 
            IFlockRepository flockRepo, IIndividualRepository individualRepo, 
            IAnimalImageRepository animalImageRepo,
            IIncompatibleAnimalTypeRepository incompatibleAnimalTypeRepo,
            IZooAreaRepository zooAreaRepo, IAnimalTypeRepository animalTypeRepo)
        {
            this.repo = repo;
            this.typeRepo = typeRepo;
            this.animalCageRepo = animalCageRepo;
            this.cageRepo = cageRepo;
            this.flockRepo = flockRepo;
            this.individualRepo = individualRepo;
            this.animalImageRepo = animalImageRepo;
            this.objectViewService = objectViewService;
            this.incompatibleAnimalTypeRepo = incompatibleAnimalTypeRepo;
            this.zooAreaRepo = zooAreaRepo;
            this.animalTypeRepo = animalTypeRepo;
        }
        public async Task<ServiceResult> GetListAnimal()
        {
            try
            {
                var animals = await repo.GetListAnimal();
                if (animals == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListAnimalView(animals);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Animals",
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
        public async Task<ServiceResult> GetListAnimalSearching(AnimalSearch<AnimalView> key)
        {
            try
            {
                var animals = await repo.GetListAnimal();
                if (animals == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListAnimalView(animals);
                if (key.AnimalTypeId != null)
                {
                    result = result.FindAll(l => l.AnimalType?.Id == key.AnimalTypeId);
                }
                if(key.Classify == "Flock")
                {
                    result = result.FindAll(l => l.Classify == key.Classify);
                }
                else if (key.Classify == "Individual")
                {
                    result = result.FindAll(l => l.Classify == key.Classify);
                    if (key.Sorting?.PropertySort == "Age")
                    {
                        if (key.Sorting.IsAsc)
                        {
                            result.OrderBy(l => l.Individual?.Age);
                        }
                        else
                        {
                            result.OrderByDescending(l => l.Individual?.Age);
                        }
                    }
                }
                if(key.Sorting?.PropertySort == "Id")
                {
                    if (key.Sorting.IsAsc)
                    {
                        result.OrderBy(l => l.Id);
                    }
                    else
                    {
                        result.OrderByDescending(l => l.Id);
                    }
                }
                int? totalNumberPaging = null;
                if (key.Paging != null)
                {
                    Paging<AnimalView> paging = new();
                    totalNumberPaging = paging.MaxPageNumber(result, key.Paging.PageSize);
                    result = paging.PagingList(result, key.Paging.PageSize, key.Paging.PageNumber);
                }
                if (totalNumberPaging == null) totalNumberPaging = 1;
                return new ServiceResult
                {
                    Status = 200,
                    Message = totalNumberPaging.ToString(),
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
        public async Task<ServiceResult> GetListAnimalByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var type = typeRepo.GetById(animalTypeId);
                if (type == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var animals = await repo.GetListAnimalByAnimalTypeId(animalTypeId);
                if (animals == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListAnimalView(animals);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Animals",
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
        public async Task<ServiceResult> GetListAnimalByCageId(int cageId)
        {
            try
            {
                var cage = cageRepo.GetById(cageId);
                if(cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Cage Not Found!",
                    };
                }
                var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cageId);
                List<Animal> animals = new List<Animal>();
                foreach(var animalCage in animalCages)
                {
                    animals.Add(repo.GetById(animalCage.AnimalId));
                }

                var result = await objectViewService.GetListAnimalView(animals);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Animals",
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
        public async Task<ServiceResult> GetListAnimalByZooAreaId(int zooAreaId)
        {
            try
            {
                var zooArea = zooAreaRepo.GetById(zooAreaId);
                if (zooArea == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "ZooArea Not Found!",
                    };
                }
                var cages = await cageRepo.GetListCageByAreaId(zooAreaId);
                List<Animal> animals = new List<Animal>();
                foreach (var cage in cages)
                {
                    var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cage.Id);
                    foreach (var animalCage in animalCages)
                    {
                        animals.Add(repo.GetById(animalCage.AnimalId));
                    }
                }
                var result = await objectViewService.GetListAnimalView(animals);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Animals",
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
        public async Task<ServiceResult> GetAnimalById(int id)
        {
            try
            {
                var animal = repo.GetById(id);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetAnimalView(animal);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Animal",
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
        public async Task<ServiceResult> AddAnimal(AnimalAdd key)
        {
            try
            {
                if(key.Classify != "Individual" && key.Classify != "Flock")
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "classify is invalid",
                    };
                }
                var animal = await repo.AddAnimal(key);
                if(animal.Classify == "Flock")
                {
                    await flockRepo.AddFlock(animal.Id ,key.Flock);
                }
                else if(animal.Classify == "Individual")
                {
                    await individualRepo.AddIndividual(animal.Id, key.Individual);
                }
                await animalImageRepo.AddAnimalImageByAnimalId(animal.Id, key.UrlImages);
                var result = await objectViewService.GetAnimalView(animal);
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
                    Message = ex.Message.ToString(),
                };
            }
        }
        public async Task<ServiceResult> UpdateAnimal(AnimalUpdate key)
        {
            try
            {
                var animal = await repo.UpdateAnimal(key);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                if (animal.Classify == "Flock")
                {
                    await flockRepo.UpdateFlock(animal.Id, key.Flock);
                    var animalCage = await animalCageRepo.GetAnimalCageCurrentByAnimalId(animal.Id);
                    if(animalCage != null)
                    {
                        var cage = cageRepo.GetById(animalCage.CageId);
                        var flock = await flockRepo.GetFlockByAnimalId(animal.Id);
                        cage.CurrentQuantity = flock.Quantity;
                        await cageRepo.UpdateAsync(cage);
                    }
                    
                }
                else if (animal.Classify == "Individual")
                {
                    await individualRepo.UpdateIndividual(animal.Id, key.Individual);
                }
                await animalImageRepo.DeleteAnimalImageByAnimalId(animal.Id);
                await animalImageRepo.AddAnimalImageByAnimalId(animal.Id, key.UrlImages);
                var result = await objectViewService.GetAnimalView(animal);
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
        public async Task<ServiceResult> DeleteAnimal(int animalId)
        {
            try
            {
                var animal = repo.GetById(animalId);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                if (animal.Classify == "Flock")
                {
                    var flock = await flockRepo.GetFlockByAnimalId(animalId);
                    await flockRepo.RemoveAsync(flock);
                }
                else if (animal.Classify == "Individual")
                {
                    var individual = await individualRepo.GetIndividualByAnimalId(animalId);
                    await individualRepo.RemoveAsync(individual);
                }
                await animalImageRepo.DeleteAnimalImageByAnimalId(animal.Id);
                await repo.RemoveAsync(animal);
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
                    Message = ex.Message.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddAnimalCage(int animalId, int cageId)
        {
            try
            {
                var cage = cageRepo.GetById(cageId);
                if(cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Cage Not Found"
                    };
                }
                if(cage.Classify == "Individual" && cage.CurrentQuantity >= cage.MaxQuantity)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Cage is Max"
                    };
                }

                var animal = repo.GetById(animalId);
                if(cage.Classify != animal.Classify || cage.Classify == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Not Same Classify"
                    };
                }

                if(animal.Classify == "Individual")
                {
                    var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cageId);
                    bool incompatible = false;
                    foreach (var animalCageTemp in animalCages)
                    {
                        var animal1 = repo.GetById(animalCageTemp.AnimalId);
                        if ((await incompatibleAnimalTypeRepo.CheckIncompatibleAnimalType((int)animal1.AnimalTypeId, (int)animal.AnimalTypeId)) == true)
                        {
                            incompatible = true;
                            break;
                        }
                    }
                    if (incompatible == false)
                    {
                        var animalCage = await animalCageRepo.AddAnimalCage(animalId, cageId);
                        if (animalCage == null)
                        {
                            return new ServiceResult
                            {
                                Status = 200,
                                Message = "Add Fail",
                            };
                        }
                        cage.CurrentQuantity += 1;
                    }
                    else
                    {
                        return new ServiceResult
                        {
                            Status = 400,
                            Message = "Cage Has Animal Incompatible",
                        };
                    }
                    await cageRepo.UpdateAsync(cage);
                }
                else if(animal.Classify == "Flock")
                {
                    var animalCages = await animalCageRepo.GetListAnimalCageByCageId(cageId);
                    if(animalCages?.Count > 0)
                    {
                        return new ServiceResult
                        {
                            Status = 400,
                            Message = "Cage Had Animal",
                        };
                    }
                    var animalCage = await animalCageRepo.AddAnimalCage(animalId, cageId);
                    cage.CurrentQuantity = (await flockRepo.GetFlockByAnimalId(animal.Id))?.Quantity;
                    await cageRepo.UpdateAsync(cage);
                    if (animalCage == null)
                    {
                        return new ServiceResult
                        {
                            Status = 200,
                            Message = "Add Fail",
                        };
                    }
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
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
        public async Task<ServiceResult> RemoveAnimalCage(int animalId, int cageId)
        {
            try
            {
                var cage = cageRepo.GetById(cageId);
                if (cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,   
                        Message = "Cage Not Found"
                    };
                }
                var animal = repo.GetById(animalId);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Animal Not Found"
                    };
                }
                var animalCage = await animalCageRepo.RemoveAnimalCage(animalId, cageId);
                if (animalCage == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Animal Not In Cage"
                    };
                }
                if(cage.Classify == "Individual")
                {
                    cage.CurrentQuantity -= 1;
                    await cageRepo.UpdateAsync(cage);
                }
                else if(cage.Classify == "Flock")
                {
                    cage.CurrentQuantity = null;
                    await cageRepo.UpdateAsync(cage);
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Remove Success",
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
        public async Task<ServiceResult> ReplaceAnimalCage(int animalId, int cageId)
        {
            try
            {
                var cage = cageRepo.GetById(cageId);
                if (cage == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Cage Not Found"
                    };
                }
                var animal = repo.GetById(animalId);
                if (animal == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Animal Not Found"
                    };
                }
                var animalCage = await animalCageRepo.GetAnimalCageCurrentByAnimalId(animalId);
                animalCage = await animalCageRepo.RemoveAnimalCage(animalId, (int)animalCage.CageId);
                if (animalCage == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Animal Not In Cage"
                    };
                }
                animalCage = await animalCageRepo.AddAnimalCage(animalId, cageId);
                if (animalCage == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Fail"
                    };
                }
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Success",
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
        public async Task<MemoryStream> ExportListAnimal()
        {
            try
            {
                
                var animals = await repo.GetListAnimal();
                var result = await objectViewService.GetListAnimalView(animals);

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
            "Id", "Animal_Type_Name", "Name", "Description",
            "Arrival_Date", "Classify", "Created_Date", "Updated_Date",
            "Birth_Date", "Age", "Gender", "Weight", "Height",
            "Individual_Notes", "Individual_Status", "Quantity",
            "Flock_Notes", "Flock_Status"
        };
                for (int i = 0; i < headers.Length; i++)
                {
                    sheet.Cells[1, i + 1].Value = headers[i];
                }
                int row = 2;
                foreach (var animal in result)
                {
                    sheet.Cells[row, 1].Value = animal.Id.ToString();
                    sheet.Cells[row, 2].Value = animal?.AnimalType?.VietnameseName ?? "";
                    sheet.Cells[row, 3].Value = animal?.Name ?? "";
                    sheet.Cells[row, 4].Value = animal?.Description ?? "";
                    sheet.Cells[row, 5].Value = animal?.ArrivalDate.ToString() ?? "";
                    sheet.Cells[row, 6].Value = animal?.Classify ?? "";
                    sheet.Cells[row, 7].Value = animal?.DateCreated.ToString() ?? "";
                    sheet.Cells[row, 8].Value = animal?.DateUpdated.ToString() ?? "";
                    sheet.Cells[row, 9].Value = animal?.Individual?.BirthDate.ToString() ?? "";
                    sheet.Cells[row, 10].Value = animal?.Individual?.Age ?? "";
                    sheet.Cells[row, 11].Value = animal?.Individual?.Gender ?? "";
                    sheet.Cells[row, 12].Value = animal?.Individual?.Weight ?? "";
                    sheet.Cells[row, 13].Value = animal?.Individual?.Height ?? "";
                    sheet.Cells[row, 14].Value = animal?.Individual?.Note ?? "";
                    sheet.Cells[row, 15].Value = animal?.Individual?.Status ?? "";
                    sheet.Cells[row, 16].Value = animal?.Flock?.Quantity.ToString() ?? "";
                    sheet.Cells[row, 17].Value = animal?.Flock?.Note ?? "";
                    sheet.Cells[row, 18].Value = animal?.Flock?.Status ?? "";

                    // Xử lý ngày tháng
                    if (animal.ArrivalDate != null)
                    {
                        sheet.Cells[row, 5].Value = animal.ArrivalDate;
                        sheet.Cells[row, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                    }

                    // Các cột khác tương tự...
                    row++;
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
        private void SetCellValue(IRow row, int columnIndex, string value)
        {
            row.CreateCell(columnIndex).SetCellValue(value ?? string.Empty);
        }
        private string? GetCellValue(ExcelRange cell)
        {
            return cell.Value?.ToString()?.Trim();
        }
        public async Task<ServiceResult> ImportAnimals(Stream stream)
        {
            using var excelPackage = new ExcelPackage(stream);

            var worksheet = excelPackage.Workbook.Worksheets[0];
            int row = 2;
            while (worksheet.Cells[row, 1].Value != null)
            {
                try
                {
                    string? animalTypeName = GetCellValue(worksheet.Cells[row, 1]);
                    string? name = GetCellValue(worksheet.Cells[row, 2]);
                    string? description = GetCellValue(worksheet.Cells[row, 3]);
                    string? arrivalDate = GetCellValue(worksheet.Cells[row, 4]);
                    string? classify = GetCellValue(worksheet.Cells[row, 5]);
                    string? birthDate = GetCellValue(worksheet.Cells[row, 6]);
                    string? age = GetCellValue(worksheet.Cells[row, 7]);
                    string? gender = GetCellValue(worksheet.Cells[row, 8]);
                    string? weigtht = GetCellValue(worksheet.Cells[row, 9]);
                    string? height = GetCellValue(worksheet.Cells[row, 10]);
                    string? quantity = GetCellValue(worksheet.Cells[row, 11]);

                    int? animalTypeId = null;
                    if (animalTypeName != "")
                    {
                        var animalType = (await animalTypeRepo.GetAllAsync()).FirstOrDefault(l => l.VietnameseName == animalTypeName);
                        animalTypeId = animalType?.Id;
                    }
                    AnimalAdd key = new()
                    {
                        AnimalTypeId = (int)animalTypeId,
                        Name = (name == "") ? null : name,
                        Description = (description == "") ? null : description,
                        ArrivalDate = (arrivalDate == "") ? null : DateOnly.Parse(arrivalDate),
                        Classify = (classify == "") ? null : classify,
                        Individual = new()
                        {
                            BirthDate = (birthDate == "") ? null : DateOnly.Parse(birthDate),
                            Age = (age == "") ? null : age,
                            Gender = (gender == "") ? null : gender,
                            Weight = (weigtht == "") ? null : weigtht,
                            Height = (height == "") ? null : height,
                        },
                        Flock = new()
                        {
                            Quantity = (quantity == "") ? null : int.Parse(quantity),
                        }
                    };
                    await AddAnimal(key);

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
            return new ServiceResult()
            {
                Status = 200,
                Message = "Import Success",
            };
        }

    }
}
