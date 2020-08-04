using System.Threading.Tasks;
using Xunit;

namespace EmuConsole.Tests
{
    public class ConsoleAppTests
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