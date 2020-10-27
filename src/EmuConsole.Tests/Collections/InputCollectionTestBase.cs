using System;
using System.Collections.Generic;
using Xunit;

namespace EmuConsole.Tests.Collections
{
    public abstract class InputCollectionTestBase<TSelection> : ConsoleTestBase
    {
        protected static readonly Dictionary<string, string> DefaultSource = new Dictionary<string, string>
        {
            ["First"] = "Number 1",
            ["Second"] = "Number 2",
            ["Third"] = "Number 3",
        };

        [Theory]
        [InlineData("First")]
        [InlineData("  First")]
        [InlineData("First  ")]
        [InlineData("  First  ")]
        public void CanSelectEntryFromEnumerable(string entry)
        {
            _console.AddLinesToRead("missing", entry);

            var selection = GetSelection(style: CollectionWriteStyle.Rows);

            AssertFound(selection, "Number 1");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> missing
> {entry}
");
        }

        [Theory]
        [InlineData("Firs")]
        [InlineData("irst")]
        [InlineData("FIRST")]
        [InlineData("first")]
        public void SelectingEntryIsCaseSensitive(string entry)
        {
            _console.AddLinesToRead(entry);

            var selection = GetSelection(isOptional: true);

            AssertMissing(selection);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> {entry}
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithEmptyResponse()
        {
            _console.AddLinesToRead("", "First");

            var selection = GetSelection();

            AssertFound(selection, "Number 1");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 
> First
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
[First] Number 1
[Second] Number 2
[Third] Number 3
> 
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithInline()
        {
            _console.AddLinesToRead("20", "3", "First");

            var selection = GetSelection(style: CollectionWriteStyle.Inline);

            AssertFound(selection, "Number 1");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(2);
            _console.HasOutput($@"
[First] Number 1 [Second] Number 2 [Third] Number 3
> 20
> 3
> First
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithDescriptionFormatting()
        {
            _console.AddLinesToRead("20", "3", "First");

            var selection = GetSelection((key, value) => value?.ToUpper());

            AssertFound(selection, "Number 1");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] NUMBER 1
[Second] NUMBER 2
[Third] NUMBER 3
> 20
> 3
> First
");
        }

        [Fact]
        public void CanOptionallySelectEntryFromEnumerable()
        {
            _console.AddLinesToRead("20");

            var selection = GetSelection(isOptional: true);

            AssertMissing(selection);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
");
        }

        [Fact]
        public void CanOptionallySelectEntryFromEnumerableWithDescriptionFormatting()
        {
            _console.AddLinesToRead("20");

            var selection = GetSelection((key, value) => value.ToUpper(), isOptional: true);

            AssertMissing(selection);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] NUMBER 1
[Second] NUMBER 2
[Third] NUMBER 3
> 20
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithFilter()
        {
            _console.AddLinesToRead("20", "% 1", "First");

            var selection = GetSelection(isOptional: false);

            AssertFound(selection, "Number 1");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(6);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
> % 1

[First] Number 1
> First
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
            _console.AddLinesToRead(filter, "First");

            var source = new Dictionary<object, string>
            {
                ["First"] = "One",
                ["Second"] = "Two",
                ["Third"] = "Three",
            };

            var selection = GetSelection(source, isOptional: false);

            AssertFound(selection, "One");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(6);
            _console.HasOutput($@"
[First] One
[Second] Two
[Third] Three
> {filter}

[First] One
> First
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWhenFilterReturnsNone()
        {
            _console.AddLinesToRead("20", "% missing", "First");

            var selection = GetSelection(isOptional: false);

            AssertFound(selection, "Number 1");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(8);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
> % missing

[First] Number 1
[Second] Number 2
[Third] Number 3
> First
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithMultipleFilters()
        {
            _console.AddLinesToRead(20, "% 2", "%3", "Third");

            var selection = GetSelection(isOptional: false);

            AssertFound(selection, "Number 3");

            _console.HasLinesRead(4);
            _console.HasLinesWritten(8);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
> % 2

[Second] Number 2
> %3

[Third] Number 3
> Third
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithFilterWhileAllowingEmpty()
        {
            _console.AddLinesToRead("% 2", "Second");

            var selection = GetSelection(isOptional: true);

            AssertFound(selection, "Number 2");

            _console.HasLinesRead(2);
            _console.HasLinesWritten(6);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> % 2

[Second] Number 2
> Second
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithFilterWithMissingSelection()
        {
            _console.AddLinesToRead("% 2", "First");

            var selection = GetSelection(isOptional: true);

            AssertMissing(selection);

            _console.HasLinesRead(2);
            _console.HasLinesWritten(6);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> % 2

[Second] Number 2
> First
");
        }

        [Theory]
        [InlineData("%Test")]
        [InlineData("% test")]
        public void CanSelectEntryFromEnumerableWithFilterKey(string entry)
        {
            _console.AddLinesToRead(entry);

            var source = new Dictionary<string, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
                [entry] = "Entry with filter key"
            };

            var selection = GetSelection(source, isOptional: true);

            AssertFound(selection, "Entry with filter key");

            _console.HasLinesRead(1);
            _console.HasLinesWritten(5);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
[{entry}] Entry with filter key
> {entry}
");
        }

        protected abstract TSelection GetSelection<TKey, TValue>(
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            bool isOptional = false,
            CollectionWriteStyle style = CollectionWriteStyle.Rows);

        protected TSelection GetSelection(
            Func<string, string, object> descriptionSelector = null,
            bool isOptional = false,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            return GetSelection(DefaultSource, descriptionSelector, isOptional, style);
        }

        protected abstract void AssertFound(TSelection selection, string expected);

        protected abstract void AssertMissing(TSelection selection);
    }
}