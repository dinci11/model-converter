using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using ModelConverter.Emulator.Interfaces;
using ModelConverter.Service.Constants;
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
        private readonly ILogger<ConverterFunction> _logger;

        public ConverterFunction(IConverterService converterService, ILogger<ConverterFunction> logger)
        {
            this._converterService = converterService;
            this._logger = logger;
        }

        [FunctionName("Convert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, Constants.HttpMethods.POST, Route = Routes.CONVERTER)] HttpRequest request)
        {
            return new OkObjectResult("Hello!");
        }
    }
}