using Xunit;

namespace EmuConsole.Tests
{
    public class MultipleIndexCollectionTests : ConsoleTestBase
    {
        [Fact]
        public void CanSelectEntryFromEnumerable()
        {
            _console.AddLinesToRead(20, 3, 0);

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new MultipleIndexCollection<string>(collection, false);
            var selections = indexCollection.GetSelection(_console);

            Assert.Single(selections, "one (0)");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
> 3
> 0
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithInline()
        {
            _console.AddLinesToRead(20, 3, 0);

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new MultipleIndexCollection<string>(collection, false);
            var selections = indexCollection.GetSelection(_console, true);

            Assert.Single(selections, "one (0)");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(2);
            _console.HasOutput($@"
[0] one (0) [1] two (1) [2] three (2) 
> 20
> 3
> 0
");
        }

        [Fact]
        public void CanSelectMultipleEntriesFromEnumerable()
        {
            _console.AddLinesToRead("20", "3", "0, 1");

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new MultipleIndexCollection<string>(collection, false);
            var selections = indexCollection.GetSelection(_console);

            Assert.Contains("one (0)", selections);
            Assert.Contains("two (1)", selections);
            Assert.Equal(2, selections.Length);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
> 3
> 0, 1
");
        }

        [Fact]
        public void CanSelectMultipleEntriesFromEnumerableWithInvalidChoice()
        {
            _console.AddLinesToRead("20", "3", "0, 1, 3");

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new MultipleIndexCollection<string>(collection, false);
            var selections = indexCollection.GetSelection(_console);

            Assert.Contains("one (0)", selections);
            Assert.Contains("two (1)", selections);
            Assert.Equal(2, selections.Length);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
> 3
> 0, 1, 3
");
        }

        [Fact]
        public void CanAllowAnEmptyReturnFromThePrompt()
        {
            _console.AddLinesToRead("20");

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new MultipleIndexCollection<string>(collection, true);
            var selections = indexCollection.GetSelection(_console);

            Assert.Empty(selections);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithDescriptionFormatting()
        {
            _console.AddLinesToRead(20, 3, 0);

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new MultipleIndexCollection<string>(collection, x => x.ToUpper(), false);
            var selections = indexCollection.GetSelection(_console);

            Assert.Single(selections, "one (0)");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] ONE (0)
[1] TWO (1)
[2] THREE (2)
> 20
> 3
> 0
");
        }
    }
}