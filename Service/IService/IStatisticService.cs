using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IStatisticService
    {
        public Task<ServiceResult> GetStatistic();
        public Task<ServiceResult> GetLeaderStatistic(int accountId, DateOnly fromDate, DateOnly toDate);
    }
}
