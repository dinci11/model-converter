using ModelConverter.TaskManager.Enums;

namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IProcessManager
    {
        Task StarConverting(string processId, string inputFilePath, TargetFormat targetFormat);
    }
}