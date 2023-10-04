using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Common.Constants
{
    public class Routing
    {
        public class TaskManagerRoutes
        {
            public const string STATUS_URL = "http://localhost:5000/api/Status";

            public const string DOWNLOAD_ORIGINAL = "http://localhost:5000/api/Download/Original";

            public const string DOWNLOAD_CONVERTED = "http://localhost:5000/api/Download/Converted";
        }

        public class ConverterFunction
        {
            public const string CONVERT_URL = "http://localhost:7093/api/Converter";
        }
    }
}