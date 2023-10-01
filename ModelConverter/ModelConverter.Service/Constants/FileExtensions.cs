using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Service.Constants
{
    public class FileExtensions
    {
        private static readonly string[] SUPPORTED_INPUT_FORMATS = { ".shapr" };

        public static bool IsFormatSupported(string fileFormat) => SUPPORTED_INPUT_FORMATS.Contains(fileFormat);
    }
}