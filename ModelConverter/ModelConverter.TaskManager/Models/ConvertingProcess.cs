using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.DTOs.Responses;
using ModelConverter.Common.Enums;
using ModelConverter.Common.Exceptions;
using ModelConverter.TaskManager.DTOs;
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
            ProcessStatus = ProcessStatus.Waiting;

            _functionService = new FunctionService();
        }

        public override async Task Run(Action<Exception> onFailure)
        {
            try
            {
                var request = new ModelConvertingRequest()
                {
                    ProcessId = ProcessId,
                    InputPath = InputPath,
                    OutputPath = OutputPath,
                    TargetFormat = TargetFormat
                };
                var response = await _functionService.StartConverting(request);
                ValidateConverterResponse(response);
                ProcessStatus = ProcessStatus.InProgress;
            }
            catch (Exception ex)
            {
                ProcessStatus = ProcessStatus.Failed;
                onFailure.Invoke(ex);
            }
        }

        private void ValidateConverterResponse(ModelConvertingResponse response)
        {
            IsProcessResultFinished(response);
        }

        private void IsProcessResultFinished(ModelConvertingResponse response)
        {
            if (response.ProcessStartResult == ProcessStartResult.Failed)
            {
                throw new Exception($"Process with id: {ProcessId} failed to start.");
            }
        }

        public void MarkAsInProgress()
        {
            ProcessStatus = ProcessStatus.InProgress;
        }

        public void MarkAsCompleted(string outputPath)
        {
            ProcessStatus = ProcessStatus.Completed;
            OutputPath = outputPath;
        }

        public void MarkAsFailed()
        { ProcessStatus = ProcessStatus.Failed; }
    }
}