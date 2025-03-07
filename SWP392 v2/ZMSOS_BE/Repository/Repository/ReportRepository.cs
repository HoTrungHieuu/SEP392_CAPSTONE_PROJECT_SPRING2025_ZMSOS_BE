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
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository()
        {
        }
        public async Task<List<Report>?> GetListReportByRecieverId(int accountId)
        {
            try
            {
                var reports = (await GetAllAsync()).FindAll(l => l.ReceiverId == accountId).OrderByDescending(l => l.Id).ToList();
                return reports;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Report>?> GetListReportBySenderId(int accountId)
        {
            try
            {
                var reports = (await GetAllAsync()).FindAll(l => l.SenderId == accountId).OrderByDescending(l => l.Id).ToList();
                return reports;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Report> AddReport(ReportAdd key, int recieverId)
        {
            try
            {
                Report report = new()
                {
                    ReceiverId = recieverId,
                    SenderId = key.SenderId,
                    Title = key.Title,
                    Content = key.Content,
                    Date = DateTime.Now,
                    Status = null
                };
                await CreateAsync(report);
                return report;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ReportView ConvertReportIntoReportView(Report report, UserView sender,UserView reciever, List<string> urlFile, StatusView? status)
        {
            try
            {
                ReportView result = new ReportView();
                result.ConvertReportIntoReportView(report,sender,reciever,urlFile,status);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
