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
    public class AnimalTypeRepository : GenericRepository<AnimalType>, IAnimalTypeRepository
    {
        public AnimalTypeRepository()
        {
        }
        public async Task<List<AnimalType>?> GetListAnimalType()
        {
            try
            {
                var animalTypes = await GetAllAsync();
                return animalTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AnimalType> AddAnimalType(AnimalTypeAdd key)
        {
            try
            {
                AnimalType animalType = new()
                {
                    ScientificName = key.ScientificName,
                    VietnameseName = key.VietnameseName,
                    EnglishName = key.EnglishName,
                    Family = key.Family,
                    WeightRange = key.WeightRange,
                    Characteristics = key.Characteristics,
                    Distribution = key.Distribution,
                    Habitat = key.Habitat,
                    Diet = key.Diet,
                    Reproduction = key.Reproduction,
                    ConservationStatus = key.ConservationStatus,
                    UrlImage = key.UrlImage,
                    CreatedDate = DateTime.Now
                };
                await CreateAsync(animalType);
                return animalType;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AnimalType?> UpdateAnimalType(AnimalTypeUpdate key)
        {
            try
            {
                var animalType = GetById(key.Id);
                if (animalType == null) return null;
                animalType.ScientificName = key.ScientificName;
                animalType.VietnameseName = key.VietnameseName;
                animalType.EnglishName = key.EnglishName;
                animalType.Family = key.Family;
                animalType.WeightRange = key.WeightRange;
                animalType.Characteristics = key.Characteristics;
                animalType.Distribution = key.Distribution;
                animalType.Habitat = key.Habitat;
                animalType.Diet = key.Diet;
                animalType.Reproduction = key.Reproduction;
                animalType.ConservationStatus = key.ConservationStatus;
                animalType.UrlImage = key.UrlImage;
                animalType.UpdatedDate = DateTime.Now;
                await UpdateAsync(animalType);
                return animalType;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public AnimalTypeView ConvertAnimalTypeIntoAnimalTypeView(AnimalType animalType)
        {
            try
            {
                AnimalTypeView result = new AnimalTypeView();
                result.ConvertAnimalTypeIntoAnimalTypeView(animalType);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
