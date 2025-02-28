using AccountManagement;
using DAO.AddModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
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
        [HttpGet("animalTypes")]
        public async Task<IActionResult> GetListAnimalType()
        {
            var result = await service.GetListAnimalType();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("animalTypes/search-sort-paging")]
        public async Task<IActionResult> GetListAnimalTypeSearch(AnimalTypeSearch<AnimalTypeView> key)
        {
            var result = await service.GetListAnimalTypeSearching(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("animalType/id")]
        public async Task<IActionResult> GetAnimalTypeById(int id)
        {
            var result = await service.GetAnimalTypeById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("animalType")]
        public async Task<IActionResult> AddAnimalType(AnimalTypeAdd key)
        {
            var result = await service.AddAnimalType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("animalType")]
        public async Task<IActionResult> UpdateAnimalType(AnimalTypeUpdate key)
        {
            var result = await service.UpdateAnimalType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
