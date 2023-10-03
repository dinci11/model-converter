using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Services
{
    public class HttpService : IHttpService
    {
        public async Task<HttpResponseMessage> PutAsync(string url, ProcessRequestBase request)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsJsonAsync(url, request);
                return response;
            }
        }
    }
}