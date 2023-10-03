using ModelConverter.Common.Enums;
using System.Text.Json.Serialization;

namespace ModelConverter.Common.DTOs.Responses
{
    public class ModelConvertingResponse
    {
        public string ProcessId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProcessStartResult ProcessStartResult { get; set; }
    }
}