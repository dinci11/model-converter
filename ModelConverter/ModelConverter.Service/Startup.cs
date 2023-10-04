using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Services;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.Common.Validators;
using ModelConverter.Service;
using ModelConverter.Service.Extensions;
using ModelConverter.Service.Services;
using ModelConverter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

[assembly: FunctionsStartup(typeof(Startup))]

namespace ModelConverter.Service
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
            });
            builder.Services.AddScoped<IConverterService, ConverterService>();
            builder.Services.AddScoped<IRequestConverter, RequestConverter>();
            builder.Services.AddScoped<IRequestValidator, RequestValidator>();
            builder.Services.AddScoped<IValidator<ModelConvertingRequest>, ModelConvertingRequestValidator>();
            builder.Services.AddScoped<IValidator<StatusUpdateRequest>, StatusUpdateRequestValidator>();
            builder.Services.AddScoped<IExceptionHandler, ExceptionHandler>();
            builder.RegisterEmulator();
        }
    }
}