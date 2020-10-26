using System.Linq;

namespace EmuConsole
{
    public static class ReadDoubleExtensions
    {
        public static double? ReadDouble(this IConsole console)
        {
            var input = console.ReadFormatted();
            return ParseDouble(input);
        }

        public static double[] ReadDelimitedDouble(this IConsole console, char delimiter = ',')
        {
            var inputs = console.ReadDelimitedLine(delimiter);
            return inputs.Select(ParseDouble).Where(x => x != null).Select(x => x.Value).ToArray();
        }

        private static double? ParseDouble(string input)
        {
            var isDouble = double.TryParse(input, out var value);
            return isDouble ? value : (double?)null;
        }
    }
}