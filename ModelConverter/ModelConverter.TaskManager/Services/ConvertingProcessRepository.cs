using ModelConverter.TaskManager.Enums;
using ModelConverter.TaskManager.Models;
using ModelConverter.TaskManager.Services.Interfaces;
using System.Collections.Concurrent;

namespace ModelConverter.TaskManager.Services
{
    public class ConvertingProcessRepository : IProcessRepository<ConvertingProcess>
    {
        private readonly ConcurrentDictionary<string, ConvertingProcess> processPool;
        private readonly ILogger<ConvertingProcessRepository> _logger;

        public ConvertingProcessRepository(ILogger<ConvertingProcessRepository> logger)
        {
            processPool = new ConcurrentDictionary<string, ConvertingProcess>();
            _logger = logger;
        }

        public async Task ScheduleProcess(ConvertingProcess process)
        {
            processPool.TryAdd(process.ProcessId, process);
            process.Run((ex) =>
            {
                _logger.LogWarning(ex.Message);
            });
        }

        public ProcessStatus GetProcessStatus(string processId)
        {
            var process = processPool[processId];
            if (process == null)
            {
                throw new Exception($"Process with id: {processId} not found");
            }
            return process.ProcessStatus;
        }
    }
}