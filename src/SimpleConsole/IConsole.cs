using System;

namespace SimpleConsole
{
    public interface IConsole
    {
        ConsoleOptions Options { get; }

        void Initialise(ConsoleOptions options);

        T Write<T>(T value, ConsoleColor? color);

        T WriteLine<T>(T value, ConsoleColor? color);

        string ReadLine();
    }
}