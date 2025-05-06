using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAnimalRepository : IGenericRepository<Animal>
    {
        public Task<List<Animal>?> GetListAnimal();
        public Task<List<Animal>?> GetListAnimalByAnimalTypeId(int animalTypeId);
        public Task<Animal> AddAnimal(AnimalAdd key);
        public Task<Animal?> UpdateAnimal(AnimalUpdate key);
        public Task<Animal?> DisableAnimal(int id);
        public AnimalView ConvertAnimalIntoAnimalView(Animal animal, AnimalTypeView animalType, FlockView flock, IndividualView individual, CageView cage, List<string> urlImages);
    }
}
