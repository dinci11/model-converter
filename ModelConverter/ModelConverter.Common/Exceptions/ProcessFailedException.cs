using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Exceptions
{
    public class ProcessFailedException : Exception
    {
        public readonly string ProcessId;

        public ProcessFailedException(string processId) : base()
        {
            ProcessId = processId;
        }

        public ProcessFailedException(string processId, string message) : base(message)
        {
            ProcessId = processId;
        }
    }
}