﻿using EmuConsole.ExampleApp.Processes;
using EmuConsole.ExampleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmuConsole.ExampleApp
{
    public class ExampleConsoleApp : ConsoleApp
    {
        private readonly string[] _words;
        private readonly IGuidGenerator _guidGenerator;
        private int[] _numbers;

        public ExampleConsoleApp(IConsole console, ConsoleOptions options, IGuidGenerator guidGenerator)
            : base(console, options)
        {
            _words = new[]
            {
                "Apple",
                "Door",
                "Table"
            };

            _numbers = new int[0];
            _guidGenerator = guidGenerator;
        }

        protected override void DisplayHeading()
        {
            var headingOptions = new ConsoleWriteOptions
            {
                Foreground = ConsoleColor.Black,
                Background = ConsoleColor.Cyan
            };

            _console.WriteLine("Welcome to the Example Console App!", headingOptions);
            _console.WriteLine("Enter one of the available commands to continue");
            _console.WriteLine();
        }

        protected override IEnumerable<ConsoleCommand> GetCommands()
        {
            yield return new ConsoleCommand(new[] { "w", "words" }, "Display words and then choose one", OnChooseWord);
            yield return new ConsoleCommand(new[] { "p", "populate" }, "Populate the numbers (only if they arent populated)", OnPopulateNumbers, CanPopulateNumbers);
            yield return new ConsoleCommand(new[] { "n", "numbers" }, "Display numbers and then choose one", OnChooseNumber, CanChooseNumber);
            yield return new ConsoleCommand(new[] { "c", "command" }, "Run a different console process", new ExampleProcess(_console, _options, _guidGenerator));
            yield return new ConsoleCommand("m", "Enter multiple values in a single action", OnEnterMultiple);
            yield return new ConsoleCommand("i", "Run the prompt process", new PromptProcess(_console, _options));
            yield return new ConsoleCommand("g", "Ping Google using an async command", OnPingGoogleAsync);
            yield return new ConsoleCommand("ex", "Throw an unhandled exception", OnThrowException);
        }

        private void OnEnterMultiple()
        {
            _console.WriteLine("Enter multiple values (comma delimited)");
            var inputs = _console.ReadDelimitedLine(',');

            _console.WriteLine("Entered the following values:");
            foreach (var input in inputs)
                _console.WriteLine($"* {input}");
        }

        private bool CanChooseNumber() => _numbers.Any();

        private void OnChooseNumber()
        {
            var number = _console.PromptIndexSelection(_numbers);
            _console.WriteLine($"Selected: {number}");
        }

        private void OnChooseWord()
        {
            var word = _console.PromptIndexSelection(_words, x => x.ToUpper());
            _console.WriteLine($"Selected: {word}");
        }

        private bool CanPopulateNumbers() => !_numbers.Any();

        private void OnPopulateNumbers() => _numbers = Enumerable.Range(1, 5).ToArray();

        private async Task OnPingGoogleAsync()
        {
            _console.WriteLine("Pinging the Google homepage");

            using var client = new HttpClient();
            var response = await client.GetAsync("https://www.google.com");

            _console.WriteLine($"Google returned: {response.StatusCode}");
        }

        private void OnThrowException()
        {
            throw new Exception("Something has gone terribly wrong");
        }
    }
}