using DAO.AddModel;
using DAO.UpdateModel;
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
        public Task<ServiceResult> GetAnimalTypeById(int id);
        public Task<ServiceResult> AddAnimalType(AnimalTypeAdd key);
        public Task<ServiceResult> UpdateAnimalType(AnimalTypeUpdate key);
    }
}
