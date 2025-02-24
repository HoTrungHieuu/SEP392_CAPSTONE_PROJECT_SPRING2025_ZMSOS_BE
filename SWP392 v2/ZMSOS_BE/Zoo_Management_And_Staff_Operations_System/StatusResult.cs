using Microsoft.AspNetCore.Mvc;
using Service;

namespace AccountManagement
{
    public class StatusResult : ControllerBase
    {
        public IActionResult Result(ServiceResult result)
        {
            switch (result.Status)
            {
                case 200:
                    return Ok(result);
                case 400:
                    return NotFound(result);
                case 404:
                    return NotFound();
                case 409:
                    return Conflict(result);
                case 501:
                    return StatusCode(501);
                default:
                    return Ok();
            }
        }
    }
}
