using AccountManagement;
using BO.Models;
using DAO.AddModel;
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
        [HttpGet("incompatibleAnimalTypes")]
        public async Task<IActionResult> GetListIncompatibleAnimalType()
        {
            var result = await service.GetListIncompatibleAnimalType();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpGet("ncompatibleAnimalType/id")]
        public async Task<IActionResult> GetIncompatibleAnimalTypeById(int id)
        {
            var result = await service.GetIncompatibleAnimalTypeById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [HttpPost("incompatibleAnimalType")]
        public async Task<IActionResult> AddIncompatibleAnimalType(IncompatibleAnimalTypeAdd key)
        {
            var result = await service.AddIncompatibleAnimalType(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
