using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleConsole.Tests
{
    public class TestConsole : IConsole
    {
        private readonly LinkedList<string> _linesToRead = new LinkedList<string>();

        public string ReadLine()
        {
            if (!_linesToRead.Any())
                throw new InvalidOperationException("No available lines to read");

            var line = _linesToRead.First();
            _linesToRead.RemoveFirst();

            return line;
        }

        public string Write(string value)
        {
            return value;
        }

        public string WriteLine(string value)
        {
            return value;
        }

        public void AddLinesToRead(params string[] linesToRead)
        {
            foreach (var lineToRead in linesToRead)
                _linesToRead.AddLast(lineToRead);
        }
    }
}