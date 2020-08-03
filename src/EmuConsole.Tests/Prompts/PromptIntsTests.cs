using Xunit;

namespace EmuConsole.Tests.Prompts
{
    public class PromptIntsTests : ConsoleTestBase
    {
        [Fact]
        public void PromptInts()
        {
            _console.AddLinesToRead("1, 2");
            var outputs = _console.PromptInts();

            Assert.Contains(1, outputs);
            Assert.Contains(2, outputs);
            Assert.Equal(2, outputs.Length);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> 1, 2
");
        }

        [Fact]
        public void PromptIntsWithEmpty()
        {
            _console.AddLinesToRead("", "1, 2");
            var outputs = _console.PromptInts(true);

            Assert.Empty(outputs);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {string.Empty}
");
        }

        [Fact]
        public void PromptIntsWithoutEmpty()
        {
            _console.AddLinesToRead("", "1, 2");
            var outputs = _console.PromptInts(false);

            Assert.Contains(1, outputs);
            Assert.Contains(2, outputs);
            Assert.Equal(2, outputs.Length);

            _console.HasLinesRead(2);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {string.Empty}
> 1, 2
");
        }

        [Fact]
        public void PromptIntsWithoutEmptyAndTemplate()
        {
            _console.AddLinesToRead("", "1, 2");
            _console.Options.InvalidPromptsTemplate = "INVALID";

            var outputs = _console.PromptInts(false);

            Assert.Contains(1, outputs);
            Assert.Contains(2, outputs);
            Assert.Equal(2, outputs.Length);

            _console.HasLinesRead(2);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {string.Empty}
[INVALID] > 1, 2
");
        }

        [Fact]
        public void PromptIntsWithMessage()
        {
            _console.AddLinesToRead(1);
            var outputs = _console.PromptInts("This is the prompt");

            Assert.Single(outputs, 1);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> 1
");
        }

        [Fact]
        public void PromptIntsWithRestrictions()
        {
            _console.AddLinesToRead("1, 2, 3");
            var outputs = _console.PromptInts(null, new[] { 1, 2 });

            Assert.Contains(1, outputs);
            Assert.Contains(2, outputs);
            Assert.DoesNotContain(3, outputs);
            Assert.Equal(2, outputs.Length);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> 1, 2, 3
");
        }

        [Fact]
        public void PromptIntsWithRestrictionsWithEmpty()
        {
            _console.AddLinesToRead("4, 5, 3");
            var outputs = _console.PromptInts(null, new[] { 1, 2 }, true);

            Assert.Empty(outputs);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> 4, 5, 3
");
        }

        [Fact]
        public void PromptIntsWithRestrictionsWithoutEmpty()
        {
            _console.AddLinesToRead("4, 5, 3", "1, 3");
            var outputs = _console.PromptInts(null, new[] { 1, 2 }, false);

            Assert.Single(outputs, 1);

            _console.HasLinesRead(2);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> 4, 5, 3
> 1, 3
");
        }
    }
}