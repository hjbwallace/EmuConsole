using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimpleConsole.Tests
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
    }
}
