using Microsoft.Extensions.Logging;
using ModelConverter.Emulator.Enums;
using ModelConverter.Emulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Emulator.Services
{
    public class ModelConverterEmulator : IModelConverter
    {
        private readonly IFileManager _fileManager;
        private readonly ILogger<ModelConverterEmulator> _logger;

        public ModelConverterEmulator(IFileManager fileManager, ILogger<ModelConverterEmulator> logger)
        {
            this._fileManager = fileManager;
            this._logger = logger;
        }

        public async Task<FileInfo> Convert(string inputPath, TargetFormat targetFormat, string outputPath)
        {
            var file = await _fileManager.LoadFile(inputPath);
            if (file != null)
            {
                await Convert(file);
                var newFile = await _fileManager.SaveFile(file, outputPath);
                return newFile;
            }
            throw new Exception();
        }

        private async Task Convert(FileInfo file)
        {
            for (int i = 0; i < 10; i++)
            {
                _logger.LogInformation($"Converting process is running, {i * 10}% is done.");
                await Task.Delay(200);
            }
            _logger.LogInformation($"Converting process is done.");
        }
    }
}