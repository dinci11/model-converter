using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Enums
{
    public enum ProcessStatus
    {
        [EnumMember(Value = "waiting")]
        Waiting,

        [EnumMember(Value = "in-progress")]
        InProgress,

        [EnumMember(Value = "completed")]
        Completed,

        [EnumMember(Value = "failed")]
        Failed
    }
}