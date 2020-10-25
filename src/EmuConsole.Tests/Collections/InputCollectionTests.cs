using System;
using System.Collections.Generic;
using Xunit;

namespace EmuConsole.Tests.Collections
{
    public class InputCollectionTests : ConsoleTestBase
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

            var selection = GetSelection(dictionary, style: CollectionWriteStyle.Rows);

            Assert.Equal("Number 1", selection);

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

            var dictionary = new Dictionary<string, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selection = GetSelection(dictionary, style: CollectionWriteStyle.Inline);

            Assert.Equal("Number 1", selection);

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

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selection = GetSelection(dictionary, (key, value) => value?.ToUpper());

            Assert.Equal("Number 1", selection);

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

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selection = GetSelection(dictionary, isOptional: true);

            Assert.Null(selection);

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

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selection = GetSelection(dictionary, (key, value) => value.ToUpper(), isOptional: true);

            Assert.Null(selection);

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
        public void CanOptionallySelectEntryFromEnumerableWithDefault()
        {
            _console.AddLinesToRead("20");

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selection = GetSelection(dictionary, isOptional: true, defaultValue: "First");

            Assert.Equal("Number 1", selection);

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
        public void CanSelectEntryFromEnumerableWithFilter()
        {
            _console.AddLinesToRead("20", "% 1", "First");

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var selection = GetSelection(dictionary, isOptional: false);

            Assert.Equal("Number 1", selection);

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

            var selection = GetSelection(dictionary, isOptional: false);

            Assert.Equal("One", selection);

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

            var selection = GetSelection(dictionary, isOptional: false);

            Assert.Equal("Number 1", selection);

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

            var selection = GetSelection(dictionary, isOptional: false);

            Assert.Equal("Number 3", selection);

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

            var selection = GetSelection(dictionary, isOptional: true);

            Assert.Equal("Number 2", selection);

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

            var selection = GetSelection(dictionary, isOptional: true);

            Assert.Null(selection);

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

        protected TValue GetSelection<TKey, TValue>(
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            bool isOptional = false,
            string defaultValue = default,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            var collection = new InputCollection<TKey, TValue>(source, descriptionSelector, isOptional, defaultValue);
            return collection.GetSelection(_console, style);
        }
    }
}