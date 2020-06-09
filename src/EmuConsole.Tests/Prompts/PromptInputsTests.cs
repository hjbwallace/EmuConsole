using Xunit;

namespace EmuConsole.Tests.Prompts
{
    public class PromptInputsTests : ConsoleTestBase
    {
        [Fact]
        public void PromptInputs()
        {
            _console.AddLinesToRead("test1, test2");
            var outputs = _console.PromptInputs();

            Assert.Contains("test1", outputs);
            Assert.Contains("test2", outputs);
            Assert.Equal(2, outputs.Length);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> test1, test2
");
        }

        [Fact]
        public void PromptInputsWithEmpty()
        {
            _console.AddLinesToRead("", "test1, test2");
            var outputs = _console.PromptInputs(true);

            Assert.Empty(outputs);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {string.Empty}
");
        }

        [Fact]
        public void PromptInputsWithoutEmpty()
        {
            _console.AddLinesToRead("", "test1, test2");
            var outputs = _console.PromptInputs(false);

            Assert.Contains("test1", outputs);
            Assert.Contains("test2", outputs);
            Assert.Equal(2, outputs.Length);

            _console.HasLinesRead(2);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> {string.Empty}
> test1, test2
");
        }

        [Fact]
        public void PromptInputsWithMessage()
        {
            _console.AddLinesToRead("input");
            var outputs = _console.PromptInputs("This is the prompt");

            Assert.Single(outputs, "input");

            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"This is the prompt
> input
");
        }

        [Fact]
        public void PromptInputsWithRestrictions()
        {
            _console.AddLinesToRead("test1, test2, test3");
            var outputs = _console.PromptInputs(null, new[] { "test1", "test2" });

            Assert.Contains("test1", outputs);
            Assert.Contains("test2", outputs);
            Assert.DoesNotContain("test3", outputs);
            Assert.Equal(2, outputs.Length);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> test1, test2, test3
");
        }

        [Fact]
        public void PromptInputsWithRestrictionsWithEmpty()
        {
            _console.AddLinesToRead("test4, test5, test3");
            var outputs = _console.PromptInputs(null, new[] { "test1", "test2" }, true);

            Assert.Empty(outputs);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> test4, test5, test3
");
        }

        [Fact]
        public void PromptInputsWithRestrictionsWithoutEmpty()
        {
            _console.AddLinesToRead("test4, test5, test3", "test1, test3");
            var outputs = _console.PromptInputs(null, new[] { "test1", "test2" }, false);

            Assert.Single(outputs, "test1");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(0);
            _console.HasOutput($@"> test4, test5, test3
> test1, test3
");
        }
    }
}