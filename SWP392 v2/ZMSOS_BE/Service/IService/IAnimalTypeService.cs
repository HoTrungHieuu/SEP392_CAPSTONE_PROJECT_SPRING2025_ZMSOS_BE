using DAO.AddModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAnimalTypeService
    {
        public Task<ServiceResult> GetListAnimalType();
        public Task<ServiceResult> GetListAnimalTypeSearching(AnimalTypeSearch<AnimalTypeView> key);
        public Task<ServiceResult> GetAnimalTypeById(int id);
        public Task<ServiceResult> AddAnimalType(AnimalTypeAdd key);
        public Task<ServiceResult> UpdateAnimalType(AnimalTypeUpdate key);
    }
}
