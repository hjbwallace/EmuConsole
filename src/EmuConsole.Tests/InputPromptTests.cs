using System;
using Xunit;

namespace EmuConsole.Tests
{
    public class InputPromptTests : ConsoleTestBase
    {
        [Fact]
        public void CanPromptTheUserForInput()
        {
            _console.AddLinesToRead("input");
            _console.PromptInput("This is the prompt");

            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> input
");
        }

        [Fact]
        public void CanPromptTheUserForInputWithRestrictions()
        {
            _console.AddLinesToRead("invalid", "valid");
            _console.PromptInput("This is the prompt", "valid");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> invalid
> valid
");
        }

        [Fact]
        public void CanPromptTheUserForInputInt()
        {
            _console.AddLinesToRead(100);
            _console.PromptInputInt("This is the prompt");

            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> 100
");
        }

        [Fact]
        public void CanPromptTheUserForInputIntWithRestrictions()
        {
            _console.AddLinesToRead(1, 2, 3);
            _console.PromptInputInt("This is the prompt", 3);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> 1
> 2
> 3
");
        }

        [Fact]
        public void CanPromptTheUserForInputOptionalInt()
        {
            _console.AddLinesToRead(100);
            var input = _console.PromptInputOptionalInt("This is the prompt", new int[] { 1, 2, 3 });

            Assert.Null(input);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> 100
");
        }

        [Fact]
        public void CanPromptTheUserForInputOptionalIntWithRestrictions()
        {
            _console.AddLinesToRead(1, 2, 3);
            var input = _console.PromptInputOptionalInt("This is the prompt", new[] { 3 });

            Assert.Null(input);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> 1
");
        }

        [Theory]
        [InlineData("y", true)]
        [InlineData("Y", true)]
        [InlineData("", false)]
        [InlineData("yy", false)]
        public void CanPromptTheUserToConfirmAnAction(string input, bool expectedResult)
        {
            _console.AddLinesToRead(input);
            var result = _console.PromptConfirm("A confirmation prompt");

            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"A confirmation prompt (Y to confirm){Environment.NewLine}> {input}{Environment.NewLine}");

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CanPromptTheUserForInputOptionalIntWithDefault()
        {
            _console.AddLinesToRead(100);

            var defaultValue = 1;
            var input = _console.PromptInputOptionalInt("This is the prompt", new int[] { 1, 2, 3 }, defaultValue);

            Assert.Equal(defaultValue, input);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> 100
");
        }
    }
}