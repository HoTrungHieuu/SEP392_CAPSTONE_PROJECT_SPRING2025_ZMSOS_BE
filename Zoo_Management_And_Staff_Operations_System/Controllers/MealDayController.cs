using AccountManagement;
using DAO.AddModel;
using DAO.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace Zoo_Management_And_Staff_Operations_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealDayController : ControllerBase
    {
        private readonly IMealDayService service;
        public MealDayController(IMealDayService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("mealDays")]
        public async Task<IActionResult> GetListMealDay()
        {
            var result = await service.GetListMealDay();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("mealDays/animalTypeId")]
        public async Task<IActionResult> GetListMealDayByAnimalTypeId(int animalTypeId)
        {
            var result = await service.GetListMealDayByAnimalTypeId(animalTypeId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("mealDay/id")]
        public async Task<IActionResult> GetMealDayById(int id)
        {
            var result = await service.GeMealDayById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("mealDay")]
        public async Task<IActionResult> AddMealDay(MealDayAdd key)
        {
            var result = await service.AddMealDay(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("mealDay")]
        public async Task<IActionResult> UpdateMealDay(MealDayUpdate key)
        {
            var result = await service.UpdateMealDay(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
