using Xunit;

namespace EmuConsole.Tests
{
    public class ConsoleDelimitedLineParsingTests : ConsoleTestBase
    {
        [Fact]
        public void ParsesValuesFromInput()
        {
            var input = "value 1, value2, , value3, ,";
            _console.AddLinesToRead(input);

            var expectedValues = new[]
            {
                "value 1",
                "value2",
                "value3"
            };

            var values = _console.ReadDelimitedLine();
            Assert.Equal(expectedValues, values);
        }

        [Theory]
        [InlineData("1, 2, 3", 3)]
        [InlineData("1", 1)]
        [InlineData("1,, 3", 2)]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        public void ParsesTheCorrectNumberOfValues(string input, int expectedCount)
        {
            _console.AddLinesToRead(input);
            var values = _console.ReadDelimitedLine();
            Assert.Equal(expectedCount, values.Length);
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