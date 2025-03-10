using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAnimalImageRepository : IGenericRepository<AnimalImage>
    {
        public Task<List<AnimalImage>?> GetListAnimalImageByAnimalId(int animalId);
        public Task<List<string>?> GetListAnimalImageUrlByAnimalId(int animalId);
        public System.Threading.Tasks.Task AddAnimalImageByAnimalId(int animalId, List<string> listUrl);
        public System.Threading.Tasks.Task DeleteAnimalImageByAnimalId(int animalId);
    }
}
