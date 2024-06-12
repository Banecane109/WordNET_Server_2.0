using Microsoft.AspNetCore.Mvc;

namespace WordNET_Server_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check() => Ok();
    }
}
