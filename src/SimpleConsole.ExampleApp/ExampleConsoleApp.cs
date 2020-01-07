using System.Collections.Generic;
using System.Linq;

namespace SimpleConsole.ExampleApp
{
    public class ExampleConsoleApp : ConsoleApp
    {
        private readonly string[] _words;
        private int[] _numbers;

        public ExampleConsoleApp(IConsole console = null, ConsoleOptions options = null)
            : base(console, options)
        {
            _words = new[]
            {
                "Apple",
                "Door",
                "Table"
            };

            _numbers = new int[0];
        }

        protected override void DisplayHeading()
        {
            _console.WriteLine("Welcome to the Example Console App!");
            _console.WriteLine();
        }

        protected override IEnumerable<ConsoleCommand> GetCommands()
        {
            return new[]
            {
                new ConsoleCommand(new[] { "w", "words" }, "Display words and then choose one", OnChooseWord),
                new ConsoleCommand(new[] { "p", "populate" }, "Populate the numbers (only if they arent populated)", OnPopulateNumbers, CanPopulateNumbers),
                new ConsoleCommand(new[] { "n", "numbers" }, "Display numbers and then choose one", OnChooseNumber, CanChooseNumber)
            };
        }

        private bool CanChooseNumber() => _numbers.Any();

        private void OnChooseNumber()
        {
            var number = _console.PromptIndexSelection(_numbers);
            _console.WriteLine($"Selected: {number}");
        }

        private void OnChooseWord()
        {
            var word = _console.PromptIndexSelection(_words);
            _console.WriteLine($"Selected: {word}");
        }

        private bool CanPopulateNumbers() => !_numbers.Any();

        private void OnPopulateNumbers() => _numbers = Enumerable.Range(1, 5).ToArray();
    }
}