using EmuConsole.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmuConsole.IntegrationTests.Mock
{
    public class TestConsoleApp : ConsoleApp
    {
        private readonly List<ConsoleCommand> _commands = new List<ConsoleCommand>();

        public TestConsoleApp(IConsole console) : base(console)
        {
        }

        public TestConsoleApp(IConsole console, ConsoleOptions options) : base(console, options)
        {
        }

        public TestConsoleApp WithCommand(string key, Action action)
        {
            _commands.Add(new ConsoleCommand(key, $"Command for {key}", action));
            return this;
        }

        protected override IEnumerable<ConsoleCommand> GetCommands() => _commands;
    }

    public class Test
    {
        [Fact]
        public async Task DoesntShowTheHelpCommandWhenAlwaysDisplayingCommands()
        {
            var console = new TestConsole();
            console.AddLinesToRead("x");

            var app = new TestConsoleApp(console, new ConsoleOptions { AlwaysDisplayCommands = true });
            await app.RunAsync();

            console.HasOutput(@"
[x|exit] Exit the application
> x

");
        }

        [Fact]
        public async Task ShowsTheHelpCommandWhenNotAlwaysDisplayingCommands()
        {
            var console = new TestConsole();
            console.AddLinesToRead("x");

            var app = new TestConsoleApp(console, new ConsoleOptions { AlwaysDisplayCommands = false });
            await app.RunAsync();

            console.HasOutput(@"
[?|help] Display available commands
[x|exit] Exit the application
> x

");
        }

        [Fact]
        public async Task AppCommandsAreShownAfterHelpAndBeforeExit()
        {
            var console = new TestConsole();
            console.AddLinesToRead("x");

            var app = new TestConsoleApp(console, new ConsoleOptions { AlwaysDisplayCommands = false })
                .WithCommand("A", () => { });
            await app.RunAsync();

            console.HasOutput(@"
[?|help] Display available commands
[A] Command for A
[x|exit] Exit the application
> x

");
        }

        [Fact]
        public async Task ConsoleOptionsFromRunAreUsed()
        {
            var console = new TestConsole();
            console.AddLinesToRead("x");

            var app = new TestConsoleApp(console, new ConsoleOptions { AlwaysDisplayCommands = false });
            await app.RunAsync(new ConsoleOptions { AlwaysDisplayCommands = true });

            Assert.True(console.Options.AlwaysDisplayCommands);
        }
    }


}
