using System;

namespace EmuConsole
{
    public class StandardConsole : IConsole
    {
        public ConsoleOptions Options { get; private set; }

        public void Initialise(ConsoleOptions options)
        {
            Options = options;

            if (!string.IsNullOrWhiteSpace(options.Title))
                Console.Title = options.Title;
        }

        public string ReadLine()
        {
            string input = string.Empty;
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