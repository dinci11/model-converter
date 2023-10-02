using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModelConverter.TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProcessStatus()
        {
            await Task.Delay(200);
            return new OkObjectResult("Running");
        }
    }
}