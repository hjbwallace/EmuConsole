using System.Linq;
using Xunit;

namespace EmuConsole.Tests
{
    public class IndexCollectionTests : ConsoleTestBase
    {
        [Fact]
        public void CanSelectEntryFromEnumerable()
        {
            _console.AddLinesToRead(20, 3, 0);

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new IndexCollection<string>(collection);
            var selection = indexCollection.GetSelection(_console);

            Assert.Equal("one (0)", selection);

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
        public void CanSelectEntryFromEnumerableWithDescriptionFormatting()
        {
            _console.AddLinesToRead(20, 3, 0);

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new IndexCollection<string>(collection, x => x.ToUpper());
            var selection = indexCollection.GetSelection(_console);

            Assert.Equal("one (0)", selection);

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

        [Fact]
        public void CanSelectEntryFromLargeEnumerable()
        {
            _console.AddLinesToRead(0);

            var collection = Enumerable.Range(1, 20).Select(x => $"Entry {x}").ToArray();
            var indexCollection = new IndexCollection<string>(collection);
            var selection = indexCollection.GetSelection(_console);

            Assert.Equal("Entry 1", selection);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(21);
            _console.HasOutput($@"
[ 0] Entry 1
[ 1] Entry 2
[ 2] Entry 3
[ 3] Entry 4
[ 4] Entry 5
[ 5] Entry 6
[ 6] Entry 7
[ 7] Entry 8
[ 8] Entry 9
[ 9] Entry 10
[10] Entry 11
[11] Entry 12
[12] Entry 13
[13] Entry 14
[14] Entry 15
[15] Entry 16
[16] Entry 17
[17] Entry 18
[18] Entry 19
[19] Entry 20
> 0
");
        }

        [Fact]
        public void CanOptionallySelectEntryFromEnumerable()
        {
            _console.AddLinesToRead(20);

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new IndexCollection<string>(collection, true);
            var selection = indexCollection.GetSelection(_console);

            Assert.Null(selection);

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
        public void CanOptionallySelectEntryFromEnumerableWithDescriptionFormatting()
        {
            _console.AddLinesToRead(20);

            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new IndexCollection<string>(collection, x => x.ToUpper(), true);
            var selection = indexCollection.GetSelection(_console);

            Assert.Null(selection);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] ONE (0)
[1] TWO (1)
[2] THREE (2)
> 20
");
        }

        [Fact]
        public void CanOptionallySelectEntryFromEnumerableWithDefault()
        {
            _console.AddLinesToRead(20);

            var defaultValue = 0;
            var collection = new[] { "one (0)", "two (1)", "three (2)" };
            var indexCollection = new IndexCollection<string>(collection, true, defaultValue);
            var selection = indexCollection.GetSelection(_console);

            Assert.Equal("one (0)", selection);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
");
        }
    }
}