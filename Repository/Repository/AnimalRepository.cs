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
                    Description = key.Description,
                    Classify = key.Classify,
                    Status = key.Status
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
                animal.Description = key.Description;
                animal.Classify = key.Classify;
                animal.Status = key.Status;
                await UpdateAsync(animal);
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
