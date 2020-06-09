using Xunit;

namespace EmuConsole.Tests.Prompts
{
    public class PromptConfirmTests : ConsoleTestBase
    {
        [Theory]
        [InlineData("y", true)]
        [InlineData("Y", true)]
        [InlineData("", false)]
        [InlineData("yy", false)]
        public void CanPromptTheUserToConfirmAnAction(string input, bool expectedResult)
        {
            _console.AddLinesToRead(input);
            var result = _console.PromptConfirm("A confirmation prompt");

            Assert.Equal(expectedResult, result);
            _console.HasLinesRead(1);
            _console.HasLinesWritten(1);
            _console.HasOutput($@"A confirmation prompt (Y to confirm)
> {input}
");
        }
    }
}