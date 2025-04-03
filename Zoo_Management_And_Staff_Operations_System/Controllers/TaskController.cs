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
        [HttpGet("tasks/by-schedule/{scheduleId}")]
        public async Task<IActionResult> GetListTaskByScheduleId(int scheduleId)
        {
            var result = await service.GetListTaskByScheduleId(scheduleId);
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
        [HttpPost("task")]
        public async Task<IActionResult> AddTask(TaskAdd key)
        {
            var result = await service.AddTask(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("taskStart/by-id/{id}")]
        public async Task<IActionResult> StartTask(int id)
        {
            var result = await service.StartTask(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("taskStaff")]
        public async Task<IActionResult> UpdateTaskStaff(TaskStaffUpdate key)
        {
            var result = await service.UpdateTaskStaff(key);
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
    }
}
