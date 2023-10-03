using ModelConverter.TaskManager.Models;
using ModelConverter.TaskManager.Services.Interfaces;
using System.Collections.Concurrent;
using System.Diagnostics;

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

        public async Task<ConvertingProcess> GetProcessAsync(string processId)
        {
            var process = await Task.Run(() =>
            {
                return processPool.GetValueOrDefault(processId);
            });

            if (process is null)
            {
                throw new Exception($"There is no process with {processId}");
            }

            return process;
        }
    }
}