using DAO.AddModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationTypeController : ControllerBase
    {
        private readonly IApplicationTypeService service;
        public ApplicationTypeController(IApplicationTypeService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("applicationTypes")]
        public async Task<IActionResult> GetListApplicationType()
        {
            var result = await service.GetListApplicationType();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("applicationType")]
        public async Task<IActionResult> GetApplicationTypeoById(int id)
        {
            var result = await service.GetApplicationTypeById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("applicationType")]
        public async Task<IActionResult> AddApplicationTypeo(ApplicationTypeAdd key)
        {
            var result = await service.AddApplicationType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
