using BO.Models;
using DAO.AddModel;
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
    public class CleaningProcessRepository : GenericRepository<CleaningProcess>, ICleaningProcessRepository
    {
        public CleaningProcessRepository()
        {
        }
        public async Task<List<CleaningProcess>?> GetListCleaningProcessByCleaningOptionId(int cleaningOptionId)
        {
            try
            {
                var cleaningProcess = (await GetAllAsync()).FindAll(l => l.CleaningOptionId == cleaningOptionId);
                return cleaningProcess;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CleaningProcess> AddCleaningProcess(int cleaningOptiontId,int stepNumber, CleaningProcessAdd key)
        {
            try
            {
                CleaningProcess cleaningProcess = new()
                {
                    CleaningOptionId = cleaningOptiontId,
                    StepNumber = stepNumber,
                    Content = key.Content,
                    Estimatetime = key.Estimatetime
                };
                await CreateAsync(cleaningProcess);
                return cleaningProcess;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CleaningProcessView ConvertCleaningProcessIntoCleaningProcessView(CleaningProcess cleaningProcess, List<UrlProcessView>? urlProcesss)
        {
            try
            {
                CleaningProcessView result = new CleaningProcessView();
                result.ConvertCleaningProcessIntoCleaningProcessView(cleaningProcess,urlProcesss);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
