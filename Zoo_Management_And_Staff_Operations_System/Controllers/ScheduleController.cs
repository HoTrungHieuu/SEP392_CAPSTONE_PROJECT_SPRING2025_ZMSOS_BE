using DAO.AddModel;
using DAO.DeleteModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("schedules/by-account/{accountId}")]
        public async Task<IActionResult> GetListScheduleByAccountId(int accountId)
        {
            var result = await service.GetListScheduleByAccountId(accountId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("schedules/by-account&&fromDate&&toDate/{accountId}/{fromDate}/{toDate}")]
        public async Task<IActionResult> GetListScheduleByAccountIdByDate(int accountId, DateOnly fromDate, DateOnly toDate)
        {
            var result = await service.GetListScheduleByAccountIdByDate(accountId,fromDate,toDate);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("schedules/by-team&&fromDate&&toDate/{teamId}/{fromDate}/{toDate}")]
        public async Task<IActionResult> GetListScheduleByTeamIdByDate(int teamId, DateOnly fromDate, DateOnly toDate)
        {
            var result = await service.GetListScheduleByTeamIdByDate(teamId, fromDate, toDate);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("schedule/{id}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            var result = await service.GetScheduleById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader")]
        [HttpPost("schedule")]
        public async Task<IActionResult> AddSchedule(ScheduleAdd key)
        {
            var result = await service.AddSchedule(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader")]
        [HttpPost("schedule/auto")]
        public async Task<IActionResult> AddScheduleAuto(ScheduleAutoAdd key)
        {
            var result = await service.AddScheduleAuto(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader")]
        [HttpPut("schedule")]
        public async Task<IActionResult> UpdateSchedule(ScheduleUpdate key)
        {
            var result = await service.UpdateSchedule(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader")]
        [HttpPut("schedule/tranfer")]
        public async Task<IActionResult> TranferSchedule(ScheduleTranfer key)
        {
            var result = await service.TranferSchedule(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpDelete("schedule")]
        public async Task<IActionResult> DeleteSchedule(ScheduleDelete key)
        {
            var result = await service.DeleteSchedule(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpDelete("schedule/disable")]
        public async Task<IActionResult> DisableSchedule(ScheduleDelete key)
        {
            var result = await service.DisableSchedule(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
