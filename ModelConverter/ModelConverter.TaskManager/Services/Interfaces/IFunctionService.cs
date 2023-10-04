using ModelConverter.TaskManager.DTOs;

namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IFunctionService
    {
        Task<ConverterServiceResponse> StartConverting(ConverterServiceRequest request);
    }
}