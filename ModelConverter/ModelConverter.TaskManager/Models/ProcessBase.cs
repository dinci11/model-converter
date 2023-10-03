using ModelConverter.Common.Enums;

namespace ModelConverter.TaskManager.Models
{
    public abstract class ProcessBase
    {
        public string ProcessId { get; protected set; }
        public ProcessStatus ProcessStatus { get; protected set; }

        public abstract Task Run(Action<Exception> onFailure);
    }
}