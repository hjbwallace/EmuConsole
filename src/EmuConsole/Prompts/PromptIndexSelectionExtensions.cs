using System;
using System.Collections.Generic;

namespace EmuConsole
{
    public static class PromptIndexSelectionExtensions
    {
        public static T PromptIndexSelection<T>(this IConsole console, IEnumerable<T> source, bool writeInline = false)
        {
            var indexCollection = new IndexCollection<T>(source);
            return indexCollection.GetSelection(console, writeInline);
        }

        public static T PromptIndexSelection<T>(this IConsole console, IEnumerable<T> source, CollectionWriteStyle style)
        {
            var indexCollection = new IndexCollection<T>(source);
            return indexCollection.GetSelection(console, style);
        }

        public static T PromptIndexSelection<T>(this IConsole console, IEnumerable<T> source, Func<T, object> descriptionSelector, bool writeInline = false)
        {
            var indexCollection = new IndexCollection<T>(source, descriptionSelector);
            return indexCollection.GetSelection(console, writeInline);
        }

        public static T PromptIndexSelection<T>(this IConsole console, IEnumerable<T> source, Func<T, object> descriptionSelector, CollectionWriteStyle style)
        {
            var indexCollection = new IndexCollection<T>(source, descriptionSelector);
            return indexCollection.GetSelection(console, style);
        }

        public static T[] PromptIndexSelections<T>(this IConsole console, IEnumerable<T> source, bool allowEmpty = false, bool writeInline = false)
        {
            var indexCollection = new MultipleIndexCollection<T>(source, allowEmpty);
            return indexCollection.GetSelection(console, writeInline);
        }

        public static T[] PromptIndexSelections<T>(this IConsole console, IEnumerable<T> source, bool allowEmpty, CollectionWriteStyle style)
        {
            var indexCollection = new MultipleIndexCollection<T>(source, allowEmpty);
            return indexCollection.GetSelection(console, style);
        }

        public static T[] PromptIndexSelections<T>(this IConsole console, IEnumerable<T> source, Func<T, object> descriptionSelector, bool allowEmpty, CollectionWriteStyle style)
        {
            var indexCollection = new MultipleIndexCollection<T>(source, descriptionSelector, allowEmpty);
            return indexCollection.GetSelection(console, style);
        }
    }
}