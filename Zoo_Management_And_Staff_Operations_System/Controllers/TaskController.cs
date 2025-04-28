using DAO.AddModel;
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
    public class TaskController : ControllerBase
    {
        private readonly ITaskService service;
        public TaskController(ITaskService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("tasks/by-team/{teamId}/{fromDate}/{toDate}")]
        public async Task<IActionResult> GetListTaskByDateByTeamId(int teamId, DateOnly fromDate, DateOnly toDate)
        {
            var result = await service.GetListTaskByDateByTeamId(teamId,fromDate,toDate);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("tasks/by-account/{accountId}/{fromDate}/{toDate}")]
        public async Task<IActionResult> GetListTaskByDateByAccountId(int accountId, DateOnly fromDate, DateOnly toDate)
        {
            var result = await service.GetListTaskByDateByAccountId(accountId, fromDate, toDate);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("tasks/animals/by-team/{teamId}/{fromDate}/{toDate}")]
        public async Task<IActionResult> GetListTaskAnimalByDateByTeamId(int teamId, DateOnly fromDate, DateOnly toDate)
        {
            var result = await service.GetListTaskAnimalByDateByTeamId(teamId, fromDate, toDate);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("tasks/by-schedule/{scheduleId}")]
        public async Task<IActionResult> GetListTaskByScheduleId(int scheduleId)
        {
            var result = await service.GetListTaskByScheduleId(scheduleId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("accounts/suitable/tranfer/by-teamId-taskId/{teamId}/{taskId}")]
        public async Task<IActionResult> GetListAccountSuitableTranfer(int teamId, int taskId)
        {
            var result = await service.GetListAccountSuitableTranfer(teamId, taskId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("task/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result = await service.GetTaskById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader")]
        [HttpPost("task/manual")]
        public async Task<IActionResult> AddTaskManual(TaskAddManual key)
        {
            var result = await service.AddTaskManual(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("taskStart/by-id/{id}")]
        public async Task<IActionResult> StartTask(int id)
        {
            var result = await service.StartTask(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("taskStaff")]
        public async Task<IActionResult> UpdateTaskStaff(TaskStaffUpdate key)
        {
            var result = await service.UpdateTaskStaff(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("taskLeader")]
        public async Task<IActionResult> UpdateTaskLeader(TaskLeaderUpdate key)
        {
            var result = await service.UpdateTaskLeader(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpDelete("task")]
        public async Task<IActionResult> ClearTask(ClearTask key)
        {
            var result = await service.ClearTaskStaff(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("taskMealAutomatic")]
        public async Task<IActionResult> AddTaskAutomatic(AnimalTaskMealSchdule key)
        {
            var result = await service.AddTaskAutomatic(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("taskCleaningAutomatic")]
        public async Task<IActionResult> AddTaskCleaningAutomatic(AnimalTaskCleaningSchedule key)
        {
            var result = await service.AddTaskCleaningAutomatic(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("taskHealthAutomatic")]
        public async Task<IActionResult> AddTaskHealthAutomatic(AnimalTaskNormalScheldule key)
        {
            var result = await service.AddTaskHealthAutomatic(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpPut("task")]
        public async Task<IActionResult> UpdateTask(TaskUpdate key)
        {
            var result = await service.UpdateTask(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpPut("task/tranfer")]
        public async Task<IActionResult> TranferTask(TaskTranfer key)
        {
            var result = await service.TranferTask(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
