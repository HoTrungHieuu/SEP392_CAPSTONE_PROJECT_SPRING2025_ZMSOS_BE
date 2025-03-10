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
    public class StatusController : ControllerBase
    {
        private readonly IStatusService service;
        public StatusController(IStatusService service)
        {
            this.service = service;
        }
        [HttpGet("statuss")]
        public async Task<IActionResult> GetListStatus()
        {
            var result = await service.GetListStatus();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("categorys")]
        public async Task<IActionResult> GetListCategorys()
        {
            var result = await service.GetListCategory();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("statuss/categoryId")]
        public async Task<IActionResult> GetListStatusByCategoryId(int categoryId)
        {
            var result = await service.GetListStatusByCategoryId(categoryId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("categorys/statusId")]
        public async Task<IActionResult> GetListCategorysByStatusId(int statusId)
        {
            var result = await service.GetListCategoryByStatusId(statusId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("status")]
        public async Task<IActionResult> AddStatus(StatusAdd key)
        {
            var result = await service.AddStatus(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus(StatusUpdate key)
        {
            var result = await service.UpdateStatus(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("category")]
        public async Task<IActionResult> AddCategory(CategoryAdd key)
        {
            var result = await service.AddCategory(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("category")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdate key)
        {
            var result = await service.UpdateCategory(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("statusCategory/statusId&&categoryId")]
        public async Task<IActionResult> AddStatusCategory(int statusId, int categoryId)
        {
            var result = await service.AddStatusCategory(statusId,categoryId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpDelete("statusCategory/statusId&&categoryId")]
        public async Task<IActionResult> DeleteStatusCategory(int statusId, int categoryId)
        {
            var result = await service.DeleteStatusCategory(statusId, categoryId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
