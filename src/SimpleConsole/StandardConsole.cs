using System;

namespace SimpleConsole
{
    public class StandardConsole : IConsole
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public string Write(string value)
        {
            Console.Write(value);
            return value;
        }

        public string WriteLine(string value)
        {
            Console.WriteLine(value);
            return value;
        }
    }
}