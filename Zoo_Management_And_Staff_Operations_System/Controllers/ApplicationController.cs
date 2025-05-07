using DAO.AddModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("applications/by-sender/{senderId}")]
        public async Task<IActionResult> GetListApplicationBySenderId(int senderId)
        {
            var result = await service.GetListApplicationBySenderId(senderId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Leader,Staff")]
        [HttpPost("applications/by-sender/search-sort-paging")]
        public async Task<IActionResult> GetListApplicationBySenderIdSearch(int senderId,ApplicationSearch<ApplicationView> key)
        {
            var result = await service.GetListApplicationBySenderIdSearch(senderId, key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader")]
        [HttpGet("applications/by-reciever/{recieverId}")]
        public async Task<IActionResult> GetListApplicationByRecieverId(int recieverId)
        {
            var result = await service.GetListApplicationByRecieverId(recieverId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader")]
        [HttpPost("applications/by-reciever/search-sort-paging")]
        public async Task<IActionResult> GetListApplicationByRecieverIdSearch(int recieverId, ApplicationSearch<ApplicationView> key)
        {
            var result = await service.GetListApplicationByRecieverIdSearch(recieverId, key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("application/{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            var result = await service.GetApplicationById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpPost("application")]
        public async Task<IActionResult> AddApplication(ApplicationAdd key)
        {
            var result = await service.AddApplication(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader")]
        [HttpPut("application/reply")]
        public async Task<IActionResult> UpdateApplicaiton(ApplicationUpdate key)
        {
            var result = await service.UpdateApplication(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
