using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModelConverter.TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetDownloadUrl()
        {
            await Task.Delay(200);
            return new OkObjectResult("CustomUrl");
        }
    }
}