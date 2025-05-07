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
    public class FlockRepository : GenericRepository<Flock>, IFlockRepository
    {
        public FlockRepository()
        {
        }
        public async Task<Flock?> GetFlockByAnimalId(int animalId)
        {
            try
            {
                var flock = (await GetAllAsync()).FirstOrDefault(l=>l.AnimalId == animalId);
                return flock;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Flock> AddFlock(int animalId, FlockAdd key)
        {
            try
            {
                Flock flock = new()
                {
                    AnimalId = animalId,
                    Quantity = key.Quantity,
                    Notes = key.Note,
                    Status = key.Status
                };
                await CreateAsync(flock);
                return flock;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Flock?> UpdateFlock(int animalId, FlockUpdate key)
        {
            try
            {
                var flock = await GetFlockByAnimalId(animalId);
                if (flock == null) return null;
                flock.Quantity = key.Quantity;
                flock.Notes = key.Note;
                flock.Status = key.Status;
                await UpdateAsync(flock);
                return flock;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public FlockView ConvertFlockIntoFlockView(Flock flock)
        {
            try
            {
                FlockView result = new FlockView();
                result.ConvertFlockIntoFlockView(flock);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
