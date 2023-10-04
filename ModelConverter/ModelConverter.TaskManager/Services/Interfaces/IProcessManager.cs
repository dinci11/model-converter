using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Enums;

namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IProcessManager
    {
        Task<ProcessStatus> GetProcessStatusAsync(string processId);

        Task StarConvertingAsync(string processId, string inputFilePath, TargetFormat targetFormat);

        Task UpdateProcessStatusAsync(StatusUpdateRequest updateRequest);
    }
}