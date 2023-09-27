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
        private IFileManager fileManager;
        private ILogger<ModelConverterEmulator> logger;

        public ModelConverterEmulator(IFileManager fileManager, ILogger<ModelConverterEmulator> logger)
        {
            this.fileManager = fileManager;
            this.logger = logger;
        }

        public async Task<FileInfo> Convert(string inputPath, TargetFormat targetFormat, string outputPath)
        {
            var file = await fileManager.LoadFile(inputPath);
            if (file != null)
            {
                await Convert(file);
                var newFile = await fileManager.SaveFile(file, outputPath);
                return newFile;
            }
            throw new Exception();
        }

        private async Task Convert(FileInfo file)
        {
            for (int i = 0; i < 10; i++)
            {
                logger.LogInformation($"Converting process is running, {i * 10}% is done.");
                await Task.Delay(200);
            }
            logger.LogInformation($"Converting process is done.");
        }
    }
}