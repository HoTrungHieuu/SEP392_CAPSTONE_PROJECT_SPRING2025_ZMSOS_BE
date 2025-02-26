using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAnimalCageRepository : IGenericRepository<AnimalCage>
    {
        public Task<List<AnimalCage>?> GetListAnimalCageByCageId(int cageId);
        public Task<List<int?>?> GetListAnimalCageIdByCageId(int cageId);
        public Task<int?> GetAnimalCageIdByCageIdAndAnimalId(int cageId, int animalId);
        public Task<AnimalCage?> AddAnimalCage(int animalId, int cageId);
        public Task<AnimalCage?> RemoveAnimalCage(int animalId, int cageId);
    }
}
