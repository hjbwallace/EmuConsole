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
            bool retry)
        {
            console.WritePromptMessage(promptMessage);

            var input = inputFunc(console);

            if (input.IsAllowed(allowedValues))
                return input;

            if (retry)
                return console.PromptInputInternal(inputFunc, null, allowedValues, defaultValue, retry);

            if (defaultValue != null)
                return defaultValue;

            return default;
        }

        public static T[] PromptInputsInternal<T>(
            this IConsole console,
            Func<IConsole, T[]> inputFunc,
            string promptMessage,
            T[] allowedValues,
            bool allowEmpty)
        {
            console.WritePromptMessage(promptMessage);

            var inputs = allowedValues?.Any() == true
                ? inputFunc(console).Intersect(allowedValues).ToArray()
                : inputFunc(console);

            if (inputs.Any() || allowEmpty)
                return inputs;

            return console.PromptInputsInternal(inputFunc, null, allowedValues, allowEmpty);
        }

        private static void WritePromptMessage(this IConsole console, string promptMessage)
        {
            if (!string.IsNullOrWhiteSpace(promptMessage))
                console.WriteLine(promptMessage);

            console.WritePrompt();
        }

        private static bool IsAllowed<T>(this T value, IEnumerable<T> allowedValues)
        {
            var allowed = allowedValues?.Where(x => x != null).ToArray() ?? new T[0];
            return value != null && (!allowed.Any() || allowed.Contains(value));
        }
    }
}