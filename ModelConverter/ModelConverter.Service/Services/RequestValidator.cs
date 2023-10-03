using ModelConverter.Service.Constants;
using ModelConverter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelConverter.Common.DTOs.Requestes;

namespace ModelConverter.Service.Services
{
    public class RequestValidator : IRequestValidator
    {
        public bool ValidateRequest(ModelConvertingRequest request)
        {
            IsStringNullOrEmpty(request.InputPath, nameof(request.InputPath));
            IsInputFormatValid(request.InputPath);
            IsStringNullOrEmpty(request.OutputPath, nameof(request.OutputPath));
            return true;
        }

        private void IsInputFormatValid(string inputPath)
        {
            var fileExtension = new FileInfo(inputPath).Extension;
            IsStringNullOrEmpty(fileExtension, nameof(fileExtension));
            if (FileFormatNotSupported(fileExtension))
            {
                throw new Exception($"Input format {fileExtension} not supported");
            }
        }

        private bool FileFormatNotSupported(string fileExtension) =>
            !FileExtensions.IsFormatSupported(fileExtension);

        private void IsStringNullOrEmpty(string stringValue, string parameterName)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}