using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EmuConsole.Tests.Collections
{
    public class MultipleInputCollectionTests : InputCollectionTestBase<string[]>
    {
        [Fact]
        public void CanSelectMultipleEntriesFromEnumerable()
        {
            _console.AddLinesToRead("20", "First, Second");

            var selection = GetSelection();

            AssertFound(selection, "Number 1");
            AssertFound(selection, "Number 2");
            Assert.Equal(2, selection.Length);

            _console.HasLinesRead(2);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
> First, Second
");
        }

        [Fact]
        public void CanSelectMultipleEntriesFromEnumerableWithInvalidChoice()
        {
            _console.AddLinesToRead("20", "First, Second, Fourth");

            var selection = GetSelection();

            AssertFound(selection, "Number 1");
            AssertFound(selection, "Number 2");
            Assert.Equal(2, selection.Length);

            _console.HasLinesRead(2);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
> First, Second, Fourth
");
        }

        protected override string[] GetSelection<TKey, TValue>(
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            bool isOptional = false,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            var collection = new MultipleInputCollection<TKey, TValue>(
                source,
                descriptionSelector,
                isOptional);

            return collection.GetSelection(_console, style).Select(x => x?.ToString()).ToArray();
        }

        protected override void AssertFound(string[] selection, string expected)
        {
            Assert.Single(selection, expected);
        }

        protected override void AssertMissing(string[] selection)
        {
            Assert.Empty(selection);
        }
    }
}