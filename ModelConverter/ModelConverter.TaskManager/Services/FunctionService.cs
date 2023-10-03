using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Services.Interfaces;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelConverter.TaskManager.Services
{
    public class FunctionService : IFunctionService
    {
        private const string URL = "http://localhost:7093/api/Converter";

        public async Task<ConverterServiceResponse> CallConverterService(ConverterServiceRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                var jsonBody = JsonSerializer.Serialize(request);
                var jsonContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(URL, jsonContent);
                ValidateResponse(response);
                var converterResponse = await DeserializeResponse<ConverterServiceResponse>(response);
                return converterResponse;
            }
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