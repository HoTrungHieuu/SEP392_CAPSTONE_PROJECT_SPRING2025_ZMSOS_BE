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
    public class CageController : ControllerBase
    {
        private readonly ICageService service;
        public CageController(ICageService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("cages")]
        public async Task<IActionResult> GetListCage()
        {
            var result = await service.GetListCage();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpPost("cages/search-sort-paging")]
        public async Task<IActionResult> GetListCageSearch(CageSearch<CageView> key)
        {
            var result = await service.GetListCageSearching(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("cages/by-zooArea/{zooAreaId}")]
        public async Task<IActionResult> GetListCageByZooAreaId(int zooAreaId)
        {
            var result = await service.GetListCageByZooAreaId(zooAreaId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("cage/{id}")]
        public async Task<IActionResult> GetCageById(int id)
        {
            var result = await service.GetCageById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("cage/history/{id}")]
        public async Task<IActionResult> GetCageHistoryById(int id)
        {
            var result = await service.GetCageHistoryById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("cage")]
        public async Task<IActionResult> AddCage(CageAdd key)
        {
            var result = await service.AddCage(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("cage")]
        public async Task<IActionResult> UpdateCage(CageUpdate key)
        {
            var result = await service.UpdateCage(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("cage/disable")]
        public async Task<IActionResult> DisableCage(List<int> ids)
        {
            var result = await service.DisableCage(ids);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("cage/{id}")]
        public async Task<IActionResult> DeleteCage(int id)
        {
            var result = await service.DeleteCage(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
