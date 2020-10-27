using EmuConsole.Tests.Collections;
using System;
using System.Collections.Generic;

namespace EmuConsole.Tests.Prompts
{
    public class PromptIndexSelectionTests : IndexCollectionTests
    {
        protected override string GetSelection<T>(
            IEnumerable<T> source,
            Func<T, object> descriptionSelector = null,
            bool writeInline = false,
            bool isOptional = false)
        {
            return _console.PromptIndexSelection(
                source,
                descriptionSelector,
                writeInline,
                isOptional)?.ToString();
        }

        protected override string GetSelectionWithDefault<T>(
            IEnumerable<T> source,
            Func<T, object> descriptionSelector = null,
            bool writeInline = false,
            int? defaultValue = null)
        {
            return _console.PromptIndexSelection(
                source,
                descriptionSelector,
                writeInline ? CollectionWriteStyle.Inline : CollectionWriteStyle.Rows,
                defaultValue)?.ToString();
        }
    }
}
