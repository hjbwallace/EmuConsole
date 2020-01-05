using Xunit;

namespace SimpleConsole.Tests
{
    public class ConsoleDoubleParsingTests : ConsoleTestBase
    {
        [Theory]
        [InlineData("1", 1)]
        [InlineData("100", 100)]
        [InlineData("-5.5", -5.5)]
        [InlineData("   1.1    ", 1.1)]
        [InlineData("+2.2000", 2.2)]
        public void CanParseValidDouble(string input, double expectedValue)
        {
            _console.AddLinesToRead(input);
            var value = _console.ReadDouble();
            Assert.Equal(expectedValue, value);
        }

        [Theory]
        [InlineData("1.1.1")]
        [InlineData("100a")]
        [InlineData("- 5")]
        [InlineData("INT")]
        [InlineData("")]
        [InlineData(null)]
        public void ReturnsNullForInvalidDouble(string input)
        {
            _console.AddLinesToRead(input);
            var value = _console.ReadDouble();
            Assert.Null(value);
        }
    }
}