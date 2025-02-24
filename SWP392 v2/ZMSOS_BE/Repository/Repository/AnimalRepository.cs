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
    public class AnimalRepository : GenericRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository()
        {
        }
        public async Task<List<Animal>?> GetListAnimal()
        {
            try
            {
                var animals = await GetAllAsync();
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
                var animals = (await GetAllAsync()).FindAll(l => l.AnimalTypeId == animalTypeId);
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
                    Age = key.Age,
                    Gender = key.Gender,
                    Weight = key.Weight,
                    Notes = key.Notes,
                    ArrivalDate = DateOnly.FromDateTime(DateTime.Now),
                    Status = "",
                    UrlImage = key.UrlImage,
                };
                await CreateAsync(animal);
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
                animal.AnimalTypeId = key.AnimalTypeId;
                animal.Name = key.Name;
                animal.Description = key.Description;
                animal.Age = key.Age;
                animal.Gender = key.Gender;
                animal.Weight = key.Weight;
                animal.Notes = key.Notes;
                animal.Status = key.Status;
                animal.UrlImage = key.UrlImage;
                await UpdateAsync(animal);
                return animal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<AnimalView> ConvertListAnimalIntoListAnimalView(List<Animal> animals, List<AnimalTypeView> animalTypes)
        {
            try
            {
                List<AnimalView> result = new();
                for (int i = 0; i < animals.Count; i++)
                {
                    result.Add(ConvertAnimalIntoAnimalView(animals[i], animalTypes[i]));
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public AnimalView ConvertAnimalIntoAnimalView(Animal animal, AnimalTypeView animalType)
        {
            try
            {
                AnimalView result = new AnimalView();
                result.ConvertAnimalIntoAnimalView(animal, animalType);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
