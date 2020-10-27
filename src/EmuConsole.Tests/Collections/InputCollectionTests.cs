using System;
using System.Collections.Generic;
using Xunit;

namespace EmuConsole.Tests.Collections
{
    public class InputCollectionTests : InputCollectionTestBase<string>
    {
        [Fact]
        public void CanOptionallySelectEntryFromEnumerableWithDefault()
        {
            _console.AddLinesToRead("20");

            var selection = GetSelectionWithDefault(DefaultSource, defaultValue: "First");

            AssertFound(selection, "Number 1");

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[First] Number 1
[Second] Number 2
[Third] Number 3
> 20
");
        }

        protected override string GetSelection<TKey, TValue>(
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            bool isOptional = false,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            var collection = new InputCollection<TKey, TValue>(source, descriptionSelector, isOptional);
            return collection.GetSelection(_console, style)?.ToString();
        }

        protected override void AssertFound(string selection, string expected)
        {
            Assert.Equal(expected, selection);
        }

        protected override void AssertMissing(string selection)
        {
            Assert.Null(selection);
        }

        protected virtual string GetSelectionWithDefault<TKey, TValue>(
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            string defaultValue = null,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            var collection = new InputCollection<TKey, TValue>(source, descriptionSelector, true, defaultValue: defaultValue);
            return collection.GetSelection(_console, style)?.ToString();
        }
    }
}