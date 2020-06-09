namespace EmuConsole
{
    public static class PromptIntsExtensions
    {
        public static int[] PromptInts(this IConsole console, bool allowEmpty = true)
        {
            return console.PromptInputsInternal(
                a => a.ReadDelimitedInt(),
                null,
                null,
                allowEmpty);
        }

        public static int[] PromptInts(this IConsole console, string promptMessage, bool allowEmpty = true)
        {
            return console.PromptInputsInternal(
                a => a.ReadDelimitedInt(),
                promptMessage,
                null,
                allowEmpty);
        }

        public static int[] PromptInts(this IConsole console, string promptMessage, int[] allowedValues, bool allowEmpty = true)
        {
            return console.PromptInputsInternal(
                a => a.ReadDelimitedInt(),
                promptMessage,
                allowedValues,
                allowEmpty);
        }
    }
}