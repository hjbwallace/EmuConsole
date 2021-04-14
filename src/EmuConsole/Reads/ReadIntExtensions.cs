using EmuConsole.Extensions;
using System;
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
            return inputs.SelectMany(ParseInts).Where(x => x != null).Select(x => x.Value).ToArray();
        }

        private static int? ParseInt(string input)
        {
            var isInt = int.TryParse(input, out var value);
            return isInt ? value : (int?)null;
        }

        internal static int?[] ParseInts(string input)
        {
            var splitInput = input?.Split('/').WherePopulated() ?? new string[0];

            if (splitInput.Length < 2)
                return new[] { ParseInt(splitInput.FirstOrDefault()) };

            var firstPart = ParseInt(splitInput[0]);
            var secondPart = ParseInt(splitInput[1]);

            if (firstPart == null || secondPart == null)
                return new int?[0];

            var min = Math.Min(firstPart.Value, secondPart.Value);
            var max = Math.Max(firstPart.Value, secondPart.Value);

            return Enumerable.Range(min, max - min + 1)
                .AsNullableInts()
                .ToArray();
        }
    }
}