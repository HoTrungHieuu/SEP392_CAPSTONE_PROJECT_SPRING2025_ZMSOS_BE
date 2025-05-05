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
    public class IncidentHistoryController : ControllerBase
    {
        private readonly IIncidentHistoryService service;
        public IncidentHistoryController(IIncidentHistoryService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager, Leader")]
        [HttpGet("incidentHistorys")]
        public async Task<IActionResult> GetListIncidentHistory()
        {
            var result = await service.GetListIncidentHistory();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager, Leader")]
        [HttpGet("incidentHistorys/by-animalId/{animalId}")]
        public async Task<IActionResult> GetListIncidentHisotoryByAnimalId(int animalId)
        {
            var result = await service.GetListIncidentHistoryByAnimalId(animalId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager, Leader")]
        [HttpGet("incidentHistory/{id}")]
        public async Task<IActionResult> GetIncidentHisotoryById(int id)
        {
            var result = await service.GetIncidentHistoryById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("incidentHistory")]
        public async Task<IActionResult> AddIncidentHistory(IncidentHistoryAdd key)
        {
            var result = await service.AddIncidentHistory(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("incidentHistory")]
        public async Task<IActionResult> UpdateIncidentHistory(IncidentHistoryUpdate key)
        {
            var result = await service.UpdateIncidentHistory(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("incidentHistory/disable")]
        public async Task<IActionResult> DisableIncidentHistory(List<int> ids)
        {
            var result = await service.DisableIncidentHistory(ids);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
