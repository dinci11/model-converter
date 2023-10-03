using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ModelConverter.TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class TaskManagerControllerBase : ControllerBase
    {
        protected ILogger<UploadController> logger;

        protected TaskManagerControllerBase(ILogger<UploadController> logger)
        {
            this.logger = logger;
        }

        protected async Task<TRequest> GetRequestFromForm<TRequest>() where TRequest : class
        {
            var formRequest = Request.Form["request"];
            ValidateString(formRequest);
            return DeserializeStringToTRequest<TRequest>(formRequest);
        }

        private TRequest DeserializeStringToTRequest<TRequest>(string jsonString) where TRequest : class
        {
            var requestT = JsonSerializer.Deserialize<TRequest>(jsonString);
            IsRequestNotNull(requestT);
            return requestT;
        }

        private void IsRequestNotNull(object requestT)
        {
            if (requestT is null)
            {
                throw new Exception("Request cannot be serialized!");
            }
        }

        protected async Task<IFormFile> GetFileFromRequest()
        {
            var requestFile = Request.Form.Files.FirstOrDefault();
            ValidateRequestFile(requestFile);
            return requestFile;
        }

        private void ValidateString(string stringValue)
        {
            if (StringIsNullOrEmpty(stringValue))
            {
                throw new Exception("Request body is empty");
            }
        }

        private void ValidateRequestFile(IFormFile requestFile)
        {
            IsFileNotNull(requestFile);
            IsFileSupported(requestFile);
        }

        private void IsFileNotNull(IFormFile? requestFile)
        {
            if (requestFile is null)
            {
                throw new Exception("There is no file in the request");
            }
        }

        private void IsFileSupported(IFormFile requestFile)
        {
            if (!requestFile.FileName.EndsWith(".shapr"))
            {
                throw new Exception("Only .shapr file is supported");
            }
        }

        private bool StringIsNullOrEmpty(string requestBody) => string.IsNullOrEmpty(requestBody);
    }
}