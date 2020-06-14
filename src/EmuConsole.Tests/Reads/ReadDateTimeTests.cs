using System;
using Xunit;

namespace EmuConsole.Tests
{
    public class ReadDateTimeTests : ConsoleTestBase
    {
        [Theory]
        [InlineData("2000/12/31", 2000, 12, 31)]
        [InlineData("2005-11-01", 2005, 11, 01)]
        [InlineData("1998-DEC-11", 1998, 12, 11)]
        [InlineData("30-DEC-1999", 1999, 12, 30)]
        [InlineData("30DEC1999", 1999, 12, 30)]
        public void CanParseValidDateTime(string input, int expectedYear, int expectedMonth, int expectedDay)
        {
            _console.AddLinesToRead(input);
            var expected = new DateTime(expectedYear, expectedMonth, expectedDay);
            var value = _console.ReadDateTime();
            Assert.Equal(expected, value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("20100101")]
        [InlineData("2005-13-01")]
        public void ReturnsNullForInvalidDateTime(string input)
        {
            _console.AddLinesToRead(input);
            var value = _console.ReadDateTime();
            Assert.Null(value);
        }
    }
}