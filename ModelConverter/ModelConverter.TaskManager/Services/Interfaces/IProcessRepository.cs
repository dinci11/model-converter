using ModelConverter.Common.Enums;
using ModelConverter.TaskManager.Models;

namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IProcessRepository<T> where T : ProcessBase
    {
        Task ScheduleProcess(T process);

        Task<T> GetProcessByIdAsync(string processId);

        Task UpdateStatusAsync(string processId, ProcessStatus status, string outputPath);
    }
}