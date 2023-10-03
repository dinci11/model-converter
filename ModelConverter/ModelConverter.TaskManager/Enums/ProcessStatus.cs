using System.Runtime.Serialization;

namespace ModelConverter.TaskManager.Enums
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