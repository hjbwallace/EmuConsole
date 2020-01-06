using Xunit;

namespace SimpleConsole.Tests
{
    public class InputPromptTests : ConsoleTestBase
    {
        [Fact]
        public void OnlyWritesThePromptOnTheFirstIteration()
        {
            _console.AddLinesToRead("invalid", "valid");
            _console.PromptInput("This is the prompt", "valid");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(1);
        }

        [Fact]
        public void OnlyWritesThePromptOnTheFirstIterationForInt()
        {
            _console.AddLinesToRead(1, 2, 3);
            _console.PromptInputInt("This is the prompt", 3);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(1);
        }
    }
}