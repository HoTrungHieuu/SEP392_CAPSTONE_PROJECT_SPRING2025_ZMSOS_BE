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
    public interface IUrlProcessRepository : IGenericRepository<UrlProcess>
    {
        public Task<List<UrlProcess>?> GetListUrlProcessByCleaningProcessId(int cleaningProcessId);
        public Task<UrlProcess> AddUrlProcess(int cleaningProcessId, UrlProcessAdd key);
        public UrlProcessView ConvertUrlProcessIntoUrlProcessView(UrlProcess urlProcess);
    }
}
