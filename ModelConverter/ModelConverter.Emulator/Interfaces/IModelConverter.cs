using ModelConverter.Emulator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Emulator.Interfaces
{
    public interface IModelConverter
    {
        public Task<FileInfo> Convert(string inputPath, TargetFormat targetFormat, string outputPath);
    }
}