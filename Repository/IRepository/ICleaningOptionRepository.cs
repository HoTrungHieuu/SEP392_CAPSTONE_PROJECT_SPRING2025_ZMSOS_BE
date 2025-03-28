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
    public interface ICleaningOptionRepository : IGenericRepository<CleaningOption>
    {
        public Task<List<CleaningOption>?> GetListCleaningOptionByAnimalTypeId(int animalTypeId);
        public Task<CleaningOption> AddCleaningOption(CleaningOptionAdd key);
        public Task<CleaningOption?> UpdateCleaningOption(CleaningOptionUpdate key);
        public CleaningOptionView ConvertCleaningOptionIntoCleaningOptionView(CleaningOption cleaningOption, AnimalTypeView animalType, List<CleaningProcessView>? cleaningProcesss);
    }
}
