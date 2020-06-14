using Xunit;

namespace EmuConsole.Tests
{
    public class ReadIntTests : ConsoleTestBase
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

        [Theory]
        [InlineData("1,2,3", 1, 2, 3)]
        [InlineData("1,,,2", 1, 2)]
        [InlineData("-5,-5,00", -5, -5, 0)]
        [InlineData("   1    ,2", 1, 2)]
        [InlineData("+2,-2", 2, -2)]
        [InlineData("A")]
        public void CanParseDelimitedInt(string input, params int[] expectedValues)
        {
            _console.AddLinesToRead(input);
            var values = _console.ReadDelimitedInt();
            Assert.Equal(expectedValues, values);
        }

        [Theory]
        [InlineData("1, 2.1, asd", 1)]
        [InlineData("1", 1)]
        [InlineData("1,, 3", 2)]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        public void ParsesTheCorrectNumberOfInts(string input, int expectedCount)
        {
            _console.AddLinesToRead(input);
            var values = _console.ReadDelimitedInt();
            Assert.Equal(expectedCount, values.Length);
        }
    }
}