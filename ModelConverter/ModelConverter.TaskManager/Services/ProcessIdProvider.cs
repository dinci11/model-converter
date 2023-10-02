using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Services
{
    public class ProcessIdProvider : IProcessIdProvider
    {
        public string ProcessId { get; private set; }

        public ProcessIdProvider()
        {
            ProcessId = Guid.NewGuid().ToString();
        }
    }
}