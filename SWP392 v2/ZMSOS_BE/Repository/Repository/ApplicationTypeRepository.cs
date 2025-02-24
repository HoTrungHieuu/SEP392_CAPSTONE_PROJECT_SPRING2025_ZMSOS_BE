using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ApplicationTypeRepository : GenericRepository<ApplicationType>, IApplicationTypeRepository
    {
        public ApplicationTypeRepository()
        {
        }
        public async Task<List<ApplicationType>?> GetListApplcationType()
        {
            try
            {
                var applicationTypes = await GetAllAsync();
                return applicationTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ApplicationType> AddApplicationType(ApplicationTypeAdd key)
        {
            try
            {
                ApplicationType applicationType = new()
                {
                    Name = key.Name,
                    Description = key.Description
                };
                await CreateAsync(applicationType);
                return applicationType;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ApplicationTypeView> ConvertListApplicationTypeIntoListApplicationTypeView(List<ApplicationType> applicationTypes)
        {
            try
            {
                List<ApplicationTypeView> result = new();
                foreach (var applicationType in applicationTypes)
                {
                    result.Add(ConvertApplicationTypeIntoApplicationTypeView(applicationType));
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ApplicationTypeView ConvertApplicationTypeIntoApplicationTypeView(ApplicationType applicationType)
        {
            try
            {
                ApplicationTypeView result = new ApplicationTypeView();
                result.ConvertApplicationTypeIntoApplicationtypeView(applicationType);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
