using AccountManagement;
using DAO.AddModel;
using DAO.UpdateModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace Zoo_Management_And_Staff_Operations_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTypeController : ControllerBase
    {
        private readonly ITaskTypeService service;
        public TaskTypeController(ITaskTypeService service)
        {
            this.service = service;
        }
        [HttpGet("taskTypes")]
        public async Task<IActionResult> GetListTaskType()
        {
            var result = await service.GetListTaskType();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("taskType/id")]
        public async Task<IActionResult> GetTaskTypeById(int id)
        {
            var result = await service.GetTaskTypeById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("taskType")]
        public async Task<IActionResult> AddTaskType(TaskTypeAdd key)
        {
            var result = await service.AddTaskType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("taskType")]
        public async Task<IActionResult> UpdateTask(TaskTypeUpdate key)
        {
            var result = await service.UpdateTaskType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
