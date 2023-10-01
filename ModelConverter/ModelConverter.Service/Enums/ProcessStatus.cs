using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProcessStatus
    {
        [EnumMember(Value = "finished")]
        Finished,

        [EnumMember(Value = "failed")]
        Failed
    }
}