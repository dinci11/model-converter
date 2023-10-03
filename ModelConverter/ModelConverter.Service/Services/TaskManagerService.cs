using ModelConverter.Common.Constants;
using ModelConverter.Common.DTOs.Requestes;
using ModelConverter.Common.Enums;
using ModelConverter.Common.Services.Interfaces;
using ModelConverter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Services
{
    public class TaskManagerService : ITaskManagerService
    {
        private readonly IHttpService _httpService;

        public TaskManagerService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task MarkProcessCompletedAsync(string processId, FileInfo newFile)
        {
            var request = new StatusUpdateRequest()
            {
                OutputPath = newFile.FullName,
                ProcessId = processId,
                Status = ProcessStatus.Completed
            };

            _httpService.PutAsync(Routing.TaskManagerRoutes.STATUS_UPDATE_URL, request);
        }

        public async Task MarkProcessFailedAsync(string processId)
        {
            var request = new StatusUpdateRequest
            {
                ProcessId = processId,
                Status = ProcessStatus.Failed,
                OutputPath = string.Empty
            };
            _httpService.PutAsync(Routing.TaskManagerRoutes.STATUS_UPDATE_URL, request);
        }
    }
}