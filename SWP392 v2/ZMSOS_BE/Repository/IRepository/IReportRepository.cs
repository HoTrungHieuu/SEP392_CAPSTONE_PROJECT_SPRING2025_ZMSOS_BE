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
    public interface IReportRepository : IGenericRepository<Report>
    {
        public Task<List<Report>?> GetListReportByRecieverId(int accountId);
        public Task<List<Report>?> GetListReportBySenderId(int accountId);
        public Task<Report> AddReport(ReportAdd key, int recieverId);   
        public ReportView ConvertReportIntoReportView(Report report, UserView sender, UserView reciever, List<string> urlFile, StatusView? status);
    }
}
