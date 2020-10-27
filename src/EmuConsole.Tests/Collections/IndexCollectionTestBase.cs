using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EmuConsole.Tests.Collections
{
    public abstract class IndexCollectionTestBase<TSelection> : ConsoleTestBase
    {
        protected static readonly string[] DefaultSource = new[]
        {
            "one (0)",
            "two (1)",
            "three (2)"
        };

        [Fact]
        public void CanSelectEntryFromEnumerable()
        {
            _console.AddLinesToRead(20, 3, 0);

            var selection = GetSelection();

            AssertFound(selection, "one (0)");

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
        public void CanSelectEntryFromEnumerableWithEmptyResponse()
        {
            _console.AddLinesToRead("", 0);

            var selection = GetSelection();

            AssertFound(selection, "one (0)");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 
> 0
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithEmptyResponseWhenAllowingEmpty()
        {
            _console.AddLinesToRead("");

            var selection = GetSelection(isOptional: true);

            AssertMissing(selection);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithInline()
        {
            _console.AddLinesToRead(20, 3, 0);

            var selection = GetSelection(writeInline: true);

            AssertFound(selection, "one (0)");

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
        public void CanSelectEntryFromEnumerableWithDescriptionFormatting()
        {
            _console.AddLinesToRead(20, 3, 0);

            var selection = GetSelection(x => x.ToUpper());

            AssertFound(selection, "one (0)");

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

            var source = Enumerable.Range(1, 20).Select(x => $"Entry {x}").ToArray();
            var selection = GetSelection(source);

            AssertFound(selection, "Entry 1");

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

            var selection = GetSelection(isOptional: true);

            AssertMissing(selection);

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

            var selection = GetSelection(x => x.ToUpper(), isOptional: true);

            AssertMissing(selection);

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
        public void CanSelectEntryFromEnumerableWithFilter()
        {
            _console.AddLinesToRead(20, "% two", 0);

            var selection = GetSelection();

            AssertFound(selection, "two (1)");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(6);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
> % two

[0] two (1)
> 0
");
        }

        [Theory]
        [InlineData("% ONE")]
        [InlineData("% N")]
        [InlineData("% n")]
        [InlineData("% One")]
        [InlineData("% one")]
        [InlineData("%ONE   ")]
        [InlineData("%One   ")]
        [InlineData("%one   ")]
        public void FilterIsCaseInsensitiveAndTrimmed(string filter)
        {
            _console.AddLinesToRead(filter, 0);

            var selection = GetSelection();

            AssertFound(selection, "one (0)");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(6);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> {filter}

[0] one (0)
> 0
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWhenFilterReturnsNone()
        {
            _console.AddLinesToRead(20, "% missing", 0);

            var selection = GetSelection();

            AssertFound(selection, "one (0)");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(8);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
> % missing

[0] one (0)
[1] two (1)
[2] three (2)
> 0
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithMultipleFilters()
        {
            _console.AddLinesToRead(20, "% two", "%(0)", 0);

            var selection = GetSelection();

            AssertFound(selection, "one (0)");

            _console.HasLinesRead(4);
            _console.HasLinesWritten(8);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
> % two

[0] two (1)
> %(0)

[0] one (0)
> 0
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithFilterWhileAllowingEmpty()
        {
            _console.AddLinesToRead("% two", 0);

            var selection = GetSelection(isOptional: true);

            AssertFound(selection, "two (1)");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(6);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> % two

[0] two (1)
> 0
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithFilterWithMissingSelection()
        {
            _console.AddLinesToRead("% two", 1);

            var selection = GetSelection(isOptional: true);

            AssertMissing(selection);

            _console.HasLinesRead(2);
            _console.HasLinesWritten(6);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> % two

[0] two (1)
> 1
");
        }

        protected abstract TSelection GetSelection<T>(IEnumerable<T> source, Func<T, object> descriptionSelector = null, bool writeInline = false, bool isOptional = false);

        protected TSelection GetSelection(Func<string, object> descriptionSelector = null, bool writeInline = false, bool isOptional = false)
        {
            return GetSelection(DefaultSource, descriptionSelector, writeInline, isOptional);
        }

        protected abstract void AssertFound(TSelection selection, string expected);

        protected abstract void AssertMissing(TSelection selection);
    }
}