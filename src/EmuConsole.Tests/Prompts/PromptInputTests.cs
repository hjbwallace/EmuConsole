using Xunit;

namespace EmuConsole.Tests.Prompts
{
    public class PromptInputTests : ConsoleTestBase
    {
        [Fact]
        public void PromptInput()
        {
            _console.AddLinesToRead("input");
            var output = _console.PromptInput();

            Assert.Equal("input", output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> input
");
        }

        [Fact]
        public void PromptInputWithMessage()
        {
            _console.AddLinesToRead("input");
            var output = _console.PromptInput("This is the prompt");

            Assert.Equal("input", output);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> input
");
        }

        [Fact]
        public void PromptInputWithRestrictions()
        {
            _console.AddLinesToRead("invalid", "valid");
            var output = _console.PromptInput("This is the prompt", new[] { "valid" });

            Assert.Equal("valid", output);
            _console.HasLinesRead(2);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> invalid
> valid
");
        }

        [Fact]
        public void PromptInputWithRestrictionsWithoutMessage()
        {
            _console.AddLinesToRead("invalid", "valid");
            var output = _console.PromptInput(null, new[] { "valid" });

            Assert.Equal("valid", output);
            _console.HasLinesRead(2);
            _console.HasLinesWritten(0);
            _console.HasOutput(@"> invalid
> valid
");
        }

        [Theory]
        [InlineData("default")]
        [InlineData("")]
        [InlineData(null)]
        public void PromptInputWithRestrictionsAndDefault(string defaultValue)
        {
            _console.AddLinesToRead("invalid", "valid");
            var output = _console.PromptInput("This is the prompt", new[] { "valid" }, defaultValue);

            Assert.Equal(defaultValue, output);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> invalid
");
        }
    }
}