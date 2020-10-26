using System;

namespace EmuConsole
{
    public static class PromptDateTimeExtensions
    {
        public static DateTime PromptDateTime(this IConsole console)
        {
            return console.PromptDateTimeInternal(
                null,
                null,
                null,
                false,
                true);
        }

        public static DateTime PromptDateTime(this IConsole console, string promptMessage)
        {
            return console.PromptDateTimeInternal(
                promptMessage,
                null,
                null,
                false,
                true);
        }

        public static DateTime PromptDateTime(this IConsole console, string promptMessage, DateTime defaultValue)
        {
            return console.PromptDateTimeInternal(
                promptMessage,
                null,
                defaultValue,
                true,
                false);
        }

        public static DateTime PromptDateTime(this IConsole console, string promptMessage, DateTime[] allowedValues)
        {
            return console.PromptDateTimeInternal(
                promptMessage,
                allowedValues,
                null,
                false,
                true);
        }

        public static DateTime PromptDateTime(this IConsole console, string promptMessage, DateTime[] allowedValues, DateTime defaultValue)
        {
            return console.PromptDateTimeInternal(
                promptMessage,
                allowedValues,
                defaultValue,
                true,
                false);
        }

        internal static DateTime PromptDateTimeInternal(
            this IConsole console,
            string promptMessage,
            DateTime[] allowedValues,
            DateTime? defaultValue,
            bool hasDefault,
            bool retry)
        {
            return console.PromptDateTimeOptionalInternal(
                promptMessage,
                allowedValues,
                defaultValue,
                hasDefault,
                retry).Value;
        }
    }
}