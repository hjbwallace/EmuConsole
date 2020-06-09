namespace EmuConsole
{
    public static class PromptInputExtensions
    {
        public static string PromptInput(this IConsole console)
        {
            return console.PromptInputInternal(
                a => a.ReadFormatted(),
                null,
                null,
                null,
                false);
        }

        public static string PromptInput(this IConsole console, string promptMessage)
        {
            return console.PromptInputInternal(
                a => a.ReadFormatted(),
                promptMessage,
                null,
                null,
                false);
        }

        public static string PromptInput(this IConsole console, string promptMessage, string[] allowedValues)
        {
            return console.PromptInputInternal(
                a => a.ReadFormatted(),
                promptMessage,
                allowedValues,
                null,
                true);
        }

        public static string PromptInput(this IConsole console, string promptMessage, string[] allowedValues, string defaultValue)
        {
            return console.PromptInputInternal(
                a => a.ReadFormatted(),
                promptMessage,
                allowedValues,
                defaultValue,
                false);
        }
    }
}