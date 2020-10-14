using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole.ExampleApp.Processes
{
    public class PromptCollectionProcess : ConsoleProcess
    {
        private readonly IDictionary<string, string> _source;

        public PromptCollectionProcess(IConsole console, ConsoleOptions options = null)
            : base(console, options)
        {
            var today = DateTime.Now.Date;
            _source = Enumerable.Range(0, 22).ToDictionary(
                e => today.AddDays(e).ToString("MMdd"),
                e => "Date: " + today.AddDays(e).ToShortDateString());
        }

        protected override void DisplayHeading()
        {
            _console.WriteLine("This will demonstrate various prompt functionalities for collections");
            _console.WriteLine("These allow the user to be prompted for input from a selection of options");
            _console.WriteLine();
        }

        protected override IEnumerable<ConsoleCommand> GetCommands()
        {
            yield return new ConsoleCommand(new[] { "r", "rows" }, "PromptInputSelection: Rows", () => OnPromptInputSelection(CollectionWriteStyle.Rows));
            yield return new ConsoleCommand(new[] { "i", "inline" }, "PromptInputSelection: Inline", () => OnPromptInputSelection(CollectionWriteStyle.Inline));
            yield return new ConsoleCommand(new[] { "c", "columns" }, "PromptInputSelection: Columns", () => OnPromptInputSelection(CollectionWriteStyle.Columns));

            yield return new ConsoleCommand(new[] { "e", "enum" }, "PromptIndexSelection: Enum", OnPromptEnumSelection);
            yield return new ConsoleCommand(new[] { "x", "index" }, "PromptIndexSelection: Values", OnPromptIndexSelection);
        }

        private void OnPromptInputSelection(CollectionWriteStyle style)
        {
            DisplaySelection(() => _console.PromptInputSelection(_source, style: style, isOptional: true));
        }

        private void OnPromptEnumSelection()
        {
            DisplaySelection(() => _console.PromptIndexSelection<ExampleEnum>());
        }

        private void OnPromptIndexSelection()
        {
            DisplaySelection(() => _console.PromptIndexSelection(_source.Values));
        }

        private void DisplaySelection<T>(Func<T> getSelection)
        {
            var selection = getSelection();
            _console.WriteLine("Selection was: " + selection);
        }
    }
}