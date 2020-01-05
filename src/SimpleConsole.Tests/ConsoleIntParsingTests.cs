using Xunit;

namespace SimpleConsole.Tests
{
    public class ConsoleIntParsingTests : ConsoleTestBase
    {
        [Theory]
        [InlineData("1", 1)]
        [InlineData("100", 100)]
        [InlineData("-5", -5)]
        [InlineData("   1    ", 1)]
        [InlineData("+2", 2)]
        public void CanParseValidInt(string input, int expectedValue)
        {
            _console.AddLinesToRead(input);
            var value = _console.ReadInt();
            Assert.Equal(expectedValue, value);
        }

        [Theory]
        [InlineData("1.1")]
        [InlineData("100a")]
        [InlineData("- 5")]
        [InlineData("INT")]
        [InlineData("")]
        [InlineData(null)]
        public void ReturnsNullForInvalidInt(string input)
        {
            _console.AddLinesToRead(input);
            var value = _console.ReadInt();
            Assert.Null(value);
        }
    }
}