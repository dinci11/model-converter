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

        public async Task<FileInfo> SaveFile(FileInfo inputFile, FileInfo outputPath)
        {
            if (PathNotExists(outputPath))
            {
                throw new Exception($"Directory {outputPath.Directory.FullName} not existing");
            }
            File.Copy(inputFile.FullName, outputPath.FullName, true);
            return new FileInfo(outputPath.FullName);
        }

        private bool PathNotExists(FileInfo path) => !path.Directory.Exists;
    }
}