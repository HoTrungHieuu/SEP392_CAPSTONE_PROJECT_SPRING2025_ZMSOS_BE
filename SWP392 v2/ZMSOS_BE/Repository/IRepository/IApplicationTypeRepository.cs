using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositoyr
{
    public interface IApplicationTypeRepository : IGenericRepository<ApplicationType>
    {
        public Task<List<ApplicationType>?> GetListApplcationType();
        public Task<ApplicationType> AddApplicationType(ApplicationTypeAdd key);
        public ApplicationTypeView ConvertApplicationTypeIntoApplicationTypeView(ApplicationType applicationType);
    }
}
