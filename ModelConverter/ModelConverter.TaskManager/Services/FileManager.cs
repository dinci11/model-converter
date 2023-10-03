using ModelConverter.TaskManager.Constants;
using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Services
{
    public class FileManager : IFileManager
    {
        private readonly ILogger<FileManager> _logger;
        private readonly IProcessIdProvider _processIdProvider;

        public FileManager(ILogger<FileManager> logger, IProcessIdProvider processIdProvider)
        {
            _logger = logger;
            _processIdProvider = processIdProvider;
        }

        public async Task<string> SaveFile(IFormFile formFile)
        {
            var filelExtension = Path.GetExtension(formFile.FileName);
            var newFileName = $"{_processIdProvider.ProcessId}{filelExtension}";
            var filePath = Path.Combine(FileConstants.Directories.UPLOADED_FILE_DIRECTORY, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            var fileInfo = new FileInfo(filePath);
            if (FileNotExists(fileInfo))
            {
                throw new Exception("Saving file failed");
            }
            _logger.LogInformation($"{formFile.FileName} copied to {filePath}");
            return fileInfo.FullName;
        }

        private bool FileNotExists(FileInfo fileInfo) => !fileInfo.Exists;
    }
}