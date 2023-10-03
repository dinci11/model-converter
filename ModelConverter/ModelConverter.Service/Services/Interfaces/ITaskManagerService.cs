using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Services.Interfaces
{
    public interface ITaskManagerService
    {
        Task MarkProcessCompletedAsync(string processId, FileInfo newFile);

        Task MarkProcessFailedAsync(string processId);
    }
}