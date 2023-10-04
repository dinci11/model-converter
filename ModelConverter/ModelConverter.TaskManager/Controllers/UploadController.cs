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
                _logger.LogInformation($"Endpoint invoked. ProcessId: {_processIdProvider.ProcessId} ");
                var request = Request.GetObjectFromRequestForm<UploadRequest>();
                ValidateUploadRequest(request);
                var file = Request.GetFileFromRequest();

                ValidateRequest(request);

                var savedFilePath = await _fileManager.SaveFile(file);

                _logger.LogInformation($"Start converting. ProcessId: {_processIdProvider.ProcessId} ");
                await _processManager.StarConvertingAsync(_processIdProvider.ProcessId, savedFilePath, request.TargetFormat.Value);

                var response = new UploadResponse
                {
                    ProcessId = _processIdProvider.ProcessId,
                    ProcessStatusUrl = $"{Routing.TaskManagerRoutes.STATUS_URL}?processId={_processIdProvider.ProcessId}"
                };
                _logger.LogInformation($"Endpoint finished. ProcessId: {_processIdProvider.ProcessId} ");
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Endpoint thrown Exception. ProcessId: {_processIdProvider.ProcessId} ");
                return await _exceptionHandler.HandleExceptionAsync(ex);
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