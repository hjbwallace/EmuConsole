namespace EmuConsole
{
    public static class PromptDelimitedInputExtensions
    {
        public static string[] PromptInputs(this IConsole console, bool allowEmpty = true)
        {
            return console.PromptInputsInternal(
                a => a.ReadDelimitedLine(),
                null,
                null,
                allowEmpty);
        }

        public static string[] PromptInputs(this IConsole console, string promptMessage, bool allowEmpty = true)
        {
            return console.PromptInputsInternal(
                a => a.ReadDelimitedLine(),
                promptMessage,
                null,
                allowEmpty);
        }

        public static string[] PromptInputs(this IConsole console, string promptMessage, string[] allowedValues, bool allowEmpty = true)
        {
            return console.PromptInputsInternal(
                a => a.ReadDelimitedLine(),
                promptMessage,
                allowedValues,
                allowEmpty);
        }
    }
}