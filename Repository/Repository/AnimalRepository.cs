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
                    CreatedDate = DateTime.Now,
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
                animal.Name = key.Name;
                animal.Description = key.Description;
                animal.Classify = key.Classify;
                animal.UpdatedDate = DateTime.Now;
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
