using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Services.Interfaces
{
    public interface IConverterService
    {
        Task Convert3DModelToNewFormatAsync(ModelConvertingRequest modelConvertingRequest);
    }
}