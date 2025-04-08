using AccountManagement;
using DAO.AddModel;
using DAO.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
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
        [HttpGet("mealDays/by-animalType/{animalTypeId}")]
        public async Task<IActionResult> GetListMealDayByAnimalTypeId(int animalTypeId)
        {
            var result = await service.GetListMealDayByAnimalTypeId(animalTypeId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("mealDay/{id}")]
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
        [Authorize(Roles = "Manager")]
        [HttpDelete("mealDay/disable")]
        public async Task<IActionResult> DisableMealDay(List<int> ids)
        {
            var result = await service.DisableMealDay(ids);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("mealDay/export/excel")]
        public async Task<IActionResult> ExportListMealDay()
        {
            var result = await service.ExportListMealDay();
            return File(result,
               "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               "MealDays.xlsx");
        }
        [HttpPost("mealDay/import/excel")]
        public async Task<IActionResult> ImportListMealDay(IFormFile file)
        {

            if (file == null || file.Length == 0)
                return BadRequest(new ServiceResult
                {
                    Status = 400,
                    Message = "File required"
                });

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (fileExtension != ".xlsx")
                return BadRequest(new ServiceResult
                {
                    Status = 400,
                    Message = "File must be .xlsx"
                });
            using (var stream = file.OpenReadStream())
            {
                var result = await service.ImportMealDays(stream);
                StatusResult statusResult = new StatusResult();
                return statusResult.Result(result);
            }
        }
    }
}
