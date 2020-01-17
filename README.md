# EmuConsole
Framework for building simple console applications

## Creating an app

A console app can be created by subclassing `ConsoleApp` in the `EmuConsole` namespace and overriding the required functionality.

These include:
* Commands for a given page
* Heading to display when the process is run for the first time
* Closing message for when the process has ended

### Standard Console
An implementation of the standard console functionality in a C# project which uses `Console.WriteLine` and `Console.Readline` for IO. If no `IConsole` implementation is added to a console app, a new instance of `StandardConsole` will be used instead.

Passing arguments to the `StandardConsole` constructor will give the implementation default inputs to read from whenenver the app requests user input. If there are no remaining default inputs, the app will revert to asking the user directly for input.

### Console Commands
Console commands are how the application knows how to handle user input. The can be wired up to listen to a variety of inputs, as long as they dont conflict with a previously used input on the same process.

Commands are added to a console process by overriding the `GetCommands` method.

These commands can also be conditionally made available by using the `canExecute` parameter.

```
protected override IEnumerable<ConsoleCommand> GetCommands()
{
    yield return new ConsoleCommand(new[] { "w", "words" }, "Display words and then choose one", OnChooseWord);
    yield return new ConsoleCommand(new[] { "p", "populate" }, "Populate the numbers (only if they arent populated)", OnPopulateNumbers, CanPopulateNumbers);
    yield return new ConsoleCommand(new[] { "n", "numbers" }, "Display numbers and then choose one", OnChooseNumber, CanChooseNumber);
    yield return new ConsoleCommand(new[] { "c", "command" }, "Run a different console process", new ExampleConsoleProcess(_console, _options));
    yield return new ConsoleCommand("m", "Enter multiple values in a single action", OnEnterMultiple);
}
```

```
[?|help] Display available commands
[c|command] Run a different console process
[m] Enter multiple values in a single action
[p|populate] Populate the numbers (only if they arent populated)
[w|words] Display words and then choose one
[x|exit] Exit the application
```

### Implementation
```
var console = new StandardConsole(args);
var options = new ConsoleOptions
{
    Title = "An Example App",
    AlwaysDisplayCommands = true
};

// ExampleConsoleApp inherits ConsoleApp
await new ExampleConsoleApp(console, options).RunAsync();
```

## Example Implementation
Look at the `ExampleApp` project for examples on how to wire everything together.
