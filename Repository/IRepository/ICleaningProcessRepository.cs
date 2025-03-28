using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICleaningProcessRepository : IGenericRepository<CleaningProcess>
    {
        public Task<List<CleaningProcess>?> GetListCleaningProcessByCleaningOptionId(int cleaningOptionId);
        public Task<CleaningProcess> AddCleaningProcess(int cleaningOptiontId, int stepNumber, CleaningProcessAdd key);
        public CleaningProcessView ConvertCleaningProcessIntoCleaningProcessView(CleaningProcess cleaningProcess, List<UrlProcessView>? urlProcesss);
    }
}
