using System.Linq;

namespace EmuConsole.Extensions
{
    internal static class IntExtensions
    {
        public static int?[] AsNullableInts(this int[] source)
        {
            return source?.Select(x => (int?)x).ToArray();
        }
    }
}