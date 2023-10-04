using ModelConverter.Common.Enums;
using ModelConverter.Common.Exceptions;
using ModelConverter.TaskManager.Models;
using ModelConverter.TaskManager.Services.Interfaces;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ModelConverter.TaskManager.Services
{
    public class ConvertingProcessRepository : IProcessRepository<ConvertingProcess>
    {
        private readonly ConcurrentDictionary<string, ConvertingProcess> _processPool;
        private readonly ILogger<ConvertingProcessRepository> _logger;

        public ConvertingProcessRepository(ILogger<ConvertingProcessRepository> logger)
        {
            _processPool = new ConcurrentDictionary<string, ConvertingProcess>();
            _logger = logger;
        }

        public async Task ScheduleProcess(ConvertingProcess process)
        {
            _processPool.TryAdd(process.ProcessId, process);
            _ = process.Run((ex) =>
            {
                _logger.LogWarning(ex.Message);
            });
        }

        public async Task<ConvertingProcess> GetProcessAsync(string processId)
        {
            var process = await Task.Run(() =>
            {
                return _processPool.GetValueOrDefault(processId);
            });

            if (process is null)
            {
                throw new NotFoundException($"There is no process with {processId}");
            }

            return process;
        }

        public async Task UpdateStatusAsync(string processId, ProcessStatus status, string outputPath = "")
        {
            var process = await GetProcessAsync(processId);

            switch (status)
            {
                case ProcessStatus.Completed:
                    process.MarkAsCompleted(outputPath); break;
                case ProcessStatus.Failed:
                    process.MarkAsFailed(); break;
                default:
                    process.MarkAsInProgress(); break;
            }

            await Task.Run(() => _processPool[processId] = process);
        }
    }
}