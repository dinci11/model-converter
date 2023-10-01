using Microsoft.AspNetCore.Http;
using ModelConverter.Emulator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelConverter.Service.DTOs.Requestes
{
    public class ModelConvertingRequest
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TargetFormat TargetFormat { get; set; }
    }
}