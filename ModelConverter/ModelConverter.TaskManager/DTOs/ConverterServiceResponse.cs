using ModelConverter.Common.Enums;

namespace ModelConverter.TaskManager.DTOs
{
    public class ConverterServiceResponse
    {
        public string OutputFilePath { get; private set; }

        public ProcessStartResult ProcessResult { get; private set; }

        public string FailingResult { get; private set; }
    }
}