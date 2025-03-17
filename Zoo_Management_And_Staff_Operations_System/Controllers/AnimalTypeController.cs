using AccountManagement;
using DAO.AddModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace AnimalAndCageManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalTypeController : ControllerBase
    {
        private readonly IAnimalTypeService service;
        public AnimalTypeController(IAnimalTypeService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("animalTypes")]
        public async Task<IActionResult> GetListAnimalType()
        {
            var result = await service.GetListAnimalType();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpPost("animalTypes/search-sort-paging")]
        public async Task<IActionResult> GetListAnimalTypeSearch(AnimalTypeSearch<AnimalTypeView> key)
        {
            var result = await service.GetListAnimalTypeSearching(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("animalType/{id}")]
        public async Task<IActionResult> GetAnimalTypeById(int id)
        {
            var result = await service.GetAnimalTypeById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("animalType")]
        public async Task<IActionResult> AddAnimalType(AnimalTypeAdd key)
        {
            var result = await service.AddAnimalType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("animalType")]
        public async Task<IActionResult> UpdateAnimalType(AnimalTypeUpdate key)
        {
            var result = await service.UpdateAnimalType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("animalType")]
        public async Task<IActionResult> DeleteAnimalType(int id)
        {
            var result = await service.DeleteAnimalType(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
