using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAnimalAssignRepository : IGenericRepository<AnimalAssign>
    {
        public Task<List<AnimalAssign>?> GetListAnimalAssignByTaskId(int taskId);
        public Task<AnimalAssign?> AddAnimalAssign(int taskId, int animalCageId);
    }
}
