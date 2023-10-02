using Microsoft.AspNetCore.Mvc;

namespace ModelConverter.TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private ILogger<UploadController> logger;

        public UploadController(ILogger<UploadController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> UploadShaprFile()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                logger.LogInformation($"/Upload called with body: {requestBody}");
            }
            return new OkObjectResult("Hello!");
        }
    }
}