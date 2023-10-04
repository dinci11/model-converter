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
            public const string STATUS_URL = "http://localhost:500/api/Status";
        }

        public class ConverterFunction
        {
            public const string CONVERT_URL = "http://localhost:7093/api/Converter";
        }
    }
}