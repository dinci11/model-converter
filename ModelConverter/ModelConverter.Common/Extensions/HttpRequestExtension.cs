using Microsoft.AspNetCore.Http;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModelConverter.Common.Extensions
{
    public static class HttpRequestExtension
    {
        public static async Task<T> GetObjectFromRequestBodyAsync<T>(this HttpRequest request) where T : class
        {
            try
            {
                var requestT = await JsonSerializer.DeserializeAsync<T>(request.Body);
                return requestT;
            }
            catch (JsonException ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public static T GetObjectFromRequestForm<T>(this HttpRequest request) where T : class
        {
            try
            {
                var requestJson = request.Form["body"];
                if (string.IsNullOrEmpty(requestJson))
                {
                    throw new BadRequestException("Key: 'body' should be defined in the form");
                }
                var requestT = JsonSerializer.Deserialize<T>(requestJson);
                return requestT;
            }
            catch (JsonException ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public static IFormFile GetFileFromRequest(this HttpRequest request)
        {
            if (request.Form.Files.Count != 1)
            {
                throw new BadRequestException("Request should contain exactl one file");
            }
            return request.Form.Files.FirstOrDefault();
        }

        public static string GetRequestParam(this HttpRequest request, string parameterName)
        {
            return request.Query[parameterName];
        }
    }
}