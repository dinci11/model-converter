using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelConverter.Common.Constants;
using ModelConverter.Common.Enums;
using ModelConverter.Common.Exceptions;
using ModelConverter.Common.Extensions;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.TaskManager.DTOs;
using ModelConverter.TaskManager.Enums;
using ModelConverter.TaskManager.Models;
using ModelConverter.TaskManager.Services.Interfaces;

namespace ModelConverter.TaskManager.Controllers
{
    public class DownloadController : TaskManagerControllerBase
    {
        public DownloadController(ILogger<UploadController> logger, IExceptionHandler exceptionHandler, IProcessManager processManager)
            : base(logger, exceptionHandler, processManager)
        {
        }

        [HttpGet]
        [Route("GetUrl")]
        public async Task<IActionResult> GetDownloadUrl()
        {
            try
            {
                _logger.LogInformation($"GetUrl Endpoint invoked.");

                var processId = GetProcessIdFromRequest();
                var process = await _processManager.GetProcessAsync(processId);

                CheckProcessStatus(process);

                var response = new DownloadResponse
                {
                    ProcessId = processId,
                    OriginalFileUrl = $"{Routing.TaskManagerRoutes.DOWNLOAD_ORIGINAL}?processId={processId}",
                    ConvertedFileUrl = $"{Routing.TaskManagerRoutes.DOWNLOAD_CONVERTED}?processId={processId}"
                };

                _logger.LogInformation($"Endpoint finished");
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"GetUrl Endpoint thrown an exception");
                return await _exceptionHandler.HandleExceptionAsync(ex);
            }
        }

        [HttpGet]
        [Route("Original")]
        public async Task<IActionResult> GetOriginalFile()
        {
            try
            {
                _logger.LogInformation($"GetOriginal Endpoint invoked.");

                var response = await GetFile(FileVersion.Original);

                _logger.LogInformation($"GetOriginal Endpoint finished.");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"GetOriginal Endpoint thrown an exception.");
                return await _exceptionHandler.HandleExceptionAsync(ex);
            }
        }

        [HttpGet]
        [Route("Converted")]
        public async Task<IActionResult> GetConvertedFile()
        {
            try
            {
                _logger.LogInformation($"GetConverted Endpoint invoked.");

                var response = await GetFile(FileVersion.Converted);

                _logger.LogInformation($"GetConverted Endpoint finished.");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"GetConverted Endpoint thrown an exception.");
                return await _exceptionHandler.HandleExceptionAsync(ex);
            }
        }

        private async Task<IActionResult> GetFile(FileVersion fileVersion)
        {
            var processId = GetProcessIdFromRequest();
            var process = await _processManager.GetProcessAsync(processId);
            CheckProcessStatus(process);
            var pathToRetrive = fileVersion == FileVersion.Original ? process.InputPath : process.OutputPath;
            var filePath = GetFilePath(pathToRetrive);
            return PhysicalFile(filePath, "application/octet-stream");
        }

        private string GetFilePath(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if (IsFileNotExists(fileInfo))
            {
                throw new NotFoundException($"File {fileInfo.Name} not found");
            }
            return fileInfo.FullName;
        }

        private bool IsFileNotExists(FileInfo fileInfo) => !fileInfo.Exists;

        private static void CheckProcessStatus(ConvertingProcess process)
        {
            if (process.ProcessStatus != ProcessStatus.Completed)
            {
                throw new NotFoundException("Process is not completed, check status");
            }
        }

        private string GetProcessIdFromRequest()
        {
            var processId = Request.GetRequestParam("processId");
            ValidateProcessId(processId);
            return processId;
        }

        private void ValidateProcessId(string processId)
        {
            if (IsStringNullOrEmpty(processId))
            {
                throw new BadRequestException("ProcessId must be declared");
            }
        }

        private bool IsStringNullOrEmpty(string processId) => string.IsNullOrEmpty(processId);
    }
}