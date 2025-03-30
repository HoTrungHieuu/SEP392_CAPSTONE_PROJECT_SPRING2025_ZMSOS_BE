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
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.IO;
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

                // Create new Excel workbook (XSSF for .xlsx format)
                IWorkbook workbook = new XSSFWorkbook();
                try
                {
                    ISheet sheet = workbook.CreateSheet("Animals");

                    // Create header style
                    IFont headerFont = workbook.CreateFont();
                    headerFont.IsBold = true;
                    headerFont.FontHeightInPoints = 12;

                    ICellStyle headerStyle = workbook.CreateCellStyle();
                    headerStyle.SetFont(headerFont);
                    headerStyle.FillForegroundColor = IndexedColors.LightBlue.Index;
                    headerStyle.FillPattern = FillPattern.SolidForeground;

                    // Create date style
                    ICellStyle dateStyle = workbook.CreateCellStyle();
                    IDataFormat dateFormat = workbook.CreateDataFormat();
                    dateStyle.DataFormat = dateFormat.GetFormat("dd/MM/yyyy");

                    // Create header row
                    IRow headerRow = sheet.CreateRow(0);
                    string[] headers = {
        "Id", "Animal_Type_Name", "Name", "Description",
        "Arrival_Date", "Classify", "Created_Date", "Updated_Date",
        "Birth_Date", "Age", "Gender", "Weight", "Height",
        "Individual_Notes", "Individual_Status", "Quantity",
        "Flock_Notes", "Flock_Status"
    };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        ICell cell = headerRow.CreateCell(i);
                        cell.SetCellValue(headers[i]);
                        cell.CellStyle = headerStyle;
                    }

                    // Add data rows
                    int rowNum = 1;
                    foreach (var animal in result)
                    {
                        IRow row = sheet.CreateRow(rowNum++);

                        // Set cell values with proper null checks
                        row.CreateCell(0).SetCellValue(animal.Id.ToString());
                        SetCellValue(row, 1, animal?.AnimalType?.VietnameseName);
                        SetCellValue(row, 2, animal?.Name);
                        SetCellValue(row, 3, animal?.Description);

                        // Handle date fields with proper formatting
                        SetCellValue(row, 4, animal?.ArrivalDate.ToString());
                        SetCellValue(row, 5, animal?.Classify);
                        SetCellValue(row, 6, animal?.DateCreated.ToString());
                        SetCellValue(row, 7, animal?.DateUpdated.ToString());
                        SetCellValue(row, 8, animal?.Individual?.BirthDate.ToString());

                        SetCellValue(row, 9, animal?.Individual?.Age);
                        SetCellValue(row, 10, animal?.Individual?.Gender);
                        SetCellValue(row, 11, animal?.Individual?.Weight);
                        SetCellValue(row, 12, animal?.Individual?.Height);
                        SetCellValue(row, 13, animal?.Individual?.Note);
                        SetCellValue(row, 14, animal?.Individual?.Status);
                        SetCellValue(row, 15, animal?.Flock?.Quantity.ToString());
                        SetCellValue(row, 16, animal?.Flock?.Note);
                        SetCellValue(row, 17, animal?.Flock?.Status);
                    }

                    // Auto-size all columns
                    for (int i = 0; i < headers.Length; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                    using var tempStream = new MemoryStream();
                    workbook.Write(tempStream);

                    // Tạo stream mới độc lập
                    var outputStream = new MemoryStream(tempStream.ToArray());
                    outputStream.Position = 0;

                    return outputStream;
                }
                finally
                {
                    workbook.Close();
                }
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
        private string? GetCellValue(ICell cell)
        {
            if (cell == null)
                return null;
            return cell.StringCellValue;
        }
        public async Task<ServiceResult> ImportAnimals(Stream stream)
        {
            IWorkbook workbook = new XSSFWorkbook(stream);
           
            ISheet sheet = workbook.GetSheetAt(0); // Lấy sheet đầu tiên
            if (sheet == null)
                throw new Exception("Không tìm thấy sheet nào trong file");

            ServiceResult serviceResult = new();
            for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                try
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row == null) continue; // Bỏ qua dòng trống
                    string? animalTypeName = GetCellValue(row.GetCell(0));
                    string? name = GetCellValue(row.GetCell(1));
                    string? description = GetCellValue(row.GetCell(2));
                    string? arrivalDate = GetCellValue(row.GetCell(3));
                    string? classify = GetCellValue(row.GetCell(4));
                    string? birthDate = GetCellValue(row.GetCell(5));
                    string? age = GetCellValue(row.GetCell(6));
                    string? gender = GetCellValue(row.GetCell(7));
                    string? weigtht = GetCellValue(row.GetCell(8));
                    string? height = GetCellValue(row.GetCell(9));
                    string? quantity = GetCellValue(row.GetCell(10));

                    int? animalTypeId = null;
                    if(animalTypeName != "")
                    {
                        var animalType = (await animalTypeRepo.GetAllAsync()).FirstOrDefault(l => l.VietnameseName == animalTypeName);
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
                    serviceResult = await AddAnimal(key);
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi và tiếp tục
                    Console.WriteLine($"Lỗi dòng {rowIndex + 1}: {ex.Message}");
                }
            }
            return serviceResult;
        }

    }
}
