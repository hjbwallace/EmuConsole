using EmuConsole.Extensions;
using System;

namespace EmuConsole
{
    public static class PromptDateTimeExtensions
    {
        public static DateTime PromptDateTime(this IConsole console)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                null,
                null,
                null,
                true).Value;
        }

        public static DateTime PromptDateTime(this IConsole console, string promptMessage)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                promptMessage,
                null,
                null,
                true).Value;
        }

        public static DateTime PromptDateTime(this IConsole console, string promptMessage, DateTime defaultValue)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                promptMessage,
                null,
                defaultValue,
                false).Value;
        }

        public static DateTime PromptDateTime(this IConsole console, string promptMessage, DateTime[] allowedValues)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                promptMessage,
                allowedValues.AsNullableDateTimes(),
                null,
                true).Value;
        }

        public static DateTime PromptDateTime(this IConsole console, string promptMessage, DateTime[] allowedValues, DateTime defaultValue)
        {
            return console.PromptInputInternal(
                a => a.ReadDateTime(),
                promptMessage,
                allowedValues.AsNullableDateTimes(),
                defaultValue,
                false).Value;
        }
    }
}