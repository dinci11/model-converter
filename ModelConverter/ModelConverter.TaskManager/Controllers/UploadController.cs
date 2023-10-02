using Microsoft.AspNetCore.Mvc;
using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Controllers
{
    public class UploadController : TaskManagerControllerBase
    {
        private readonly IFileManager _fileManager;
        private readonly IProcessManager _processManager;
        private readonly IProcessIdProvider _processIdProvider;

        public UploadController(ILogger<UploadController> logger, IFileManager fileManager, IProcessManager processManager, IProcessIdProvider processIdProvider)
            : base(logger)
        {
            _fileManager = fileManager;
            _processManager = processManager;
            _processIdProvider = processIdProvider;
        }

        [HttpPost]
        public async Task<IActionResult> UploadShaprFile()
        {
            try
            {
                var request = GetRequestFromForm<UploadRequest>();
                var file = GetFileFromRequest();

                ValidateRequest(await request);

                var inputFilePath = await _fileManager.SaveFile(await file);

                await _processManager.StarConverting(inputFilePath);

                var response = new UploadResponse
                {
                    ProcessId = _processIdProvider.ProcessId,
                    ProcessStatusUrl = $"http://localhost:5000/api/Status/{_processIdProvider.ProcessId}"
                };
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                var response = new ObjectResult(ex.Message);
                response.StatusCode = 500;
                return response;
            }
        }

        private void ValidateRequest(UploadRequest request)
        {
            if (request.targetFormat is null)
            {
                throw new Exception("targetFormat should be specified");
            }
        }
    }
}