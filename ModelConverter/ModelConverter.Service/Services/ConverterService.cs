using ModelConverter.Emulator.Enums;
using ModelConverter.Emulator.Interfaces;
using ModelConverter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Services
{
    public class ConverterService : IConverterService
    {
        private readonly IModelConverter _modelConverter;

        public ConverterService(IModelConverter modelConverter)
        {
            this._modelConverter = modelConverter;
        }

        public async Task<FileInfo> Convert3DModelToNewFormat(string inputPath, TargetFormat targetFormat, string outputPath)
        {
            return await _modelConverter.Convert(inputPath, targetFormat, outputPath);
        }
    }
}