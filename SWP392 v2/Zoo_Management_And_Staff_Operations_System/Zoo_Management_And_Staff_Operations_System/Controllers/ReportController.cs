using DAO.AddModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService service;
        public ReportController(IReportService service)
        {
            this.service = service;
        }
        [HttpGet("reports/senderId")]
        public async Task<IActionResult> GetListReportBySenderId(int senderId)
        {
            var result = await service.GetListReportBySenderId(senderId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("reports/recieverId")]
        public async Task<IActionResult> GetListReportByRecieverId(int recieverId)
        {
            var result = await service.GetListReportByRecieverId(recieverId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("report/id")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var result = await service.GetReportById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("report")]
        public async Task<IActionResult> AddReport(ReportAdd key)
        {
            var result = await service.AddReport(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
