using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IReportService
    {
        public Task<ServiceResult> GetListReportBySenderId(int senderId);
        public Task<ServiceResult> GetListReportByRecieverId(int recieverId);
        public Task<ServiceResult> GetReportById(int id);
        public Task<ServiceResult> AddReport(ReportAdd key);
    }
}
