using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    internal static class InternalPromptExtensions
    {
        public static T PromptInputInternal<T>(
            this IConsole console,
            Func<IConsole, T> inputFunc,
            string promptMessage,
            T[] allowedValues,
            T defaultValue,
            bool retry, 
            string promptError = null)
        {
            console.WritePromptMessage(promptMessage, promptError);

            var input = inputFunc(console);

            if (input.IsAllowed(allowedValues))
                return input;

            if (retry)
                return console.PromptInputInternal(inputFunc, null, allowedValues, defaultValue, retry, string.Format(console.Options.InvalidPromptTemplate ?? "", input?.ToString() ?? "-"));

            if (defaultValue != null)
                return defaultValue;

            return default;
        }

        public static T[] PromptInputsInternal<T>(
            this IConsole console,
            Func<IConsole, T[]> inputFunc,
            string promptMessage,
            T[] allowedValues,
            bool allowEmpty,
            string promptError = null)
        {
            console.WritePromptMessage(promptMessage, promptError);

            var inputs = allowedValues?.Any() == true
                ? inputFunc(console).Intersect(allowedValues).ToArray()
                : inputFunc(console);

            if (inputs.Any() || allowEmpty)
                return inputs;

            return console.PromptInputsInternal(inputFunc, null, allowedValues, allowEmpty,
                console.Options.InvalidPromptsTemplate);
        }

        private static void WritePromptMessage(this IConsole console, string promptMessage, string errorPrompt)
        {
            if (!string.IsNullOrWhiteSpace(promptMessage))
                console.WriteLine(promptMessage);

            console.WritePrompt(errorPrompt);
        }

        private static bool IsAllowed<T>(this T value, IEnumerable<T> allowedValues)
        {
            var allowed = allowedValues?.Where(x => x != null).ToArray() ?? new T[0];
            return value != null && (!allowed.Any() || allowed.Contains(value));
        }
    }
}