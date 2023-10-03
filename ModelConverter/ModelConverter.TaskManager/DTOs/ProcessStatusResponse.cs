using ModelConverter.Common.Enums;
using System.Text.Json.Serialization;

namespace ModelConverter.TaskManager.DTOs
{
    public class ProcessStatusResponse
    {
        public string ProcessId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProcessStatus ProcessStatus { get; set; }
    }
}