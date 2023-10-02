using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModelConverter.TaskManager.Controllers
{
    public class DownloadController : TaskManagerControllerBase
    {
        public DownloadController(ILogger<UploadController> logger)
            : base(logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetDownloadUrl()
        {
            await Task.Delay(200);
            return new OkObjectResult("CustomUrl");
        }
    }
}