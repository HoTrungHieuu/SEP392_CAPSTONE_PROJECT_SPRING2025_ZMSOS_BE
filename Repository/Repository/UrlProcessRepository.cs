using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using Repository.IRepository;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UrlProcessRepository : GenericRepository<UrlProcess>, IUrlProcessRepository
    {
        public UrlProcessRepository()
        {
        }
        public async Task<List<UrlProcess>?> GetListUrlProcessByCleaningProcessId(int cleaningProcessId)
        {
            try
            {
                var urlProcess = (await GetAllAsync()).FindAll(l => l.CleaningProcessId == cleaningProcessId);
                return urlProcess;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UrlProcess> AddUrlProcess(int cleaningProcessId, UrlProcessAdd key)
        {
            try
            {
                UrlProcess urlProcess = new()
                {
                    CleaningProcessId = cleaningProcessId,
                    Url = key.Url,
                };
                await CreateAsync(urlProcess);
                return urlProcess;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UrlProcessView ConvertUrlProcessIntoUrlProcessView(UrlProcess urlProcess)
        {
            try
            {
                UrlProcessView result = new UrlProcessView();
                result.ConvertUrlProcessIntoUrlProcessView(urlProcess);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
