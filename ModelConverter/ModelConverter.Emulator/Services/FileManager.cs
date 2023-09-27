using ModelConverter.Emulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Emulator.Services
{
    public class FileManager : IFileManager
    {
        public async Task<FileInfo> LoadFile(string inputPath)
        {
            var fileInfo = new FileInfo(inputPath);
            return fileInfo;
        }

        public async Task<FileInfo> SaveFile(FileInfo fileInfo, string outputPath)
        {
            File.Copy(fileInfo.FullName, outputPath, true);
            return new FileInfo(outputPath);
        }
    }
}