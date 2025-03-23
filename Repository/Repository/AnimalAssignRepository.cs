using BO.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AnimalAssignRepository : GenericRepository<AnimalAssign>, IAnimalAssignRepository
    {
        public async Task<List<AnimalAssign>?> GetListAnimalAssignByTaskId(int taskId)
        {
            try
            {
                var animalAssigns = (await GetAllAsync()).FindAll(l => l.TaskId == taskId);
                return animalAssigns;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AnimalAssign?> AddAnimalAssign(int taskId, int animalCageId)
        {
            try
            {
                AnimalAssign animalAssign = new()
                {
                    TaskId = taskId,
                    AnimalCageId = animalCageId,
                };
                await CreateAsync(animalAssign);
                return animalAssign;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
