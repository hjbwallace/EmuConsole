using System;
using Xunit;

namespace EmuConsole.Tests
{
    public class ConsoleCommandCollectionTests
    {
        [Fact]
        public void ThrowsExceptionIfNoCommandsProvided()
        {
            Assert.Throws<ArgumentException>(() => new ConsoleCommandCollection());
        }

        [Fact]
        public void ThrowsExceptionIfCommandIsNull()
        {
            Assert.Throws<ArgumentException>(() => new ConsoleCommandCollection(null));
        }

        [Fact]
        public void ThrowsExceptionIfCommandAddedWithDuplicateKey()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new ConsoleCommandCollection(
                    new ConsoleCommand("a", "Command a", () => { }),
                    new ConsoleCommand("a", "Command a again", () => { })));
        }

        [Fact]
        public void ThrowsExceptionIfCommandAddedWithAnyDuplicateKey()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new ConsoleCommandCollection(
                    new ConsoleCommand("a", "Command a", () => { }),
                    new ConsoleCommand(new[] { "a", "b" }, "Command a again with multiple keys", () => { })));
        }

        [Fact]
        public void KeysAreCaseSensitive()
        {
            new ConsoleCommandCollection(
                new ConsoleCommand("a", "Command a", () => { }),
                new ConsoleCommand("A", "Command A", () => { }));
        }
    }
}