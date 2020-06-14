using Xunit;

namespace EmuConsole.Tests
{
    public class ReadDoubleTests : ConsoleTestBase
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

        [Theory]
        [InlineData("1.2,2.1,3.3", 1.2, 2.1, 3.3)]
        [InlineData("1.1    ,,,2", 1.1, 2.0)]
        [InlineData("-5.12,-5.1,00", -5.12, -5.1, 0.0)]
        [InlineData("   1    ,2..1", 1.0)]
        [InlineData("+2.2,-2.2", 2.2, -2.2)]
        [InlineData("A")]
        public void CanParseDelimitedDouble(string input, params double[] expectedValues)
        {
            _console.AddLinesToRead(input);
            var values = _console.ReadDelimitedDouble();
            Assert.Equal(expectedValues, values);
        }

        [Theory]
        [InlineData("1.1, 2, asd", 2)]
        [InlineData("1", 1)]
        [InlineData("1,, 3.2", 2)]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        public void ParsesTheCorrectNumberOfDoubles(string input, int expectedCount)
        {
            _console.AddLinesToRead(input);
            var values = _console.ReadDelimitedDouble();
            Assert.Equal(expectedCount, values.Length);
        }
    }
}