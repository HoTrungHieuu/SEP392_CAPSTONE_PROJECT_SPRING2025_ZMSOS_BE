using BO.Models;
using DAO.AddModel;
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
    public class CleaningOptionRepository : GenericRepository<CleaningOption>, ICleaningOptionRepository
    {
        public CleaningOptionRepository()
        {
        }
        public async Task<List<CleaningOption>?> GetListCleaningOptionByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var cleaningOption = (await GetAllAsync()).FindAll(l => l.AnimalTypeId == animalTypeId).FindAll(l=>l.Status != "Deleted");
                return cleaningOption;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CleaningOption> AddCleaningOption(CleaningOptionAdd key)
        {
            try
            {
                CleaningOption cleaningOption = new()
                {
                    AnimalTypeId = key.AnimalTypeId,
                    Name = key.Name,
                    Description = key.Description,
                    Status = "Active",
                    CreatedDate = DateTime.Now,
                };
                await CreateAsync(cleaningOption);
                return cleaningOption;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CleaningOption?> UpdateCleaningOption(CleaningOptionUpdate key)
        {
            try
            {
                var cleaningOption = GetById(key.Id);
                if (cleaningOption == null) return null;
                cleaningOption.Name = key.Name;
                cleaningOption.Description = key.Description;
                cleaningOption.UpdatedDate = DateTime.Now;
                await UpdateAsync(cleaningOption);
                return cleaningOption;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> DisableCleaningOption(int id)
        {
            try
            {
                var cleaningOption = GetById(id);
                if (cleaningOption == null) return 0;
                cleaningOption.Status = "Deleted";
                var row = await UpdateAsync(cleaningOption);
                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CleaningOptionView ConvertCleaningOptionIntoCleaningOptionView(CleaningOption cleaningOption, AnimalTypeView animalType, List<CleaningProcessView>? cleaningProcesss)
        {
            try
            {
                CleaningOptionView result = new CleaningOptionView();
                result.ConvertCleaningOptionIntoCleaningOptionView(cleaningOption, animalType,cleaningProcesss);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
