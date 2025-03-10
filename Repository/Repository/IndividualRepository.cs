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
    public class IndividualRepository : GenericRepository<Individual>, IIndividualRepository
    {
        public IndividualRepository()
        {
        }
        public async Task<Individual?> GetIndividualByAnimalId(int animalId)
        {
            try
            {
                var individual = (await GetAllAsync()).FirstOrDefault(l => l.AnimalId == animalId);
                return individual;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Individual> AddIndividual(int animalId, IndividualAdd key)
        {
            try
            {
                Individual individual = new()
                {
                    AnimalId = animalId,
                    BirthDate = key.BirthDate,
                    Name = key.Name,
                    Age = key.Age,
                    Gender = key.Gender,
                    Weight = key.Weight,
                    Height = key.Height,
                    ArrivalDate = key.ArrivalDate,
                    Notes = key.Note,
                    Status = null,
                };
                await CreateAsync(individual);
                return individual;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Individual?> UpdateIndividual(int animalId, IndividualUpdate key)
        {
            try
            {
                var individual = await GetIndividualByAnimalId(animalId);
                if (individual == null) return null;
                individual.BirthDate = key.BirthDate;
                individual.Name = key.Name;
                individual.Age = key.Age;
                individual.Gender = key.Gender;
                individual.Weight = key.Weight;
                individual.Height = key.Height;
                individual.ArrivalDate = key.ArrivalDate;
                individual.Notes = key.Note;
                await UpdateAsync(individual);
                return individual;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IndividualView ConvertIndividualIntoIndividualView(Individual individual, StatusView? status)
        {
            try
            {
                IndividualView result = new IndividualView();
                result.ConvertIndividualIntoIndividualView(individual, status);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
