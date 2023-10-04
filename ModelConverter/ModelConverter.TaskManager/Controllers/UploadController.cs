using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ModelConverter.Common.Constants;
using ModelConverter.Common.Exceptions;
using ModelConverter.Common.Extensions;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Controllers
{
    public class UploadController : TaskManagerControllerBase
    {
        private readonly IFileManager _fileManager;
        private readonly IProcessIdProvider _processIdProvider;
        private readonly IValidator<UploadRequest> _validator;

        public UploadController(ILogger<UploadController> logger, IFileManager fileManager, IProcessManager processManager, IProcessIdProvider processIdProvider, IValidator<UploadRequest> validator, IExceptionHandler exceptionHandler)
            : base(logger, exceptionHandler, processManager)
        {
            _fileManager = fileManager;
            _processIdProvider = processIdProvider;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadShaprFile()
        {
            try
            {
                var request = Request.GetObjectFromRequestForm<UploadRequest>();
                ValidateUploadRequest(request);
                var file = Request.GetFileFromRequest();

                ValidateRequest(request);

                var savedFilePath = await _fileManager.SaveFile(file);

                await _processManager.StarConvertingAsync(_processIdProvider.ProcessId, savedFilePath, request.TargetFormat.Value);

                var response = new UploadResponse
                {
                    ProcessId = _processIdProvider.ProcessId,
                    ProcessStatusUrl = $"{Routing.TaskManagerRoutes.STATUS_URL}?processId={_processIdProvider.ProcessId}"
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

        private void ValidateUploadRequest(UploadRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Upload request was null");
            }
            _validator.ValidateAndThrow(request);
        }

        private void ValidateRequest(UploadRequest request)
        {
            if (request.TargetFormat is null)
            {
                throw new Exception("targetFormat should be specified");
            }
            _validator.ValidateAndThrow(request);
        }
    }
}