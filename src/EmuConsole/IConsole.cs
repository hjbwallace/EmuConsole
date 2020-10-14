using System.Drawing;

namespace EmuConsole
{
    public interface IConsole
    {
        ConsoleOptions Options { get; }

        Size Dimensions { get; }

        void Initialise(ConsoleOptions options);

        T Write<T>(T value, ConsoleWriteOptions writeOptions);

        T WriteLine<T>(T value, ConsoleWriteOptions writeOptions);

        string ReadLine();
    }
}