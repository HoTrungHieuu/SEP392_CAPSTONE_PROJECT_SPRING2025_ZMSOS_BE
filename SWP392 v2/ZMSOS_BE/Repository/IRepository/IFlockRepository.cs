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
    public interface IFlockRepository : IGenericRepository<Flock>
    {
        public Task<Flock?> GetFlockByAnimalId(int animalId);
        public Task<Flock> AddFlock(int animalId, FlockAdd key);
        public Task<Flock?> UpdateFlock(int animalId, FlockUpdate key);
        public List<FlockView> ConvertListFlockIntoListFlockView(List<Flock> flocks);
        public FlockView ConvertFlockIntoFlockView(Flock flock);
    }
}
