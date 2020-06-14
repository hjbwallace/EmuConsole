using EmuConsole.Extensions;
using System;

namespace EmuConsole
{
    public static class PromptDateTimeOptionalExtensions
    {
        public static DateTime? PromptDateTimeOptional(this IConsole console)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                null,
                null,
                null,
                false);
        }

        public static DateTime? PromptDateTimeOptional(this IConsole console, string promptMessage)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                promptMessage,
                null,
                null,
                false);
        }

        public static DateTime? PromptDateTimeOptional(this IConsole console, string promptMessage, DateTime? defaultValue)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                promptMessage,
                null,
                defaultValue,
                false);
        }

        public static DateTime? PromptDateTimeOptional(this IConsole console, string promptMessage, DateTime[] allowedValues)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                promptMessage,
                allowedValues.AsNullableDateTimes(),
                null,
                true);
        }

        public static DateTime? PromptDateTimeOptional(this IConsole console, string promptMessage, DateTime[] allowedValues, DateTime? defaultValue)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                promptMessage,
                allowedValues.AsNullableDateTimes(),
                defaultValue,
                false);
        }
    }
}