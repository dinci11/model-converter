using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ModelConverter.TaskManager.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TargetFormat
    {
        Step,
        Iges,
        Stl,
        Obj
    }
}