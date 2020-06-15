namespace EmuConsole
{
    public static class PromptIntsExtensions
    {
        public static int[] PromptInts(this IConsole console, bool allowEmpty = true)
        {
            return console.PromptIntsInternal(
                null,
                null,
                allowEmpty);
        }

        public static int[] PromptInts(this IConsole console, string promptMessage, bool allowEmpty = true)
        {
            return console.PromptIntsInternal(
                promptMessage,
                null,
                allowEmpty);
        }

        public static int[] PromptInts(this IConsole console, string promptMessage, int[] allowedValues, bool allowEmpty = true)
        {
            return console.PromptIntsInternal(
                promptMessage,
                allowedValues,
                allowEmpty);
        }

        internal static int[] PromptIntsInternal(this IConsole console, string promptMessage, int[] allowedValues, bool allowEmpty = true)
        {
            return console.PromptValuesInternal(
                a => a.ReadDelimitedInt(),
                promptMessage,
                allowedValues,
                allowEmpty);
        }
    }
}