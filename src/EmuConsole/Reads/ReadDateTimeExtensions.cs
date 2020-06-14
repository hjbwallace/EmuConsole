using System;

namespace EmuConsole
{
    public static class ReadDateTimeExtensions
    {
        public static DateTime? ReadDateTime(this IConsole console)
        {
            var input = console.ReadFormatted();
            return ParseDateTime(input);
        }

        private static DateTime? ParseDateTime(string input)
        {
            var idDateTime = DateTime.TryParse(input, out var value);
            return idDateTime ? value : (DateTime?)null;
        }
    }
}