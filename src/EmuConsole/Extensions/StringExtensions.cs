using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole.Extensions
{
    internal static class StringExtensions
    {
        public static string[] WherePopulated(this IEnumerable<string> source)
        {
            return source?.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }

        public static bool Contains(this string text, string value, StringComparison stringComparison)
        {
            if (text == null)
                return false;

            return text.IndexOf(value, stringComparison) >= 0;
        }
    }
}