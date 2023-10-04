using ModelConverter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelConverter.Common.DTOs.Requestes
{
    public class StatusUpdateRequest : ProcessRequestBase
    {
        public string OutputPath { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProcessStatus? Status { get; set; }
    }
}