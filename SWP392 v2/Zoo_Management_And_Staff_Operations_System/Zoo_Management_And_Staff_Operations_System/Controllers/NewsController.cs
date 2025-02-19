using DAO.AddModel;
using DAO.UpdateModel;
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
        [HttpGet("newss")]
        public async Task<IActionResult> GetListNews()
        {
            var result = await service.GetListNews();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("news/id")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var result = await service.GetNewsById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("news")]
        public async Task<IActionResult> AddNews(NewsAdd key)
        {
            var result = await service.AddNews(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("news")]
        public async Task<IActionResult> UpdateNews(NewsUpdate key)
        {
            var result = await service.UpdateNews(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
