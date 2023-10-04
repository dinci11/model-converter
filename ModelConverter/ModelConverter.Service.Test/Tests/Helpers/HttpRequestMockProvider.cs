using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModelConverter.Service.Test.Tests.Helpers
{
    public class HttpRequestMockProvider
    {
        public HttpRequest httpRequest;

        public HttpRequestMockProvider()
        {
            httpRequest = Mock.Of<HttpRequest>();
        }

        public void SetRequestBody(object body)
        {
            var jsonString = JsonSerializer.Serialize(body);
            var requestBodyStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            httpRequest.Body = requestBodyStream;
        }
    }
}