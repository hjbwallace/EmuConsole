using System;
using System.Collections.Generic;
using Xunit;

namespace EmuConsole.Tests.Collections
{
    public abstract class MultipleInputCollectionTestBase : ConsoleTestBase
    {
        [Fact]
        public void CanSelectEntryFromEnumerable()
        {
            _console.AddLinesToRead("20", "3", "First");

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary, style: CollectionWriteStyle.Rows);

            Assert.Single(selections, "Number 1");

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
> 3
> First
");
        }

        [Fact]
        public void CanSelectEntryFromEnumerableWithInline()
        {
            _console.AddLinesToRead("20", "3", "First");

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary, style: CollectionWriteStyle.Inline);

            Assert.Single(selections, "Number 1");

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
        public void CanSelectMultipleEntriesFromEnumerable()
        {
            _console.AddLinesToRead("20", "3", "First, Second");

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary);

            Assert.Single(selections, "Number 1");

            Assert.Contains("Number 1", selections);
            Assert.Contains("Number 2", selections);
            Assert.Equal(2, selections.Length);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
> 3
> First, Second
");
        }

        [Fact]
        public void CanSelectMultipleEntriesFromEnumerableWithInvalidChoice()
        {
            _console.AddLinesToRead("20", "3", "First, Second, Fourth");

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary);

            Assert.Single(selections, "Number 1");

            Assert.Contains("Number 1", selections);
            Assert.Contains("Number 2", selections);
            Assert.Equal(2, selections.Length);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
> 3
> First, Second, Fourth
");
        }

        [Fact]
        public void CanAllowAnEmptyReturnFromThePrompt()
        {
            _console.AddLinesToRead("20");

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary, allowEmpty: true);

            Assert.Empty(selections);

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
        public void CanSelectEntryFromEnumerableWithDescriptionFormatting()
        {
            _console.AddLinesToRead("20", "3", "First");

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary, (key, value) => value.ToUpper());

            Assert.Single(selections, "Number 1");

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
        public void CanSelectEntryFromEnumerableWithFilter()
        {
            _console.AddLinesToRead("20", "% 1", "First");

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary, allowEmpty: false);

            Assert.Single(selections, "Number 1");

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

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "One",
                ["Second"] = "Two",
                ["Third"] = "Three",
            };

            var selections = GetSelections(dictionary, allowEmpty: false);

            Assert.Single(selections, "One");

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

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary, allowEmpty: false);

            Assert.Single(selections, "Number 1");

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

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary, allowEmpty: false);

            Assert.Single(selections, "Number 3");

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

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary, allowEmpty: true);

            Assert.Single(selections, "Number 2");

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

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selections = GetSelections(dictionary, allowEmpty: true);

            Assert.Empty(selections);

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

        protected abstract string[] GetSelections(IDictionary<object, string> source,
                                                  Func<object, string, object> descriptionSelector = null,
                                                  bool allowEmpty = false,
                                                  CollectionWriteStyle style = CollectionWriteStyle.Rows);
    }
}