using ModelConverter.TaskManager.Enums;
using System.Text.Json.Serialization;

namespace ModelConverter.TaskManager.DTOs
{
    public class ConverterServiceRequest
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TargetFormat TargetFormat { get; set; }
    }
}