using System.Linq;

namespace EmuConsole
{
    public static class ConsoleReadExtensions
    {
        public static int? ReadInt(this IConsole console)
        {
            var input = console.ReadFormatted();
            return ParseInt(input);
        }

        public static double? ReadDouble(this IConsole console)
        {
            var input = console.ReadFormatted();
            return ParseDouble(input);
        }

        public static string ReadFormatted(this IConsole console)
        {
            return console.ReadLine()?.Trim() ?? string.Empty;
        }

        public static string[] ReadDelimitedLine(this IConsole console, char delimiter = ',')
        {
            var input = console.ReadFormatted();
            return input.Split(delimiter)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToArray();
        }

        public static double[] ReadDelimitedDouble(this IConsole console, char delimiter = ',')
        {
            var inputs = ReadDelimitedLine(console, delimiter);
            return inputs.Select(ParseDouble).Where(x => x != null).Select(x => x.Value).ToArray();
        }

        public static int[] ReadDelimitedInt(this IConsole console, char delimiter = ',')
        {
            var inputs = ReadDelimitedLine(console, delimiter);
            return inputs.Select(ParseInt).Where(x => x != null).Select(x => x.Value).ToArray();
        }

        private static double? ParseDouble(string input)
        {
            var isDouble = double.TryParse(input, out var value);
            return isDouble ? value : (double?)null;
        }

        private static int? ParseInt(string input)
        {
            var isInt = int.TryParse(input, out var value);
            return isInt ? value : (int?)null;
        }
    }
}