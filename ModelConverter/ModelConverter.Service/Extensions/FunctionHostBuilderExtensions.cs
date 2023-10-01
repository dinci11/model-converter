using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ModelConverter.Emulator.Interfaces;
using ModelConverter.Emulator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Extensions
{
    public static class FunctionHostBuilderExtensions
    {
        public static void RegisterEmulator(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IFileManager, FileManager>();
            builder.Services.AddScoped<IModelConverter, ModelConverterEmulator>();
        }
    }
}