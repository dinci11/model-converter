using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Enums;
using ModelConverter.TaskManager.Models;

namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IProcessManager
    {
        Task<ConvertingProcess> GetProcessAsync(string processId);

        Task<ProcessStatus> GetProcessStatusAsync(string processId);

        Task StarConvertingAsync(string processId, string inputFilePath, TargetFormat targetFormat);

        Task UpdateProcessStatusAsync(StatusUpdateRequest updateRequest);
    }
}