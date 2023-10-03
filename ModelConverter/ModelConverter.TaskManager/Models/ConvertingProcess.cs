﻿using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Enums;
using ModelConverter.TaskManager.Services;
using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Models
{
    public class ConvertingProcess : ProcessBase
    {
        public string InputPath { get; private set; }
        public string OutputPath { get; private set; }
        public TargetFormat TargetFormat { get; private set; }

        private readonly IFunctionService _functionService;

        public ConvertingProcess(string processId, string inputPath, string outputPath, TargetFormat targetFormat)
        {
            ProcessId = processId;
            InputPath = inputPath;
            OutputPath = outputPath;
            TargetFormat = targetFormat;
            ProcessStatus = Enums.ProcessStatus.Waiting;

            _functionService = new FunctionService();
        }

        public override async Task Run(Action<Exception> onFailure = null)
        {
            try
            {
                ProcessStatus = Enums.ProcessStatus.InProgress;
                var request = new ConverterServiceRequest()
                {
                    InputPath = InputPath,
                    OutputPath = OutputPath,
                    TargetFormat = TargetFormat
                };
                var response = await _functionService.CallConverterService(request);
                ValidateConverterResponse(response);
                ProcessStatus = ProcessStatus.Completed;
            }
            catch (Exception ex)
            {
                ProcessStatus = Enums.ProcessStatus.Failed;
                onFailure?.Invoke(ex);
            }
        }

        private void ValidateConverterResponse(ConverterServiceResponse response)
        {
            IsProcessResultFinished(response);
        }

        private void IsProcessResultFinished(ConverterServiceResponse response)
        {
            if (response.ProcessResult == ProcessResult.Failed)
            {
                throw new Exception($"Process with id: {ProcessId} failed. Message: {response.FailingResult}");
            }
        }
    }
}