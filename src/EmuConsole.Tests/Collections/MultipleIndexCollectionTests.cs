using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EmuConsole.Tests.Collections
{
    public class MultipleIndexCollectionTests : IndexCollectionTestBase<string[]>
    {
        [Fact]
        public void CanSelectMultipleEntriesFromEnumerable()
        {
            _console.AddLinesToRead("20", "3", "0, 1");

            var selections = GetSelection();

            Assert.Contains("one (0)", selections);
            Assert.Contains("two (1)", selections);
            Assert.Equal(2, selections.Length);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
> 3
> 0, 1
");
        }

        [Fact]
        public void CanSelectMultipleEntriesFromEnumerableWithInvalidChoice()
        {
            _console.AddLinesToRead("20", "3", "0, 1, 3");

            var selections = GetSelection();

            Assert.Contains("one (0)", selections);
            Assert.Contains("two (1)", selections);
            Assert.Equal(2, selections.Length);

            _console.HasLinesRead(3);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 20
> 3
> 0, 1, 3
");
        }

        [Fact]
        public void CanSelectMultipleEntriesFromEnumerableWithRange()
        {
            _console.AddLinesToRead("0/2");

            var selections = GetSelection();

            Assert.Contains("one (0)", selections);
            Assert.Contains("two (1)", selections);
            Assert.Contains("three (2)", selections);
            Assert.Equal(3, selections.Length);

            _console.HasLinesRead(1);
            _console.HasLinesWritten(4);
            _console.HasOutput($@"
[0] one (0)
[1] two (1)
[2] three (2)
> 0/2
");
        }

        protected override void AssertFound(string[] selection, string expected)
        {
            Assert.Single(selection, expected);
            Assert.Single(selection);
        }

        protected override void AssertMissing(string[] selection)
        {
            Assert.Empty(selection);
        }

        protected override string[] GetSelection<T>(IEnumerable<T> source,
                                                    Func<T, object> descriptionSelector = null,
                                                    bool writeInline = false,
                                                    bool isOptional = false)
        {
            var collection = new MultipleIndexCollection<T>(source, descriptionSelector, allowEmpty: isOptional);
            return collection.GetSelection(_console, writeInline).Select(x => x?.ToString()).ToArray();
        }
    }
}