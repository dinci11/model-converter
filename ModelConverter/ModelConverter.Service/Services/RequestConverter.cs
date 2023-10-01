using Microsoft.AspNetCore.Http;
using ModelConverter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModelConverter.Service.Services
{
    public class RequestConverter : IRequestConverter
    {
        public T ConvertHttpRequest<T>(HttpRequest request)
        {
            using (var reader = new StreamReader(request.Body))
            {
                var jsonBody = reader.ReadToEnd();
                var requestT = JsonSerializer.Deserialize<T>(jsonBody);
                return requestT;
            }
        }
    }
}