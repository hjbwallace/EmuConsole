﻿using System;

namespace SimpleConsole
{
    public class ConsoleOptions
    {
        public string Title { get; set; } = "Console Application";

        public bool AlwaysDisplayCommands { get; set; } = true;

        public bool HighlightPromptOptions { get; set; } = true;

        public ConsoleColor WarningColor { get; set; } = ConsoleColor.Yellow;

        public ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;

        public ConsoleColor HighlightColor { get; set; } = ConsoleColor.Cyan;

        public ConsoleColor PromptColor { get; set; } = ConsoleColor.Yellow;
    }
}