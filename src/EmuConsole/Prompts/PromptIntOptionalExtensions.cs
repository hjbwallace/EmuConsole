using EmuConsole.Extensions;

namespace EmuConsole
{
    public static class PromptIntOptionalExtensions
    {
        public static int? PromptIntOptional(this IConsole console)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                null,
                null,
                null,
                false);
        }

        public static int? PromptIntOptional(this IConsole console, string promptMessage)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                promptMessage,
                null,
                null,
                false);
        }

        public static int? PromptIntOptional(this IConsole console, string promptMessage, int? defaultValue)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                promptMessage,
                null,
                defaultValue,
                false);
        }

        public static int? PromptIntOptional(this IConsole console, string promptMessage, int[] allowedValues)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                promptMessage,
                allowedValues.AsNullableInts(),
                null,
                true);
        }

        public static int? PromptIntOptional(this IConsole console, string promptMessage, int[] allowedValues, int? defaultValue)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                promptMessage,
                allowedValues.AsNullableInts(),
                defaultValue,
                false);
        }
    }
}