using System.Linq;

namespace EmuConsole
{
    public static class ReadIntExtensions
    {
        public static int? ReadInt(this IConsole console)
        {
            var input = console.ReadFormatted();
            return ParseInt(input);
        }

        public static int[] ReadDelimitedInt(this IConsole console, char delimiter = ',')
        {
            var inputs = console.ReadDelimitedLine(delimiter);
            return inputs.Select(ParseInt).Where(x => x != null).Select(x => x.Value).ToArray();
        }

        private static int? ParseInt(string input)
        {
            var isInt = int.TryParse(input, out var value);
            return isInt ? value : (int?)null;
        }
    }
}