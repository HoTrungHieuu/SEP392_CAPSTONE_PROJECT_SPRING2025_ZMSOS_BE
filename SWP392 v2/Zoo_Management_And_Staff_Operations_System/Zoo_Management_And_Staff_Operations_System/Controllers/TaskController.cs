using DAO.AddModel;
using DAO.UpdateModel;
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
        [HttpGet("tasks/scheduleId")]
        public async Task<IActionResult> GetListTaskByScheduleId(int scheduleId)
        {
            var result = await service.GetListTaskByScheduleId(scheduleId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("task/id")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result = await service.GetTaskById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("task")]
        public async Task<IActionResult> AddTask(TaskAdd key)
        {
            var result = await service.AddTask(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("task")]
        public async Task<IActionResult> UpdateTask(TaskUpdate key)
        {
            var result = await service.UpdateTask(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
