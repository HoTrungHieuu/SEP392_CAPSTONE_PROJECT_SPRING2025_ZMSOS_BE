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
    public interface IAnimalTypeRepository : IGenericRepository<AnimalType>
    {
        public Task<List<AnimalType>?> GetListAnimalType();
        public Task<AnimalType> AddAnimalType(AnimalTypeAdd key);
        public Task<AnimalType?> UpdateAnimalType(AnimalTypeUpdate key);
        public List<AnimalTypeView> ConvertListAnimalTypeIntoListAnimalTypeView(List<AnimalType> animalTypes);
        public AnimalTypeView ConvertAnimalTypeIntoAnimalTypeView(AnimalType animalType);
    }
}
