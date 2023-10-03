using ModelConverter.Common.Enums;
using System.Text.Json.Serialization;

namespace ModelConverter.TaskManager.DTOs
{
    public class UploadRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TargetFormat? TargetFormat { get; set; }
    }
}