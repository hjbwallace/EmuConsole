namespace EmuConsole
{
    public class ConsoleApp : ConsoleProcess
    {
        public ConsoleApp()
            : base(new StandardConsole(), new ConsoleOptions())
        {
        }

        public ConsoleApp(ConsoleOptions options)
            : base(new StandardConsole(), options)
        {
        }

        public ConsoleApp(IConsole console)
            : base(console, new ConsoleOptions())
        {
        }

        public ConsoleApp(IConsole console, ConsoleOptions options)
            : base(console, options)
        {
        }

        protected override ConsoleCommand GetExitCommand()
        {
            return new ConsoleCommand(new[] { "x", "exit" }, "Exit the application", StopRunning);
        }
    }
}