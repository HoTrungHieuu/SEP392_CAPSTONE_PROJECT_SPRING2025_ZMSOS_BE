using DAO.AddModel;
using DAO.UpdateModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService service;
        public ScheduleController(IScheduleService service)
        {
            this.service = service;
        }
        [HttpGet("schedules/accountId")]
        public async Task<IActionResult> GetListScheduleByAccountId(int accountId)
        {
            var result = await service.GetListScheduleByAccountId(accountId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("schedule/id")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            var result = await service.GetScheduleById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("schedule")]
        public async Task<IActionResult> AddSchedule(ScheduleAdd key)
        {
            var result = await service.AddSchedule(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("schedule")]
        public async Task<IActionResult> UpdateSchedule(ScheduleUpdate key)
        {
            var result = await service.UpdateSchedule(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
