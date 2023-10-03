using ModelConverter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.DTOs.Requestes
{
    public class StatusUpdateRequest : ProcessRequestBase
    {
        public string OutputPath { get; set; }
        public ProcessStatus Status { get; set; }
    }
}