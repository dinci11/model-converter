using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.DTOs.Responses;
using ModelConverter.TaskManager.DTOs;

namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IFunctionService
    {
        Task<ModelConvertingResponse> StartConverting(ModelConvertingRequest request);
    }
}