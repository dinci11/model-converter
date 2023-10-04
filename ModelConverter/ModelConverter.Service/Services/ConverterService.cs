using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Enums;
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
        private readonly ITaskManagerService _taskManagerService;

        public ConverterService(IModelConverter modelConverter, ITaskManagerService taskManagerService)
        {
            this._modelConverter = modelConverter;
            _taskManagerService = taskManagerService;
        }

        public async Task Convert3DModelToNewFormatAsync(ModelConvertingRequest modelConvertingRequest)
        {
            try
            {
                var newFile = await _modelConverter.Convert(modelConvertingRequest.InputPath, modelConvertingRequest.TargetFormat.Value, modelConvertingRequest.OutputPath);
                _ = _taskManagerService.MarkProcessCompletedAsync(modelConvertingRequest.ProcessId, newFile);
            }
            catch (Exception ex)
            {
                _ = _taskManagerService.MarkProcessFailedAsync(modelConvertingRequest.ProcessId);
            }
        }
    }
}