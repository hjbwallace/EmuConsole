using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    internal static class InternalPromptExtensions
    {
        public static T PromptValueInternal<T>(
            this IConsole console,
            Func<IConsole, T> inputFunc,
            string promptMessage,
            T[] allowedValues,
            T defaultValue,
            bool hasDefault,
            bool retry,
            string promptError = null)
        {
            console.WritePromptMessage(promptMessage, promptError);

            var input = inputFunc(console);

            if (input.IsAllowed(allowedValues))
                return input;

            if (hasDefault)

                return defaultValue;

            if (retry)
                return console.PromptValueInternal(inputFunc, null, allowedValues, defaultValue, hasDefault, retry, string.Format(console.Options.InvalidPromptTemplate ?? "", input?.ToString() ?? "-"));

            return default;
        }

        public static T[] PromptValuesInternal<T>(
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

            return console.PromptValuesInternal(inputFunc, null, allowedValues, allowEmpty,
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