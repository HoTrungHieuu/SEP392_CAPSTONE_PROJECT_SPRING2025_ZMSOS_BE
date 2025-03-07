using DAO.AddModel;
using DAO.SearchModel;
using DAO.ViewModel;
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
        public Task<ServiceResult> GetListReportBySenderIdSearch(int senderId, ReportSearch<ReportView> key);
        public Task<ServiceResult> GetListReportByRecieverId(int recieverId);
        public Task<ServiceResult> GetListReportByRecieverIdSearch(int recieverId, ReportSearch<ReportView> key);
        public Task<ServiceResult> GetReportById(int id);
        public Task<ServiceResult> AddReport(ReportAdd key);
    }
}
