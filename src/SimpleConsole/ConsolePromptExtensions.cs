using System.Linq;

namespace SimpleConsole
{
    public static class ConsolePromptExtensions
    {
        public static string PromptInput(this IConsole console, string prompt, params string[] allowed)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                console.WriteLine(prompt);

            console.Write("> ");
            var input = console.ReadFormatted();

            if (allowed.Any() && !allowed.Contains(input))
                return console.PromptInput(null, allowed);

            return input;
        }

        public static int PromptInputInt(this IConsole console, string prompt, params int[] allowed)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                console.WriteLine(prompt);

            console.Write("> ");
            var input = console.ReadInt();

            if (input == null || (allowed.Any() && !allowed.Contains(input.Value)))
                return console.PromptInputInt(null, allowed);

            return input.Value;
        }

        public static bool PromptConfirm(this IConsole console, string prompt)
        {
            var input = console.PromptInput($"{prompt ?? string.Empty} (Y to confirm)").ToUpper();
            return input == "Y";
        }
    }
}