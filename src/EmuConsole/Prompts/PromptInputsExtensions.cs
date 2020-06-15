namespace EmuConsole
{
    public static class PromptDelimitedInputExtensions
    {
        public static string[] PromptInputs(this IConsole console, bool allowEmpty = true)
        {
            return console.PromptInputsInternal(
                null,
                null,
                allowEmpty);
        }

        public static string[] PromptInputs(this IConsole console, string promptMessage, bool allowEmpty = true)
        {
            return console.PromptInputsInternal(
                promptMessage,
                null,
                allowEmpty);
        }

        public static string[] PromptInputs(this IConsole console, string promptMessage, string[] allowedValues, bool allowEmpty = true)
        {
            return console.PromptInputsInternal(
                promptMessage,
                allowedValues,
                allowEmpty);
        }

        internal static string[] PromptInputsInternal(this IConsole console, string promptMessage, string[] allowedValues, bool allowEmpty = true)
        {
            return console.PromptValuesInternal(
                a => a.ReadDelimitedLine(),
                promptMessage,
                allowedValues,
                allowEmpty);
        }
    }
}