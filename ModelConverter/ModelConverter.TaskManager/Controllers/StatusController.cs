using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Exceptions;
using ModelConverter.Common.Extensions;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Controllers
{
    public class StatusController : TaskManagerControllerBase
    {
        private readonly IProcessManager _processManager;
        private readonly IValidator<StatusUpdateRequest> _statusUpdateRequestValidator;
        private readonly IExceptionHandler _exceptionHandler;

        public StatusController(ILogger<UploadController> logger, IProcessManager processManager, IValidator<StatusUpdateRequest> statusUpdateRequestValidator, IExceptionHandler exceptionHandler)
            : base(logger)
        {
            _processManager = processManager;
            _statusUpdateRequestValidator = statusUpdateRequestValidator;
            _exceptionHandler = exceptionHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessStatus()
        {
            try
            {
                var processId = GetProcessIdFromReuquestParam();

                var processStatus = await _processManager.GetProcessStatusAsync(processId);

                return new OkObjectResult(new ProcessStatusResponse
                {
                    ProcessId = processId,
                    ProcessStatus = processStatus
                });
            }
            catch (Exception ex)
            {
                return await _exceptionHandler.HandleException(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProcessStatus()
        {
            try
            {
                var updateRequest = await Request.GetObjectFromRequestBodyAsync<StatusUpdateRequest>();
                ValidateRequest(updateRequest);
                await _processManager.UpdateProcessStatusAsync(updateRequest);
                return new OkObjectResult("Updated");
            }
            catch (Exception ex)
            {
                return await _exceptionHandler.HandleException(ex);
            }
        }

        private void ValidateRequest(StatusUpdateRequest updateRequest)
        {
            if (updateRequest is null)
            {
                throw new BadRequestException("Request body was invalid");
            }
            _statusUpdateRequestValidator.ValidateAndThrow(updateRequest);
        }

        private string GetProcessIdFromReuquestParam()
        {
            var processId = Request.GetRequestParam("processId");
            IsStringNotNullOrEmpty(processId);
            return processId;
        }

        private void IsStringNotNullOrEmpty(StringValues processId)
        {
            if (string.IsNullOrEmpty(processId))
            {
                throw new BadRequestException("ProcessId should be provided");
            }
        }
    }
}