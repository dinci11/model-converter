using ModelConverter.Common.Enums;

namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IProcessManager
    {
        Task<ProcessStatus> GetProcessStatus(string processId);

        Task StarConverting(string processId, string inputFilePath, TargetFormat targetFormat);
    }
}