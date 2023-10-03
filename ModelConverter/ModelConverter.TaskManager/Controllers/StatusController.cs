using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Controllers
{
    public class StatusController : TaskManagerControllerBase
    {
        private readonly IProcessManager _processManager;

        public StatusController(ILogger<UploadController> logger, IProcessManager processManager)
            : base(logger)
        {
            _processManager = processManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessStatus()
        {
            try
            {
                var processId = GetProcessIdFromReuquestParam();

                var processStatus = await _processManager.GetProcessStatus(processId);

                return new OkObjectResult(new ProcessStatusResponse
                {
                    ProcessId = processId,
                    ProcessStatus = processStatus
                });
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProcessStatus()
        {
            return new OkObjectResult("Hello");
        }

        private string GetProcessIdFromReuquestParam()
        {
            var processId = Request.Query["processId"];

            IsStringNotNullOrEmpty(processId);

            return processId;
        }

        private void IsStringNotNullOrEmpty(StringValues processId)
        {
            if (string.IsNullOrEmpty(processId))
            {
                throw new Exception("ProcessId should be provided");
            }
        }
    }
}