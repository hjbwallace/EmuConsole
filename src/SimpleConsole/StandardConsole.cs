using System;

namespace SimpleConsole
{
    public class StandardConsole : IConsole
    {
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