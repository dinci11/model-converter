using ModelConverter.TaskManager.Enums;
using ModelConverter.TaskManager.Models;

namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IProcessRepository<T> where T : ProcessBase
    {
        Task ScheduleProcess(T process);

        ProcessStatus GetProcessStatus(string processId);
    }
}