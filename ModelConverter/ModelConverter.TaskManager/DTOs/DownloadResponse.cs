namespace ModelConverter.TaskManager.DTOs
{
    public class DownloadResponse
    {
        public string ProcessId { get; set; }
        public string OriginalFileUrl { get; set; }
        public string ConvertedFileUrl { get; set; }
    }
}