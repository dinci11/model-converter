namespace ModelConverter.TaskManager.DTOs
{
    public class UploadResponse
    {
        public string ProcessId { get; set; }
        public string ProcessStatusUrl { get; set; }

        public UploadResponse()
        {
            ProcessId = string.Empty;
            ProcessStatusUrl = string.Empty;
        }
    }
}