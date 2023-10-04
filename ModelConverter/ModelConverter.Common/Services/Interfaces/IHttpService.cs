using ModelConverter.Common.DTOs.Requestes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Services.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> PostAsync<T>(string url, T request) where T : ProcessRequestBase;

        Task<HttpResponseMessage> PutAsync<T>(string url, T request) where T : ProcessRequestBase;
    }
}