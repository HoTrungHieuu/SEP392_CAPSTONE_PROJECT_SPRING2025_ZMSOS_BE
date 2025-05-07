using DAO.AddModel;
using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IIncompatibleAnimalTypeService
    {
        public Task<ServiceResult> GetListIncompatibleAnimalType();
        public Task<ServiceResult> GetListIncompatibleAnimalTypeSpecial();
        public Task<ServiceResult> GetIncompatibleAnimalTypeById(int id);
        public Task<ServiceResult> AddIncompatibleAnimalType(IncompatibleAnimalTypeAdd key);
        public Task<ServiceResult> AddListIncompatibleAnimalType(IncompatibleTypeAddList key);

    }
}
