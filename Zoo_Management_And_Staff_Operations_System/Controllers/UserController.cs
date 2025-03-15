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
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("user/accountId")]
        public async Task<IActionResult> GetUserByAccountId(int accountId)
        {
            var result = await service.GetUserByAccountId(accountId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpPut("user/accountId")]
        public async Task<IActionResult> UpdateUser(UserUpdate key)
        {
            var result = await service.UpdateUser(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
