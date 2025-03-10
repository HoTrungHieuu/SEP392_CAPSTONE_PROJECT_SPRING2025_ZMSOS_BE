using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IIncompatibleAnimalTypeRepository : IGenericRepository<IncompatibleAnimalType>
    {
        public Task<List<IncompatibleAnimalType>?> GetListIncompatibleAnimalType();
        public Task<List<IncompatibleAnimalType>?> GetListIncompatibleAnimalTypeByAnimalTypeId(int animalTypeId);
        public Task<bool> CheckIncompatibleAnimalType(int animalTypeId1, int animalTypeId2);
        public Task<IncompatibleAnimalType> AddIncompatibleAnimalType(IncompatibleAnimalTypeAdd key);
        public IncompatibleAnimalTypeView ConvertIncompatibleAnimalTypeIntoIncompatibleAnimalTypeView(IncompatibleAnimalType incompatibleAnimalType, AnimalTypeView animalType1, AnimalTypeView animalType2, StatusView? status);


    }
}
