namespace SimpleConsole
{
    public interface IConsole
    {
        void Initialise(ConsoleOptions options);

        T Write<T>(T value);

        T WriteLine<T>(T value);

        string ReadLine();
    }
}