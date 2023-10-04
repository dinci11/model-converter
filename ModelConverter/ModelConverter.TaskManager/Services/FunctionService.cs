using ModelConverter.Common.Constants;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.DTOs.Responses;
using ModelConverter.Common.Extensions;
using ModelConverter.Common.Services;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Services.Interfaces;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelConverter.TaskManager.Services
{
    public class FunctionService : IFunctionService
    {
        private IHttpService _httpService;

        public FunctionService()
        {
            _httpService = new HttpService();
        }

        public async Task<ModelConvertingResponse> StartConverting(ModelConvertingRequest request)
        {
            var response = await _httpService.PostAsync(Routing.ConverterFunction.CONVERT_URL, request);
            var converterResponse = await response.GetObjectFromResponseBody<ModelConvertingResponse>();
            return converterResponse;
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            var responseT = JsonSerializer.Deserialize<T>(body);
            if (responseT is null)
            {
                throw new Exception("Cannot desirializ response body");
            }
            return responseT;
        }

        private void ValidateResponse(HttpResponseMessage response)
        {
            if (StatusCodeNotSuccess(response))
            {
                throw new Exception($"Response was not sucess. StatusCode: {response.StatusCode}");
            }
        }

        private bool StatusCodeNotSuccess(HttpResponseMessage response) => !response.IsSuccessStatusCode;
    }
}