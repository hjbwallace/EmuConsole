using Xunit;

namespace EmuConsole.Tests.Prompts
{
    public enum TestEnum
    {
        Value1,
        Value2,
    }

    public class PromptEnumTests : ConsoleTestBase
    {
        [Fact]
        public void CanSelectEntryFromEnum()
        {
            _console.AddLinesToRead(20, 3, 0);
            var result = _console.PromptEnumIndexSelection<TestEnum>(false);

            Assert.Equal(TestEnum.Value1, result);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(3);
            _console.HasOutput($@"
[0] Value1
[1] Value2
> 20
> 3
> 0
");
        }

        [Fact]
        public void CanSelectEntryFromEnumWithInline()
        {
            _console.AddLinesToRead(20, 3, 0);
            var result = _console.PromptEnumIndexSelection<TestEnum>(true);

            Assert.Equal(TestEnum.Value1, result);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(2);
            _console.HasOutput($@"
[0] Value1 [1] Value2
> 20
> 3
> 0
");
        }
    }
}