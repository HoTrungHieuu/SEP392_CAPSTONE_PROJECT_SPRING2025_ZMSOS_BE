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
    public interface IIndividualRepository : IGenericRepository<Individual>
    {
        public Task<Individual?> GetIndividualByAnimalId(int animalId);
        public Task<Individual> AddIndividual(int animalId, IndividualAdd key);
        public Task<Individual?> UpdateIndividual(int animalId, IndividualUpdate key);
        public IndividualView ConvertIndividualIntoIndividualView(Individual individual);
    }
}
