namespace EmuConsole
{
    public static class PromptInputExtensions
    {
        public static string PromptInput(this IConsole console)
        {
            return console.PromptInputInternal(
                null,
                null,
                null,
                false,
                false);
        }

        public static string PromptInput(this IConsole console, string promptMessage)
        {
            return console.PromptInputInternal(
                promptMessage,
                null,
                null,
                false,
                false);
        }

        public static string PromptInput(this IConsole console, string promptMessage, string[] allowedValues)
        {
            return console.PromptInputInternal(
                promptMessage,
                allowedValues,
                null,
                false,
                true);
        }

        public static string PromptInput(this IConsole console, string promptMessage, string[] allowedValues, string defaultValue)
        {
            return console.PromptInputInternal(
                promptMessage,
                allowedValues,
                defaultValue,
                true,
                false);
        }

        internal static string PromptInputInternal(this IConsole console, string promptMessage, string[] allowedValues, string defaultValue, bool hasDefault, bool retry)
        {
            return console.PromptValueInternal(
                a => a.ReadFormatted(),
                promptMessage,
                allowedValues,
                defaultValue,
                hasDefault,
                retry);
        }
    }
}