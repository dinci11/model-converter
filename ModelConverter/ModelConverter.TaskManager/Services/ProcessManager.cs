using ModelConverter.TaskManager.Constants;
using ModelConverter.TaskManager.Enums;
using ModelConverter.TaskManager.Models;
using ModelConverter.TaskManager.Services.Interfaces;
using System.Collections.Concurrent;

namespace ModelConverter.TaskManager.Services
{
    public class ProcessManager : IProcessManager
    {
        private readonly ILogger<ProcessManager> _logger;
        private readonly IProcessRepository<ConvertingProcess> _repository;

        //private readonly ConcurrentDictionary<string, Task>

        public ProcessManager(ILogger<ProcessManager> logger, IProcessRepository<ConvertingProcess> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task StarConverting(string processId, string inputFilePath, TargetFormat targetFormat)
        {
            var process = new ConvertingProcess(processId, inputFilePath, FileConstants.Directories.CONVERTED_FILE_DIRECTORY, targetFormat);
            _repository.ScheduleProcess(process);
            _logger.LogInformation($"Processing started on file: {inputFilePath}");
        }
    }
}