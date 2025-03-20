using AccountManagement;
using DAO.AddModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace Zoo_Management_And_Staff_Operations_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService service;
        public FoodController(IFoodService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("foods")]
        public async Task<IActionResult> GetListFood()
        {
            var result = await service.GetListFood();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("food/{id}")]
        public async Task<IActionResult> GetFoodById(int id)
        {
            var result = await service.GeFoodById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("food")]
        public async Task<IActionResult> AddFood(FoodAdd key)
        {
            var result = await service.AddFood(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("food")]
        public async Task<IActionResult> UpdateFood(FoodUpdate key)
        {
            var result = await service.UpdateFood(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpDelete("food/id/{foodId}")]
        public async Task<IActionResult> DeleteFood(int foodId)
        {
            var result = await service.DeleteFood(foodId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
