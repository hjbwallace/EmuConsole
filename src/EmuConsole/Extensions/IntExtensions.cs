using System.Collections.Generic;
using System.Linq;

namespace EmuConsole.Extensions
{
    internal static class IntExtensions
    {
        public static int?[] AsNullableInts(this IEnumerable<int> source)
        {
            return source?.Select(x => (int?)x).ToArray();
        }
    }
}