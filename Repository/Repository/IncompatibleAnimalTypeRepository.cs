using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
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
    public class IncompatibleAnimalTypeRepository : GenericRepository<IncompatibleAnimalType>, IIncompatibleAnimalTypeRepository
    {
        public IncompatibleAnimalTypeRepository()
        {
        }
        public async Task<List<IncompatibleAnimalType>?> GetListIncompatibleAnimalType()
        {
            try
            {
                var incompatibleAnimalTypes = await GetAllAsync();
                return incompatibleAnimalTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<IncompatibleAnimalType>?> GetListIncompatibleAnimalTypeByAnimalTypeId(int animalTypeId)
        {
            try
            {
                var incompatibleAnimalTypes = (await GetAllAsync()).FindAll(l=>l.AnimalTypeId1 == animalTypeId || l.AnimalTypeId2 == animalTypeId);
                return incompatibleAnimalTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> CheckIncompatibleAnimalType(int animalTypeId1, int animalTypeId2)
        {
            try
            {
                var incompatibleAnimalType = (await GetAllAsync()).FirstOrDefault(l => (l.AnimalTypeId1 == animalTypeId1 && l.AnimalTypeId2 == animalTypeId2) ||
                                                                                    (l.AnimalTypeId1 == animalTypeId2 && l.AnimalTypeId2 == animalTypeId1));
                if (incompatibleAnimalType == null) return false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IncompatibleAnimalType> AddIncompatibleAnimalType(IncompatibleAnimalTypeAdd key)
        {
            try
            {
                IncompatibleAnimalType incompatibleAnimalType = new()
                {
                    AnimalTypeId1 = key.AnimalTypeId1,
                    AnimalTypeId2 = key.AnimalTypeId2,
                    Reason = key.Reason,
                    CreatedDate = VietNamTime.GetVietNamTime(),
                };
                await CreateAsync(incompatibleAnimalType);
                return incompatibleAnimalType;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IncompatibleAnimalTypeView ConvertIncompatibleAnimalTypeIntoIncompatibleAnimalTypeView(IncompatibleAnimalType incompatibleAnimalType, AnimalTypeView animalType1, AnimalTypeView animalType2)
        {
            try
            {
                IncompatibleAnimalTypeView result = new IncompatibleAnimalTypeView();
                result.ConvertIncompatibleAnimalTypeIntoIncompatibleAnimalTypeView(incompatibleAnimalType,animalType1,animalType2);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
