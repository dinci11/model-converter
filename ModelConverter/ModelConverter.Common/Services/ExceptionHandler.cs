using Microsoft.AspNetCore.Mvc;
using ModelConverter.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Services
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async Task<IActionResult> HandleException(Exception exception)
        {
            var response = new ObjectResult("Exception handeld");
            response.StatusCode = 500;
            return response;
        }
    }
}