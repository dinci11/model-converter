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
        Task<HttpResponseMessage> PostAsync(string cONVERT_URL, ModelConvertingRequest request);

        Task<HttpResponseMessage> PutAsync(string url, ProcessRequestBase request);
    }
}