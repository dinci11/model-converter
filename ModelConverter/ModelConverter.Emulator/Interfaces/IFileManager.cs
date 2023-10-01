using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Emulator.Interfaces
{
    public interface IFileManager
    {
        public Task<FileInfo> LoadFile(string inputPath);

        public Task<FileInfo> SaveFile(FileInfo inputFile, FileInfo outputPath);
    }
}