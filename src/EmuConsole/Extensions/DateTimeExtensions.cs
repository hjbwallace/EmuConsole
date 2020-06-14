using System;
using System.Linq;

namespace EmuConsole.Extensions
{
    internal static class DateTimeExtensions
    {
        public static DateTime?[] AsNullableDateTimes(this DateTime[] source)
        {
            return source?.Select(x => (DateTime?)x).ToArray();
        }
    }
}