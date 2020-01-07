using System;

namespace SimpleConsole
{
    public class StandardConsole : IConsole
    {
        public void Initialise(ConsoleOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.Title))
                Console.Title = options.Title;
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public T Write<T>(T value)
        {
            Console.Write(value);
            return value;
        }

        public T WriteLine<T>(T value)
        {
            Console.WriteLine(value);
            return value;
        }
    }
}