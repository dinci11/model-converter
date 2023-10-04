using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Exceptions;
using ModelConverter.Common.Extensions;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Services;
using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Controllers
{
    public class StatusController : TaskManagerControllerBase
    {
        private readonly IValidator<StatusUpdateRequest> _statusUpdateRequestValidator;

        public StatusController(ILogger<UploadController> logger, IProcessManager processManager, IValidator<StatusUpdateRequest> statusUpdateRequestValidator, IExceptionHandler exceptionHandler)
            : base(logger, exceptionHandler, processManager)
        {
            _statusUpdateRequestValidator = statusUpdateRequestValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProcessStatus()
        {
            try
            {
                _logger.LogInformation($"GET Endpoint invoked.");
                var processId = GetProcessIdFromReuquestParam();

                _logger.LogInformation($"Retrieving process status. ProcessId: {processId}");
                var processStatus = await _processManager.GetProcessStatusAsync(processId);

                _logger.LogInformation($"GET Endpoint finished");
                return new OkObjectResult(new ProcessStatusResponse
                {
                    ProcessId = processId,
                    ProcessStatus = processStatus
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"GET Endpoint thrown an exception");
                return await _exceptionHandler.HandleException(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProcessStatus()
        {
            try
            {
                _logger.LogInformation($"PUT Endpoint invoked.");
                var updateRequest = await Request.GetObjectFromRequestBodyAsync<StatusUpdateRequest>();

                ValidateRequest(updateRequest);

                _logger.LogInformation($"Update process status. ProcessId: {updateRequest.ProcessId}");
                await _processManager.UpdateProcessStatusAsync(updateRequest);

                _logger.LogInformation($"Endpoint finished");
                return new OkObjectResult("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"PUT Endpoint thrown an exception");
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