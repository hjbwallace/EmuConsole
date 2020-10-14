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