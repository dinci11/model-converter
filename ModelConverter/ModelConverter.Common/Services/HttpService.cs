using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModelConverter.Common.Services
{
    public class HttpService : IHttpService
    {
        public async Task<HttpResponseMessage> PostAsync<T>(string url, T request) where T : ProcessRequestBase
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, GetJsonBody(request));
                return response;
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T request) where T : ProcessRequestBase
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(url, GetJsonBody(request));
                return response;
            }
        }

        private static StringContent GetJsonBody<T>(T request) where T : ProcessRequestBase
        {
            var jsonBody = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            return content;
        }
    }
}