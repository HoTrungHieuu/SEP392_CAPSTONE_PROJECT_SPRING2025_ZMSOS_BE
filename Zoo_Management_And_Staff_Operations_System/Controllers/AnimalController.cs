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
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService service;
        public AnimalController(IAnimalService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("animals")]
        public async Task<IActionResult> GetListAnimal()
        {
            var result = await service.GetListAnimal();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpPost("animals/search-sort-paging")]
        public async Task<IActionResult> GetListAnimalSearch(AnimalSearch<AnimalView> key)
        {
            var result = await service.GetListAnimalSearching(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("animals/animalTypeId")]
        public async Task<IActionResult> GetListAnimalByAnimalTypeId(int animalTypeId)
        {
            var result = await service.GetListAnimalByAnimalTypeId(animalTypeId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("animals/cageId")]
        public async Task<IActionResult> GetListAnimalByCageId(int cageId)
        {
            var result = await service.GetListAnimalByCageId(cageId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("animal/id")]
        public async Task<IActionResult> GetAnimalById(int id)
        {
            var result = await service.GetAnimalById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("animal")]
        public async Task<IActionResult> AddAnimal(AnimalAdd key)
        {
            var result = await service.AddAnimal(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("animal")]
        public async Task<IActionResult> UpdateAnimal(AnimalUpdate key)
        {
            var result = await service.UpdateAnimal(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("animal/cage/animalIds&&cageId")]
        public async Task<IActionResult> AddAnimalCage(int animalId, int cageId)
        {
            var result = await service.AddAnimalCage(animalId, cageId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("animal/cage/animalId&&cageId")]
        public async Task<IActionResult> RemoveAnimalCage(int animalId, int cageId)
        {
            var result = await service.RemoveAnimalCage(animalId, cageId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("animal/cage/animalId&&cageId/replace")]
        public async Task<IActionResult> ReplaceAnimalCage(int animalId, int cageId)
        {
            var result = await service.ReplaceAnimalCage(animalId, cageId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
