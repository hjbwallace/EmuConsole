using System;
using System.Collections.Generic;
using Xunit;

namespace EmuConsole.Tests.Collections
{
    public class IndexCollectionTests : IndexCollectionTestBase<string>
    {
        [Fact]
        public void CanOptionallySelectEntryFromEnumerableWithDefault()
        {
            _console.AddLinesToRead(20);

            var selection = GetSelectionWithDefault(DefaultSource, defaultValue: 0);

            AssertFound(selection, "one (0)");

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
");
        }

        protected override void AssertFound(string selection, string expected)
        {
            Assert.Equal(expected, selection);
        }

        protected override void AssertMissing(string selection)
        {
            Assert.Null(selection);
        }

        protected override string GetSelection<T>(IEnumerable<T> source,
                                                  Func<T, object> descriptionSelector = null,
                                                  bool writeInline = false,
                                                  bool isOptional = false)
        {
            var collection = new IndexCollection<T>(source, descriptionSelector, isOptional: isOptional);
            return collection.GetSelection(_console, writeInline)?.ToString();
        }

        protected virtual string GetSelectionWithDefault<T>(IEnumerable<T> source,
                                                            Func<T, object> descriptionSelector = null,
                                                            bool writeInline = false,
                                                            int? defaultValue = null)
        {
            var collection = new IndexCollection<T>(source, descriptionSelector, isOptional: true, defaultValue: defaultValue);
            return collection.GetSelection(_console, writeInline)?.ToString();
        }
    }
}