using EmuConsole.Extensions;
using System;

namespace EmuConsole
{
    public static class PromptDateTimeOptionalExtensions
    {
        public static DateTime? PromptDateTimeOptional(this IConsole console)
        {
            return console.PromptDateTimeOptionalInternal(
                null,
                null,
                null,
                false,
                false);
        }

        public static DateTime? PromptDateTimeOptional(this IConsole console, string promptMessage)
        {
            return console.PromptDateTimeOptionalInternal(
                promptMessage,
                null,
                null,
                false,
                false);
        }

        public static DateTime? PromptDateTimeOptional(this IConsole console, string promptMessage, DateTime? defaultValue)
        {
            return console.PromptDateTimeOptionalInternal(
                promptMessage,
                null,
                defaultValue,
                true,
                false);
        }

        public static DateTime? PromptDateTimeOptional(this IConsole console, string promptMessage, DateTime[] allowedValues)
        {
            return console.PromptDateTimeOptionalInternal(
                promptMessage,
                allowedValues,
                null,
                false,
                true);
        }

        public static DateTime? PromptDateTimeOptional(this IConsole console, string promptMessage, DateTime[] allowedValues, DateTime? defaultValue)
        {
            return console.PromptDateTimeOptionalInternal(
                promptMessage,
                allowedValues,
                defaultValue,
                true,
                false);
        }

        internal static DateTime? PromptDateTimeOptionalInternal(
            this IConsole console, 
            string promptMessage, 
            DateTime[] allowedValues, 
            DateTime? defaultValue, 
            bool hasDefault, 
            bool retry)
        {
            return console.PromptValueInternal(
                a => a.ReadDateTime(),
                promptMessage,
                allowedValues?.AsNullableDateTimes(),
                defaultValue,
                hasDefault,
                retry);
        }
    }
}