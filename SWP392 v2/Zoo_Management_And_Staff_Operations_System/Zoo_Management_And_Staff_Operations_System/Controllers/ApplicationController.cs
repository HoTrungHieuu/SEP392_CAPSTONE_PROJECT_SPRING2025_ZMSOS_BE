using DAO.AddModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService service;
        public ApplicationController(IApplicationService service)
        {
            this.service = service;
        }
        [HttpGet("applications/senderId")]
        public async Task<IActionResult> GetListApplicationBySenderId(int senderId)
        {
            var result = await service.GetListApplicationBySenderId(senderId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("applications/recieverId")]
        public async Task<IActionResult> GetListApplicationByRecieverId(int recieverId)
        {
            var result = await service.GetListApplicationByRecieverId(recieverId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("application/id")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            var result = await service.GetApplicationById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("application")]
        public async Task<IActionResult> AddApplication(ApplicationAdd key)
        {
            var result = await service.AddApplication(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
