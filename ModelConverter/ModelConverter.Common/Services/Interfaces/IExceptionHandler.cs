using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Services.Interfaces
{
    public interface IExceptionHandler
    {
        Task<IActionResult> HandleException(Exception exception);
    }
}