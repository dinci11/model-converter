using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Controllers
{
    public class DownloadController : TaskManagerControllerBase
    {
        public DownloadController(ILogger<UploadController> logger, IExceptionHandler exceptionHandler, IProcessManager processManager)
            : base(logger, exceptionHandler, processManager)
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