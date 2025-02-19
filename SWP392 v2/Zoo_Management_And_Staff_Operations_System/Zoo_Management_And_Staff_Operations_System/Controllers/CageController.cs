using AccountManagement;
using DAO.AddModel;
using DAO.UpdateModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace AnimalAndCageManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CageController : ControllerBase
    {
        private readonly ICageService service;
        public CageController(ICageService service)
        {
            this.service = service;
        }
        [HttpGet("cages")]
        public async Task<IActionResult> GetListCage()
        {
            var result = await service.GetListCage();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("cages/zooAreaId")]
        public async Task<IActionResult> GetListCageByZooAreaId(int zooAreaId)
        {
            var result = await service.GetListCageByZooAreaId(zooAreaId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("cage/id")]
        public async Task<IActionResult> GetCageById(int id)
        {
            var result = await service.GetCageById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("cage")]
        public async Task<IActionResult> AddCage(CageAdd key)
        {
            var result = await service.AddCage(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPut("cage")]
        public async Task<IActionResult> UpdateCage(CageUpdate key)
        {
            var result = await service.UpdateCage(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
