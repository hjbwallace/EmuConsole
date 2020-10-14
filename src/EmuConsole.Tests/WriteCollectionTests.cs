using System;
using System.Collections.Generic;
using Xunit;

namespace EmuConsole.Tests
{
    public class WriteCollectionTests : ConsoleTestBase
    {
        [Fact]
        public void WritesCollectionNotInline()
        {
            var collection = new Dictionary<string, string>
            {
                ["A"] = "Value A",
                ["B"] = "Value B",
                ["C"] = "Value C",
            };

            _console.WriteCollection(collection, false);
            _console.HasOutput($@"[A] Value A
[B] Value B
[C] Value C
");
        }

        [Fact]
        public void WritesCollectionInline()
        {
            var collection = new Dictionary<string, string>
            {
                ["A"] = "Value A",
                ["B"] = "Value B",
                ["C"] = "Value C",
            };

            _console.WriteCollection(collection, true);
            _console.HasOutput($@"[A] Value A [B] Value B [C] Value C
");
        }

        [Fact]
        public void ThrowsExceptionWhenNullCollectionUsed()
        {
            var collection = (IDictionary<string, string>)null;
            Assert.Throws<ArgumentException>(() => _console.WriteCollection(collection));
        }

        [Fact]
        public void ThrowsExceptionWhenEmptyCollectionUsed()
        {
            var collection = new Dictionary<string, string>();
            Assert.Throws<ArgumentException>(() => _console.WriteCollection(collection));
        }
    }
}