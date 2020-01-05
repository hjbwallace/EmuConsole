namespace SimpleConsole
{
    public static class ConsoleReadExtensions
    {
        public static int? ReadInt(this IConsole console)
        {
            var input = console.ReadLine()?.Trim() ?? string.Empty;
            var isInt = int.TryParse(input, out var value);
            return isInt ? value : (int?)null;
        }

        public static double? ReadDouble(this IConsole console)
        {
            var input = console.ReadLine()?.Trim() ?? string.Empty;
            var isDouble = double.TryParse(input, out var value);
            return isDouble ? value : (double?)null;
        }
    }
}