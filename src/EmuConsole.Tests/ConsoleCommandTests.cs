using System;
using Xunit;

namespace EmuConsole.Tests
{
    public class ConsoleCommandTests
    {
        [Fact]
        public void ThrowsExceptionWithNullKey()
        {
            Assert.Throws<ArgumentException>(() =>
                new ConsoleCommand((string)null, "Description", () => { }));
        }

        [Fact]
        public void ThrowsExceptionWithNullKeys()
        {
            Assert.Throws<ArgumentException>(() =>
                new ConsoleCommand((string[])null, "Description", () => { }));
        }

        [Fact]
        public void ThrowsExceptionWithEmptyKeys()
        {
            Assert.Throws<ArgumentException>(() =>
                new ConsoleCommand(new string[0], "Description", () => { }));
        }

        [Fact]
        public void ThrowsExceptionWithAnyNullKeys()
        {
            Assert.Throws<ArgumentException>(() =>
                new ConsoleCommand(new[] { "key", null }, "Description", () => { }));
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void ThrowsExceptionWithInvalidDescription(string description)
        {
            Assert.Throws<ArgumentException>(() =>
                new ConsoleCommand("key", description, () => { }));
        }

        [Fact]
        public void ThrowsExceptionWithInvalidAction()
        {
            Assert.Throws<ArgumentException>(() =>
                new ConsoleCommand("key", "Description", (Action)null));
        }

        [Fact]
        public void OnlyAddsDistinctKeys()
        {
            var command = new ConsoleCommand(new[] { "key", "key" }, "Description", () => { });
            Assert.Single(command.Keys);
            Assert.Single(command.Keys, "key");
        }

        [Fact]
        public void DistinctKeysAreCaseSensitive()
        {
            var command = new ConsoleCommand(new[] { "key", "KEY", "Key" }, "Description", () => { });
            Assert.Equal(3, command.Keys.Length);
        }
    }
}