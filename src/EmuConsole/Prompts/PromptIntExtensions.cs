using EmuConsole.Extensions;

namespace EmuConsole
{
    public static class PromptIntExtensions
    {
        public static int PromptInt(this IConsole console)
        {
            return console.PromptIntInternal(
                null,
                null,
                null,
                false,
                true);
        }

        public static int PromptInt(this IConsole console, string promptMessage)
        {
            return console.PromptIntInternal(
                promptMessage,
                null,
                null,
                false,
                true);
        }

        public static int PromptInt(this IConsole console, string promptMessage, int defaultValue)
        {
            return console.PromptIntInternal(
                promptMessage,
                null,
                defaultValue,
                true,
                false);
        }

        public static int PromptInt(this IConsole console, string promptMessage, int[] allowedValues)
        {
            return console.PromptIntInternal(
                promptMessage,
                allowedValues,
                null,
                false,
                true);
        }

        public static int PromptInt(this IConsole console, string promptMessage, int[] allowedValues, int defaultValue)
        {
            return console.PromptIntInternal(
                promptMessage,
                allowedValues,
                defaultValue,
                true,
                false);
        }

        internal static int PromptIntInternal(this IConsole console, string promptMessage, int[] allowedValues, int? defaultValue, bool hasDefault, bool retry)
        {
            return console.PromptIntOptionalInternal(
                promptMessage,
                allowedValues,
                defaultValue,
                hasDefault,
                retry).Value;
        }
    }
}