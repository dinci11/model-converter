using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Enums;
using ModelConverter.TaskManager.Constants;
using ModelConverter.TaskManager.Models;
using ModelConverter.TaskManager.Services.Interfaces;
using System.Collections.Concurrent;

namespace ModelConverter.TaskManager.Services
{
    public class ProcessManager : IProcessManager
    {
        private readonly ILogger<ProcessManager> _logger;
        private readonly IProcessRepository<ConvertingProcess> _repository;

        public ProcessManager(ILogger<ProcessManager> logger, IProcessRepository<ConvertingProcess> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<ProcessStatus> GetProcessStatusAsync(string processId)
        {
            var process = await _repository.GetProcessAsync(processId);
            return process.ProcessStatus;
        }

        public async Task StarConvertingAsync(string processId, string inputFilePath, TargetFormat targetFormat)
        {
            var process = new ConvertingProcess(processId, inputFilePath, FileConstants.Directories.CONVERTED_FILE_DIRECTORY, targetFormat);
            _repository.ScheduleProcess(process);
            _logger.LogInformation($"Processing started on file: {inputFilePath}");
        }

        public async Task UpdateProcessStatusAsync(StatusUpdateRequest updateRequest)
        {
            await _repository.UpdateStatusAsync(updateRequest.ProcessId, updateRequest.Status.Value, updateRequest.OutputPath);
        }
    }
}