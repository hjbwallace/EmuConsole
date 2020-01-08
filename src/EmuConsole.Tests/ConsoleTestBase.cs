namespace EmuConsole.Tests
{
    public abstract class ConsoleTestBase
    {
        protected readonly TestConsole _console;

        public ConsoleTestBase()
        {
            _console = new TestConsole();
        }
    }
}