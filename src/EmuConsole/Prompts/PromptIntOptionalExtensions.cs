using EmuConsole.Extensions;

namespace EmuConsole
{
    public static class PromptIntOptionalExtensions
    {
        public static int? PromptIntOptional(this IConsole console)
        {
            return console.PromptIntOptionalInternal(
                null,
                null,
                null,
                false,
                false);
        }

        public static int? PromptIntOptional(this IConsole console, string promptMessage)
        {
            return console.PromptIntOptionalInternal(
                promptMessage,
                null,
                null,
                false,
                false);
        }

        public static int? PromptIntOptional(this IConsole console, string promptMessage, int? defaultValue)
        {
            return console.PromptIntOptionalInternal(
                promptMessage,
                null,
                defaultValue,
                true,
                false);
        }

        public static int? PromptIntOptional(this IConsole console, string promptMessage, int[] allowedValues)
        {
            return console.PromptIntOptionalInternal(
                promptMessage,
                allowedValues,
                null,
                false,
                true);
        }

        public static int? PromptIntOptional(this IConsole console, string promptMessage, int[] allowedValues, int? defaultValue)
        {
            return console.PromptIntOptionalInternal(
                promptMessage,
                allowedValues,
                defaultValue,
                true,
                false);
        }

        internal static int? PromptIntOptionalInternal(this IConsole console, string promptMessage, int[] allowedValues, int? defaultValue, bool hasDefault, bool retry)
        {
            return console.PromptValueInternal(
                a => a.ReadInt(),
                promptMessage,
                allowedValues.AsNullableInts(),
                defaultValue,
                hasDefault,
                retry);
        }
    }
}