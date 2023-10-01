using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Services.Interfaces
{
    public interface IRequestConverter
    {
        T ConvertHttpRequest<T>(HttpRequest request);
    }
}