using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModelConverter.TaskManager.Controllers
{
    public class StatusController : TaskManagerControllerBase
    {
        public StatusController(ILogger<UploadController> logger)
            : base(logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessStatus()
        {
            await Task.Delay(200);
            return new OkObjectResult("Running");
        }
    }
}