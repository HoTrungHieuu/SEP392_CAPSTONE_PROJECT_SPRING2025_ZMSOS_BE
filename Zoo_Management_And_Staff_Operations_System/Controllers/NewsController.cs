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
    public class NewsController : ControllerBase
    {
        private readonly INewsService service;
        public NewsController(INewsService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("newss")]
        public async Task<IActionResult> GetListNews()
        {
            var result = await service.GetListNews();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("news/id")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var result = await service.GetNewsById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("news")]
        public async Task<IActionResult> AddNews(NewsAdd key)
        {
            var result = await service.AddNews(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("news")]
        public async Task<IActionResult> UpdateNews(NewsUpdate key)
        {
            var result = await service.UpdateNews(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
