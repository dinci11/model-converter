using ModelConverter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelConverter.Emulator.Extensions
{
    public static class TargetFormatExtensions
    {
        public static string ToFormatString(this TargetFormat format)
        {
            var sb = new StringBuilder();
            sb.Append(".");
            sb.Append(format.ToString().ToLower());

            return sb.ToString();
        }
    }
}