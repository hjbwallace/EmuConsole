namespace SimpleConsole
{
    public interface IConsole
    {
        T Write<T>(T value);

        T WriteLine<T>(T value);

        string ReadLine();
    }
}