using BO.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositoyr
{
    public interface IReportAttachmentRepository : IGenericRepository<ReportAttachment>
    {
        public Task<List<ReportAttachment>?> GetListReportAttachmentByReportId(int reportId);
        public List<string> ConvertListReportAttachmentIntoListString(List<ReportAttachment> reportAttachments);
    }
}
