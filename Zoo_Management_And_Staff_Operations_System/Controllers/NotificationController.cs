using DAO.AddModel;
using DAO.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace AccountManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService service;
        public NotificationController(INotificationService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("notifications/by-account/{accountId}")]
        public async Task<IActionResult> GetListNotificationByAccountId(int accountId)
        {
            var result = await service.GetListNotification(accountId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("notification(onlyDev)")]
        public async Task<IActionResult> AddNotification(List<NotificationAdd> key)
        {
            var result = await service.AddListNotification(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
