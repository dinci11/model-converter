using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Services
{
    public class FileManager : IFileManager
    {
        public Task<string> SaveFile(IFormFile formFile)
        {
            throw new NotImplementedException();
        }
    }
}