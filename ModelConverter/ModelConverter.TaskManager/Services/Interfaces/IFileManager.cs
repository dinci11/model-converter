namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IFileManager
    {
        Task<string> SaveFile(IFormFile formFile);
    }
}