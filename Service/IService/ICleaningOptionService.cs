using DAO.AddModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICleaningOptionService
    {
        public Task<ServiceResult> GetListCleaningOption();
        public Task<ServiceResult> GetListCleaningOptionByAnimalTypeId(int animalTypeId);
        public Task<ServiceResult> AddCleaningOption(CleaningOptionAdd key);
        public Task<ServiceResult> DisableCleaningOption(List<int> cleaningOptinoIds);
        public Task<ServiceResult> GetCleaningOptionById(int id);
    }
}
