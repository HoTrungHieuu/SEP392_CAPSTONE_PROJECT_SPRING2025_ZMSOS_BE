using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAnimalService
    {
        public Task<ServiceResult> GetListAnimal();
        public Task<ServiceResult> GetListAnimalByAnimalTypeId(int animalTypeId);
        public Task<ServiceResult> GetListAnimalByCageId(int cageId);
        public Task<ServiceResult> GetAnimalById(int id);
        public Task<ServiceResult> AddAnimal(AnimalAdd key);
        public Task<ServiceResult> UpdateAnimal(AnimalUpdate key);
        public Task<ServiceResult> AddAnimalCage(List<int> animalIds, int cageId);
        public Task<ServiceResult> RemoveAnimalCage(int animalId, int cageId);
        public Task<ServiceResult> ReplaceAnimalCage(int animalId, int cageId);
    }
}
