using ModelConverter.Service.DTOs.Requestes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Services.Interfaces
{
    public interface IRequestValidator
    {
        bool ValidateRequest(ModelConvertingRequest request);
    }
}