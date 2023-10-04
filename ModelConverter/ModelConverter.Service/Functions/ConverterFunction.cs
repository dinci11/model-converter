using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using ModelConverter.Common.DTOs.Responses;
using ModelConverter.Common.Enums;
using ModelConverter.Emulator.Interfaces;
using ModelConverter.Service.Constants;
using ModelConverter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Services.Interfaces;
using FluentValidation;
using ModelConverter.Common.Extensions;
using ModelConverter.Common.Exceptions;

namespace ModelConverter.Service.Functions
{
    public class ConverterFunction
    {
        private readonly IConverterService _converterService;
        private readonly ILogger<ConverterFunction> _logger;
        private readonly IValidator<ModelConvertingRequest> _validator;
        private readonly IExceptionHandler _exceptionHandler;

        public ConverterFunction(IConverterService converterService,
            ILogger<ConverterFunction> logger,
            IValidator<ModelConvertingRequest> validator,
            IExceptionHandler exceptionHandler)
        {
            _converterService = converterService;
            _logger = logger;
            _validator = validator;
            _exceptionHandler = exceptionHandler;
        }

        [FunctionName("Convert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, Constants.HttpMethods.POST, Route = Routes.CONVERTER)] HttpRequest request)
        {
            try
            {
                var modelConverterRequest = await request.GetObjectFromRequestBodyAsync<ModelConvertingRequest>();

                ValidateRequest(modelConverterRequest);

                _ = _converterService.Convert3DModelToNewFormatAsync(modelConverterRequest);

                return new OkObjectResult(new ModelConvertingResponse
                {
                    ProcessId = modelConverterRequest.ProcessId,
                    ProcessStartResult = ProcessStartResult.Started
                });
            }
            catch (Exception ex)
            {
                return await _exceptionHandler.HandleException(ex);
            }
        }

        private void ValidateRequest(ModelConvertingRequest modelConverterRequest)
        {
            if (modelConverterRequest is null)
            {
                throw new BadRequestException("Body was null");
            }

            _validator.ValidateAndThrow(modelConverterRequest);
        }
    }
}