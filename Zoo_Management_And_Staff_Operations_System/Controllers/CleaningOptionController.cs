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
    public class CleaningOptionController : ControllerBase
    {
        private readonly ICleaningOptionService service;
        public CleaningOptionController(ICleaningOptionService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("cleaningOptions")]
        public async Task<IActionResult> GetListCleaningOption()
        {
            var result = await service.GetListCleaningOption();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader")]
        [HttpGet("cleaningOptions/by-animalType/{animalTypeId}")]
        public async Task<IActionResult> GetListCleaningOptionByAnimalTypeId(int animalTypeId)
        {
            var result = await service.GetListCleaningOptionByAnimalTypeId(animalTypeId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Admin,Manager,Leader,Staff")]
        [HttpGet("cleaningOption/{id}")]
        public async Task<IActionResult> GetCleaningOptionById(int id)
        {
            var result = await service.GetCleaningOptionById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("cleaningOption")]
        public async Task<IActionResult> AddCleaningOption(CleaningOptionAdd key)
        {
            var result = await service.AddCleaningOption(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("cleaningOption/disable")]
        public async Task<IActionResult> DisableCleaningOption(List<int> ids)
        {
            var result = await service.DisableCleaningOption(ids);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        /*[Authorize(Roles = "Manager")]
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
        }*/
    }
}
