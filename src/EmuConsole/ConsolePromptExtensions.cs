using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public static class ConsolePromptExtensions
    {
        public static string PromptInput(this IConsole console, string prompt, params string[] allowed)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                console.WriteLine(prompt);

            console.WritePrompt();
            var input = console.ReadFormatted();

            if (allowed.Any() && !allowed.Contains(input))
                return console.PromptInput(null, allowed);

            return input;
        }

        public static int PromptInputInt(this IConsole console, string prompt, params int[] allowed)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                console.WriteLine(prompt);

            console.WritePrompt();
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

        public static T PromptIndexSelection<T>(this IConsole console, IEnumerable<T> source)
        {
            var indexCollection = new IndexCollection<T>(source);
            return indexCollection.GetSelection(console);
        }

        public static T PromptIndexSelection<T>(this IConsole console, IEnumerable<T> source, Func<T, object> descriptionSelector)
        {
            var indexCollection = new IndexCollection<T>(source, descriptionSelector);
            return indexCollection.GetSelection(console);
        }
    }
}