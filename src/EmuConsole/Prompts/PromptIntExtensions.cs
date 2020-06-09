using EmuConsole.Extensions;

namespace EmuConsole
{
    public static class PromptIntExtensions
    {
        public static int PromptInt(this IConsole console)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                null,
                null,
                null,
                true).Value;
        }

        public static int PromptInt(this IConsole console, string promptMessage)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                promptMessage,
                null,
                null,
                true).Value;
        }

        public static int PromptInt(this IConsole console, string promptMessage, int defaultValue)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                promptMessage,
                null,
                defaultValue,
                false).Value;
        }

        public static int PromptInt(this IConsole console, string promptMessage, int[] allowedValues)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                promptMessage,
                allowedValues.AsNullableInts(),
                null,
                true).Value;
        }

        public static int PromptInt(this IConsole console, string promptMessage, int[] allowedValues, int defaultValue)
        {
            return console.PromptInputInternal(
                a => a.ReadInt(),
                promptMessage,
                allowedValues.AsNullableInts(),
                defaultValue,
                false).Value;
        }
    }
}