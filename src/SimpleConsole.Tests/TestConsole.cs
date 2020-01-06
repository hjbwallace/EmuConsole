using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimpleConsole.Tests
{
    public class TestConsole : IConsole
    {
        private readonly LinkedList<string> _linesToRead = new LinkedList<string>();
        private int _linesRead;
        private int _linesWritten;

        public string ReadLine()
        {
            if (!_linesToRead.Any())
                throw new InvalidOperationException("No available lines to read");

            var line = _linesToRead.First();
            _linesToRead.RemoveFirst();

            _linesRead++;

            return line;
        }

        public T Write<T>(T value)
        {
            return value;
        }

        public T WriteLine<T>(T value)
        {
            _linesWritten++;
            return value;
        }

        public void AddLinesToRead(params object[] linesToRead)
        {
            foreach (var lineToRead in linesToRead)
                _linesToRead.AddLast(lineToRead?.ToString());
        }

        public void HasLinesRead(int expected)
        {
            Assert.Equal(expected, _linesRead);
        }

        public void HasLinesWritten(int expected)
        {
            Assert.Equal(expected, _linesWritten);
        }
    }
}