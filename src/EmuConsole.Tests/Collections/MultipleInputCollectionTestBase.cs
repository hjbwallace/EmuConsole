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

            var selections = GetSelections(dictionary, writeInline: false);

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

            var selections = GetSelections(dictionary, writeInline: true);

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

        protected abstract string[] GetSelections(IDictionary<object, string> source,
                                                  Func<object, string, object> descriptionSelector = null,
                                                  bool allowEmpty = false,
                                                  bool writeInline = false);
    }
}