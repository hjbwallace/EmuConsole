using Xunit;

namespace EmuConsole.Tests.Prompts
{
    public class PromptIntOptionalTests : ConsoleTestBase
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void PromptIntOptional(int input)
        {
            _console.AddLinesToRead(input);
            var output = _console.PromptIntOptional();

            Assert.Equal(input, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {input}
");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void PromptIntOptionalDoesntRetry(int input)
        {
            _console.AddLinesToRead("1a", "b", input);
            var output = _console.PromptIntOptional();

            Assert.Null(output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> 1a
");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void PromptIntOptionalWithMessage(int input)
        {
            _console.AddLinesToRead(input);
            var output = _console.PromptIntOptional("Prompt message");

            Assert.Equal(input, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"Prompt message
> {input}
");
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        [InlineData("A", 0)]
        public void PromptIntWithDefault(object input, int expected)
        {
            _console.AddLinesToRead(input);
            var output = _console.PromptIntOptional(null, 0);

            Assert.Equal(expected, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {input}
");
        }

        [Fact]
        public void PromptIntOptionalWithRestrictions()
        {
            _console.AddLinesToRead(10, 20, 30);
            var output = _console.PromptIntOptional("Prompt message", new[] { 3, 30, 300 });

            Assert.Equal(30, output);
            _console.HasLinesRead(3);
            _console.HasLinesWritten(1);
            _console.HasOutput(@"Prompt message
> 10
> 20
> 30
");
        }

        [Fact]
        public void PromptIntOptionalWithRestrictionsWithoutMessage()
        {
            _console.AddLinesToRead(10, 20, 30);
            var output = _console.PromptIntOptional(null, new[] { 3, 30, 300 });

            Assert.Equal(30, output);
            _console.HasLinesRead(3);
            _console.HasLinesWritten(0);
            _console.HasOutput(@"> 10
> 20
> 30
");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void PromptIntOptionalWithRestrictionsAndDefault(int defaultValue)
        {
            _console.AddLinesToRead(1000);
            var output = _console.PromptIntOptional("This is the prompt", new[] { 1, 10, 100 }, defaultValue);

            Assert.Equal(defaultValue, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> 1000
");
        }
    }
}