namespace EmuConsole
{
    public static class PromptConfirmExtensions
    {
        public static bool PromptConfirm(this IConsole console, string prompt)
        {
            var input = console.PromptInput($"{prompt ?? string.Empty} (Y to confirm)").ToUpper();
            return input == "Y";
        }
    }
}