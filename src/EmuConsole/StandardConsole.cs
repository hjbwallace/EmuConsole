using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class StandardConsole : IConsole
    {
        private readonly LinkedList<string> _defaultInputs;

        public StandardConsole(params string[] args)
        {
            _defaultInputs = new LinkedList<string>(args ?? new string[0]);
        }

        public ConsoleOptions Options { get; private set; }

        public void Initialise(ConsoleOptions options)
        {
            Options = options;

            if (!string.IsNullOrWhiteSpace(options.Title))
                Console.Title = options.Title;
        }

        public string ReadLine()
        {
            if (_defaultInputs.Any())
            {
                var defaultInput = _defaultInputs.First();
                _defaultInputs.RemoveFirst();
                return WriteLine(defaultInput, Options.PromptColor);
            }

            var input = string.Empty;
            ColourAction(Options.PromptColor, () => input = Console.ReadLine());
            return input;
        }

        public T Write<T>(T value, ConsoleColor? color)
        {
            ColourAction(color, () => Console.Write(value));
            return value;
        }

        public T WriteLine<T>(T value, ConsoleColor? color)
        {
            ColourAction(color, () => Console.WriteLine(value));
            return value;
        }

        private void ColourAction(ConsoleColor? color, Action action)
        {
            if (color != null)
            {
                Console.ForegroundColor = color.Value;
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