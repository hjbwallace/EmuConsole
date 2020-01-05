namespace SimpleConsole
{
    public interface IConsole
    {
        string Write(string value);

        string WriteLine(string value);

        string ReadLine();
    }
}