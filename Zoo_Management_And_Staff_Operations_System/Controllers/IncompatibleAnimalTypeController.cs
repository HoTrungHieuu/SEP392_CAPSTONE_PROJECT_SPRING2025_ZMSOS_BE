using AccountManagement;
using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace Zoo_Management_And_Staff_Operations_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncompatibleAnimalTypeController : ControllerBase
    {
        private readonly IIncompatibleAnimalTypeService service;
        public IncompatibleAnimalTypeController(IIncompatibleAnimalTypeService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("incompatibleAnimalTypes")]
        public async Task<IActionResult> GetListIncompatibleAnimalType()
        {
            var result = await service.GetListIncompatibleAnimalType();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("incompatibleAnimalTypes/special")]
        public async Task<IActionResult> GetListIncompatibleAnimalTypeSpecial()
        {
            var result = await service.GetListIncompatibleAnimalTypeSpecial();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("ncompatibleAnimalType/{id}")]
        public async Task<IActionResult> GetIncompatibleAnimalTypeById(int id)
        {
            var result = await service.GetIncompatibleAnimalTypeById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("incompatibleAnimalType")]
        public async Task<IActionResult> AddIncompatibleAnimalType(IncompatibleAnimalTypeAdd key)
        {
            var result = await service.AddIncompatibleAnimalType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("incompatibleAnimalType/list")]
        public async Task<IActionResult> AddListIncompatibleAnimalType(IncompatibleTypeAddList key)
        {
            var result = await service.AddListIncompatibleAnimalType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
