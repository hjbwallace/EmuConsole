using System;
using Xunit;

namespace EmuConsole.Tests.Prompts
{
    public class PromptDateTimeOptionalTests : ConsoleTestBase
    {
        private static readonly DateTime TestDateTime = new DateTime(2000, 12, 31);

        [Theory]
        [InlineData("2000/12/31")]
        [InlineData("2000-12-31")]
        [InlineData("2000-DEC-31")]
        [InlineData("31-DEC-00")]
        public void PromptDateTimeOptional(string input)
        {
            _console.AddLinesToRead(input);
            var output = _console.PromptDateTimeOptional();

            Assert.Equal(TestDateTime, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {input}
");
        }

        [Fact]
        public void PromptDateTimeOptionalDoesntRetry()
        {
            _console.AddLinesToRead("1a", "2000-12-31");
            var output = _console.PromptDateTimeOptional();

            Assert.Null(output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> 1a
");
        }

        [Theory]
        [InlineData("2000/12/31")]
        [InlineData("2000-12-31")]
        [InlineData("2000-DEC-31")]
        [InlineData("31-DEC-00")]
        public void PromptDateTimeOptionalWithMessage(string input)
        {
            _console.AddLinesToRead(input);
            var output = _console.PromptDateTimeOptional("Prompt message");

            Assert.Equal(TestDateTime, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"Prompt message
> {input}
");
        }

        [Theory]
        [InlineData("20001231")]
        [InlineData("")]
        [InlineData(null)]
        public void PromptDateTimeOptionalWithDefault(string input)
        {
            _console.AddLinesToRead(input);
            var output = _console.PromptDateTimeOptional(null, TestDateTime);

            Assert.Equal(TestDateTime, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {input}
");
        }

        [Fact]
        public void PromptDateTimeOptionalWithRestrictions()
        {
            var allowed = new[]
            {
                new DateTime(2000, 01, 01),
                new DateTime(2000, 01, 02),
                new DateTime(2000, 01, 03)
            };

            var inputs = new[]
            {
                "2000-01-04",
                "2000-01-05",
                "2000-01-01",
            };

            _console.AddLinesToRead(inputs);
            var output = _console.PromptDateTimeOptional("Prompt message", allowed);

            Assert.Equal(allowed[0], output);
            _console.HasLinesRead(3);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"Prompt message
> {inputs[0]}
> {inputs[1]}
> {inputs[2]}
");
        }

        [Fact]
        public void PromptDateTimeOptionalWithRestrictionsWithoutMessage()
        {
            var allowed = new[]
            {
                new DateTime(2000, 01, 01),
                new DateTime(2000, 01, 02),
                new DateTime(2000, 01, 03)
            };

            var inputs = new[]
            {
                "2000-01-04",
                "2000-01-05",
                "2000-01-01",
            };

            _console.AddLinesToRead(inputs);
            var output = _console.PromptDateTimeOptional(null, allowed);

            Assert.Equal(allowed[0], output);
            _console.HasLinesRead(3);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {inputs[0]}
> {inputs[1]}
> {inputs[2]}
");
        }

        [Fact]
        public void PromptDateTimeOptionalWithRestrictionsAndDefault()
        {
            var allowed = new[]
            {
                new DateTime(2000, 01, 01),
                new DateTime(2000, 01, 02),
                new DateTime(2000, 01, 03)
            };

            _console.AddLinesToRead("2000-01-04");
            var output = _console.PromptDateTimeOptional("This is the prompt", allowed, TestDateTime);

            Assert.Equal(TestDateTime, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> 2000-01-04
");
        }
    }
}