using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using ModelConverter.Common.Enums;
using ModelConverter.Emulator.Interfaces;
using ModelConverter.Service.Constants;
using ModelConverter.Service.DTOs.Requestes;
using ModelConverter.Service.DTOs.Responses;
using ModelConverter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Functions
{
    public class ConverterFunction
    {
        private readonly IConverterService _converterService;
        private readonly IRequestConverter _requestConverter;
        private readonly IRequestValidator _requestValidator;
        private readonly ILogger<ConverterFunction> _logger;

        public ConverterFunction(IConverterService converterService,
            ILogger<ConverterFunction> logger,
            IRequestConverter requestConverter,
            IRequestValidator requestValidator)
        {
            _converterService = converterService;
            _logger = logger;
            _requestConverter = requestConverter;
            _requestValidator = requestValidator;
        }

        [FunctionName("Convert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, Constants.HttpMethods.POST, Route = Routes.CONVERTER)] HttpRequest request)
        {
            try
            {
                var modelConverterRequest = _requestConverter.ConvertHttpRequest<ModelConvertingRequest>(request);

                _requestValidator.ValidateRequest(modelConverterRequest);

                var newFile = await _converterService.Convert3DModelToNewFormat(modelConverterRequest.InputPath, modelConverterRequest.TargetFormat, modelConverterRequest.OutputPath);

                return new OkObjectResult(new ModelConvertingResponse(ProcessStatus.Completed, newFile.FullName));
            }
            catch (Exception ex)
            {
                var errorResponse = new ObjectResult(new ModelConvertingResponse(ProcessStatus.Failed, failingResult: ex.Message));
                errorResponse.StatusCode = StatusCodes.Status422UnprocessableEntity;

                return errorResponse;
            }
        }
    }
}