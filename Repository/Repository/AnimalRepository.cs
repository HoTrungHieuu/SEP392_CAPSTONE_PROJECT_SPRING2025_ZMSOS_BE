using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Microsoft.Extensions.Caching.Memory;
using Repository.IRepository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AnimalRepository : GenericRepository<Animal>, IAnimalRepository
    {
        private readonly IMemoryCache _memoryCache;
        private const string AnimalListKey = "animal_list";

        public AnimalRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<List<Animal>?> GetListAnimal()
        {
            try
            {
                List<Animal> animals = new();
                if (_memoryCache.TryGetValue("animal_list", out var list))
                {
                    animals = (List<Animal>)list;
                }
                else
                {
                    animals = await GetAllAsync();
                    _memoryCache.Set("animal_list", animals, TimeSpan.FromMinutes(5));
                }
                animals = animals.OrderByDescending(l => l.CreatedDate).ToList();
                return animals;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Animal>?> GetListAnimalByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var animals = (await GetListAnimal()).FindAll(l => l.AnimalTypeId == animalTypeId);
                return animals;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Animal> AddAnimal(AnimalAdd key)
        {
            try
            {
                Animal animal = new()
                {
                    AnimalTypeId = key.AnimalTypeId,
                    Name = key.Name,
                    Description = key.Description,
                    ArrivalDate = key.ArrivalDate,
                    Classify = key.Classify,
                    CreatedDate = VietNamTime.GetVietNamTime(),
                };
                await CreateAsync(animal);
                _memoryCache.Remove("animal_list");
                return animal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Animal?> UpdateAnimal(AnimalUpdate key)
        {
            try
            {
                var animal = GetById(key.Id);
                if (animal == null) return null;
                animal.Name = key.Name;
                animal.Description = key.Description;
                animal.Classify = key.Classify;
                animal.UpdatedDate = VietNamTime.GetVietNamTime();
                await UpdateAsync(animal);
                _memoryCache.Remove("animal_list");
                return animal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public AnimalView ConvertAnimalIntoAnimalView(Animal animal, AnimalTypeView animalType, FlockView flock, IndividualView individual, CageView cage, List<string> urlImages)
        {
            try
            {
                AnimalView result = new AnimalView();
                result.ConvertAnimalIntoAnimalView(animal, animalType,flock, individual,cage,urlImages);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
