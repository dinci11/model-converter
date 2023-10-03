using ModelConverter.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.DTOs.Responses
{
    public class ModelConvertingResponse
    {
        public ModelConvertingResponse(ProcessStatus processStatus, string outputFilePath = null, string failingResult = null)
        {
            ProcessStatus = processStatus;
            OutputFilePath = outputFilePath;
            FailingResult = failingResult;
        }

        public string OutputFilePath { get; private set; }

        public ProcessStatus ProcessStatus { get; private set; }

        public string FailingResult { get; private set; }
    }
}