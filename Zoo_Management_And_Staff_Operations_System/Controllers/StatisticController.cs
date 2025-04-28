using AccountManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace Zoo_Management_And_Staff_Operations_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService service;
        public StatisticController(IStatisticService service)
        {
            this.service = service;
        }
        [HttpGet("statistic")]
        public async Task<IActionResult> GetStatistic()
        {
            var result = await service.GetStatistic();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("statistic/leader")]
        public async Task<IActionResult> GetStatisticLeader(int accountId, DateOnly fromDate, DateOnly toDate)
        {
            var result = await service.GetLeaderStatistic(accountId,fromDate,toDate);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
