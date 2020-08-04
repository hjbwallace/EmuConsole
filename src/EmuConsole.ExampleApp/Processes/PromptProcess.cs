using System;
using System.Collections.Generic;

namespace EmuConsole.ExampleApp.Processes
{
    public class PromptProcess : ConsoleProcess
    {
        public PromptProcess(IConsole console, ConsoleOptions options = null)
            : base(console, options)
        {
        }

        protected override void DisplayHeading()
        {
            _console.WriteLine("This will demonstrate various prompt functionalities");
            _console.WriteLine("These allow the user to be prompted for input");
            _console.WriteLine();
        }

        protected override IEnumerable<ConsoleCommand> GetCommands()
        {
            yield return new ConsoleCommand("1", "PromptInput", OnPromptInput);
            yield return new ConsoleCommand("2", "PromptInput with restrictions and default", OnPromptInputRestricted);
            yield return new ConsoleCommand("3", "PromptInputs (with ',' as the delimiter)", OnPromptInputs);

            yield return new ConsoleCommand("4", "PromptInt", OnPromptInt);
            yield return new ConsoleCommand("5", "PromptIntOptional - returns a nullable int", OnPromptIntOptional);
            yield return new ConsoleCommand("6", "PromptInt with restrictions and default", OnPromptIntRestricted);
            yield return new ConsoleCommand("7", "PromptInts (with ',' as the delimiter)", OnPromptInts);
        }

        private void OnPromptInt()
        {
            RunPrompt(a => a.PromptInt($"Enter an integer value"));
        }

        private void OnPromptIntOptional()
        {
            RunPrompt(a => a.PromptIntOptional($"Enter an integer value. Anything else will be returned as null"));
        }

        private void OnPromptIntRestricted()
        {
            RunPrompt(a => a.PromptInt($"Enter some value", new[] { 1, 2, 3 }, 1));
        }

        private void OnPromptInts()
        {
            RunPrompt(a => a.PromptInts($"Enter some numbers delimited with a comma"));
        }

        private void OnPromptInput()
        {
            RunPrompt(a => a.PromptInput($"Enter some value"));
        }

        private void OnPromptInputRestricted()
        {
            RunPrompt(a => a.PromptInput($"Enter some value", new[] { "Test1", "Test2", "Test3" }, "Test1"));
        }

        private void OnPromptInputs()
        {
            RunPrompt(a => a.PromptInputs($"Enter some values delimited with a comma"));
        }

        private void RunPrompt(Func<IConsole, object> inputSelector)
        {
            var input = inputSelector(_console);
            _console.WriteLine("Input was: " + (input ?? "null"));
        }

        private void RunPrompt(Func<IConsole, object[]> inputSelector)
        {
            var inputs = inputSelector(_console);
            _console.WriteLine("Inputs were: " + (string.Join("|", inputs) ?? "null"));
        }
    }
}