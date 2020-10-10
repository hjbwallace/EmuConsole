using Xunit;

namespace EmuConsole.Tests
{
    public class ReadIntRangeTests : ConsoleTestBase
    {
        [Theory]
        [InlineData("1/3", 1, 2, 3)]
        [InlineData("1/3/5", 1, 2, 3)]
        [InlineData("1 / 3", 1, 2, 3)]
        [InlineData("1 // 3", 1, 2, 3)]
        [InlineData("1/", 1)]
        [InlineData("1/1", 1)]
        [InlineData("1/,2", 1, 2)]
        [InlineData("1/2,3/4", 1, 2, 3, 4)]
        [InlineData("3/1", 1, 2, 3)]
        [InlineData("1/3,4", 1, 2, 3, 4)]
        public void CanParseDelimitedIntRange(string input, params int[] expectedValues)
        {
            _console.AddLinesToRead(input);
            var values = _console.ReadDelimitedInt();
            Assert.Equal(expectedValues, values);
        }
    }
}