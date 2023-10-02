namespace ModelConverter.TaskManager.Services.Interfaces
{
    public interface IProcessManager
    {
        Task<string> StarConverting(string inputFilePath);
    }
}