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

        public static string PromptOptionalInput(this IConsole console, string prompt, string[] allowed, string defaultValue = null)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                console.WriteLine(prompt);

            console.WritePrompt();
            var input = console.ReadFormatted();

            if (allowed.Any() && !allowed.Contains(input))
                return defaultValue;

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

        public static int? PromptInputOptionalInt(this IConsole console, string prompt, int[] allowed, int? defaultValue = null)
        {
            if (defaultValue.HasValue && !allowed.Contains(defaultValue.Value))
                throw new ArgumentException("Default value must be included in the allowed values");

            if (!string.IsNullOrWhiteSpace(prompt))
                console.WriteLine(prompt);

            console.WritePrompt();
            var input = console.ReadInt();

            if (input.HasValue && allowed.Any() && allowed.Contains(input.Value))
                return input.Value;

            return defaultValue;
        }

        private static void WritePromptInternal(IConsole console, string promptMessage, object defaultValue = null)
        {
            var prompt = string.Join(" ", new[] { promptMessage, defaultValue?.ToString() }.Where(x => x != null));

            if (!string.IsNullOrWhiteSpace(prompt))
                console.WriteLine(prompt);
        }

        public static int[] PromptInputDelimitedInt(this IConsole console, string prompt, int[] allowed, bool allowEmpty)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                console.WriteLine(prompt);

            console.WritePrompt();
            var inputs = console.ReadDelimitedInt();

            if (allowed.Any())
                inputs = inputs.Intersect(allowed).ToArray();

            if (!allowEmpty && !inputs.Any())
                return console.PromptInputDelimitedInt(null, allowed, allowEmpty);

            return inputs;
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

        public static T[] PromptMultipleIndexSelection<T>(this IConsole console, IEnumerable<T> source, bool allowEmpty = false)
        {
            var indexCollection = new MultipleIndexCollection<T>(source, allowEmpty);
            return indexCollection.GetSelection(console);
        }

        public static T[] PromptMultipleIndexSelection<T>(this IConsole console, IEnumerable<T> source, Func<T, object> descriptionSelector, bool allowEmpty = false)
        {
            var indexCollection = new MultipleIndexCollection<T>(source, descriptionSelector, allowEmpty);
            return indexCollection.GetSelection(console);
        }
    }
}