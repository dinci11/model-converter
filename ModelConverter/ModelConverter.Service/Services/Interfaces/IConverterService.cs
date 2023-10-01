using ModelConverter.Emulator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Services.Interfaces
{
    public interface IConverterService
    {
        Task Convert3DModelToNewFormat(string inputPath, TargetFormat targetFormat, string outputPath);
    }
}