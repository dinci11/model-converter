using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProcessResult
    {
        Success,
        Failed
    }
}