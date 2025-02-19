using BO.Models;
using DAO.ViewModel;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ReportAttachmentReporitory : GenericRepository<ReportAttachment>, IReportAttachmentRepository
    {
        public ReportAttachmentReporitory()
        {
        }
        public async Task<List<ReportAttachment>?> GetListReportAttachmentByReportId(int reportId)
        {
            try
            {
                var reportAttachments = (await GetAllAsync()).FindAll(l => l.ReportId == reportId);
                return reportAttachments;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<string> ConvertListReportAttachmentIntoListString(List<ReportAttachment> reportAttachments)
        {
            try
            {
                List<string> result = new();
                foreach (var reportAttachment in reportAttachments)
                {
                    result.Add(reportAttachment.FileUrl);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
