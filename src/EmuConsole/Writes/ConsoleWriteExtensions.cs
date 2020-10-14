using System;
using System.Collections.Generic;

namespace EmuConsole
{
    public static class ConsoleWriteExtensions
    {
        public static void WriteLine(this IConsole console) => console.WriteLine("");

        public static T WriteLine<T>(this IConsole console, T value) => console.WriteLine(value, null);

        public static T Write<T>(this IConsole console, T value) => console.Write(value, null);

        public static T Write<T>(this IConsole console, T value, ConsoleColor? colour)
        {
            return console.Write(value, new ConsoleWriteOptions { Foreground = colour });
        }

        public static T WriteLine<T>(this IConsole console, T value, ConsoleColor? colour)
        {
            return console.WriteLine(value, new ConsoleWriteOptions { Foreground = colour });
        }

        public static T WriteLineWarning<T>(this IConsole console, T value)
        {
            return console.WriteLine(value, console.Options.WarningColor);
        }

        public static T WriteLineError<T>(this IConsole console, T value)
        {
            return console.WriteLine(value, console.Options.ErrorColor);
        }

        public static T WriteLineHighlight<T>(this IConsole console, T value)
        {
            return console.WriteLine(value, console.Options.HighlightColor);
        }

        public static T WriteLinePrompt<T>(this IConsole console, T value)
        {
            return console.WriteLine(value, console.Options.PromptColor);
        }

        public static T WriteWarning<T>(this IConsole console, T value)
        {
            return console.Write(value, console.Options.WarningColor);
        }

        public static T WriteError<T>(this IConsole console, T value)
        {
            return console.Write(value, console.Options.ErrorColor);
        }

        public static T WriteHighlight<T>(this IConsole console, T value)
        {
            return console.Write(value, console.Options.HighlightColor);
        }

        public static string WritePrompt(this IConsole console, string context = null)
        {
            if (!string.IsNullOrWhiteSpace(context))
                console.Write($"[{context}] ", console.Options.ErrorColor);

            return console.Write("> ", console.Options.PromptColor);
        }

        public static IEnumerable<T> WriteLines<T>(this IConsole console, IEnumerable<T> source)
        {
            foreach (var value in source)
                yield return console.WriteLine(value);
        }

        public static TException WriteException<TException>(this IConsole console, TException ex) where TException : Exception
            => console.WriteException(null, ex);

        public static TException WriteException<TException>(this IConsole console, string message, TException ex) where TException : Exception
        {
            if (!string.IsNullOrWhiteSpace(message))
                console.WriteLineError(message);

            console.WriteLineError($"[{ex.GetType().Name}]: {ex.Message}");
            console.WriteLineError(ex.StackTrace);
            return ex;
        }
    }
}