using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class StandardConsole : IConsole
    {
        private readonly LinkedList<string> _defaultInputs;

        private ConsoleWriteOptions _promptOptions = new ConsoleWriteOptions();

        public StandardConsole(params string[] args)
        {
            _defaultInputs = new LinkedList<string>(args ?? new string[0]);
        }

        public ConsoleOptions Options { get; private set; }

        public void Initialise(ConsoleOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));

            if (!string.IsNullOrWhiteSpace(options.Title))
                Console.Title = options.Title;

            _promptOptions = new ConsoleWriteOptions { Foreground = options.PromptColor };
        }

        public string ReadLine()
        {
            if (_defaultInputs.Any())
            {
                var defaultInput = _defaultInputs.First();
                _defaultInputs.RemoveFirst();
                return this.WriteLine(defaultInput, Options.PromptColor);
            }

            var input = string.Empty;
            var promptOptions = new ConsoleWriteOptions { Foreground = Options.PromptColor };

            WriteAction(_promptOptions, () => input = Console.ReadLine());
            return input;
        }

        public T Write<T>(T value, ConsoleWriteOptions writeOptions)
        {
            WriteAction(writeOptions, () => Console.Write(value));
            return value;
        }

        public T WriteLine<T>(T value, ConsoleWriteOptions writeOptions)
        {
            WriteAction(writeOptions, () => Console.WriteLine(value));
            return value;
        }

        private void WriteAction(ConsoleWriteOptions writeOptions, Action action)
        {
            if (writeOptions != null)
            {
                if (writeOptions.Foreground != null)
                    Console.ForegroundColor = writeOptions.Foreground.Value;

                if (writeOptions.Background != null)
                    Console.BackgroundColor = writeOptions.Background.Value;

                action();
                Console.ResetColor();
            }
            else
            {
                action();
            }
        }
    }
}