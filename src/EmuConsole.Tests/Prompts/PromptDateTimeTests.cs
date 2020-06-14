using System;
using Xunit;

namespace EmuConsole.Tests.Prompts
{
    public class PromptDateTimeTests : ConsoleTestBase
    {
        private static readonly DateTime TestDateTime = new DateTime(2000, 12, 31);

        [Theory]
        [InlineData("2000/12/31")]
        [InlineData("2000-12-31")]
        [InlineData("2000-DEC-31")]
        [InlineData("31-DEC-00")]
        public void PromptDateTime(string input)
        {
            _console.AddLinesToRead(input);
            var output = _console.PromptDateTime();

            Assert.Equal(TestDateTime, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {input}
");
        }

        [Theory]
        [InlineData("2000/12/31")]
        [InlineData("2000-12-31")]
        [InlineData("2000-DEC-31")]
        [InlineData("31-DEC-00")]
        public void PromptDateTimeRetries(string input)
        {
            _console.AddLinesToRead("1a", "b", input);
            var output = _console.PromptDateTime();

            Assert.Equal(TestDateTime, output);
            _console.HasLinesRead(3);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> 1a
> b
> {input}
");
        }

        [Theory]
        [InlineData("2000/12/31")]
        [InlineData("2000-12-31")]
        [InlineData("2000-DEC-31")]
        [InlineData("31-DEC-00")]
        public void PromptDateTimeWithMessage(string input)
        {
            _console.AddLinesToRead(input);
            var output = _console.PromptDateTime("Prompt message");

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
        public void PromptDateTimeWithDefault(string input)
        {
            _console.AddLinesToRead(input);
            var output = _console.PromptDateTime(null, TestDateTime);

            Assert.Equal(TestDateTime, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {input}
");
        }

        [Fact]
        public void PromptDateTimeWithRestrictions()
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
            var output = _console.PromptDateTime("Prompt message", allowed);

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
        public void PromptDateTimeWithRestrictionsWithoutMessage()
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
            var output = _console.PromptDateTime(null, allowed);

            Assert.Equal(allowed[0], output);
            _console.HasLinesRead(3);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {inputs[0]}
> {inputs[1]}
> {inputs[2]}
");
        }

        [Fact]
        public void PromptDateTimeWithRestrictionsAndDefault()
        {
            var allowed = new[]
            {
                new DateTime(2000, 01, 01),
                new DateTime(2000, 01, 02),
                new DateTime(2000, 01, 03)
            };

            _console.AddLinesToRead("2000-01-04");
            var output = _console.PromptDateTime("This is the prompt", allowed, TestDateTime);

            Assert.Equal(TestDateTime, output);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> 2000-01-04
");
        }
    }
}