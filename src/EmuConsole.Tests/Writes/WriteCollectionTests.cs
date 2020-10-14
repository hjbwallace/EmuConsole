using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xunit;

namespace EmuConsole.Tests.Writes
{
    public class WriteCollectionTests : ConsoleTestBase
    {
        private readonly Dictionary<string, string> _source;

        public WriteCollectionTests()
        {
            _source = Enumerable.Range(1, 10).ToDictionary(e => $"Key {e}", e => $"Value {e}");
        }

        [Fact]
        public void CanWriteCollectionAsRows()
        {
            _console.WriteCollection(_source, CollectionWriteStyle.Rows);
            _console.HasOutput($@"[Key 1] Value 1
[Key 2] Value 2
[Key 3] Value 3
[Key 4] Value 4
[Key 5] Value 5
[Key 6] Value 6
[Key 7] Value 7
[Key 8] Value 8
[Key 9] Value 9
[Key 10] Value 10
");
        }

        [Fact]
        public void CanWriteCollectionInline()
        {
            _console.WriteCollection(_source, CollectionWriteStyle.Inline);
            _console.HasOutput($@"[Key 1] Value 1 [Key 2] Value 2 [Key 3] Value 3 [Key 4] Value 4 [Key 5] Value 5 [Key 6] Value 6 [Key 7] Value 7 [Key 8] Value 8 [Key 9] Value 9 [Key 10] Value 10
");
        }

        [Theory]
        [InlineData(54)]
        [InlineData(71)]
        public void CanWriteCollectionWithThreeHorizontalColumns(int width)
        {
            _console.Dimensions = new Size(width, width);
            _console.WriteCollection(_source, CollectionWriteStyle.Columns);
            _console.HasOutput($@"[Key 1] Value 1   [Key 2] Value 2   [Key 3] Value 3
[Key 4] Value 4   [Key 5] Value 5   [Key 6] Value 6
[Key 7] Value 7   [Key 8] Value 8   [Key 9] Value 9
[Key 10] Value 10
");
        }

        [Theory]
        [InlineData(36)]
        [InlineData(53)]
        public void CanWriteCollectionWithTwoHorizontalColumns(int width)
        {
            _console.Dimensions = new Size(width, width);
            _console.WriteCollection(_source, CollectionWriteStyle.Columns);
            _console.HasOutput($@"[Key 1] Value 1   [Key 2] Value 2
[Key 3] Value 3   [Key 4] Value 4
[Key 5] Value 5   [Key 6] Value 6
[Key 7] Value 7   [Key 8] Value 8
[Key 9] Value 9   [Key 10] Value 10
");
        }

        [Theory]
        [InlineData(17)]
        [InlineData(5)]
        [InlineData(0)]
        public void CanWriteCollectionWithSingleHorizontalColumn(int width)
        {
            _console.Dimensions = new Size(width, width);
            _console.WriteCollection(_source, CollectionWriteStyle.Rows);
            _console.HasOutput($@"[Key 1] Value 1
[Key 2] Value 2
[Key 3] Value 3
[Key 4] Value 4
[Key 5] Value 5
[Key 6] Value 6
[Key 7] Value 7
[Key 8] Value 8
[Key 9] Value 9
[Key 10] Value 10
");
        }
    }
}