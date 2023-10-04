using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModelConverter.Common.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> GetObjectFromResponseBody<T>(this HttpResponseMessage response)
        {
            var responseT = await JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStream());
            return responseT;
        }
    }
}