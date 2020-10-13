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

            var collection = new InputCollection<string>(dictionary);
            var selection = collection.GetSelection(_console, false);

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

            var dictionary = new Dictionary<object, string>
            {
                ["First"] = "Number 1",
                ["Second"] = "Number 2",
                ["Third"] = "Number 3",
            };

            var collection = new InputCollection<string>(dictionary);
            var selection = collection.GetSelection(_console, true);

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

            var collection = new InputCollection<string>(dictionary, (key, value) => value?.ToUpper());
            var selection = collection.GetSelection(_console, false);

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

            var collection = new InputCollection<string>(dictionary, isOptional: true);
            var selection = collection.GetSelection(_console, false);

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

            var collection = new InputCollection<string>(dictionary, (key, value) => value.ToUpper(), isOptional: true);
            var selection = collection.GetSelection(_console, false);

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

            var collection = new InputCollection<string>(dictionary, isOptional: true, defaultValue: "First");
            var selection = collection.GetSelection(_console, false);

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
    }
}