using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            builder.RegisterEmulator();
        }
    }
}