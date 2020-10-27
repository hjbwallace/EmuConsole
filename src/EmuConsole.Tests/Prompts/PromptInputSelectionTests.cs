using EmuConsole.Tests.Collections;
using System;
using System.Collections.Generic;

namespace EmuConsole.Tests.Prompts
{
    public class PromptInputSelectionTests : InputCollectionTests
    {
        protected override string GetSelection<TKey, TValue>(
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            bool isOptional = false,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            return _console.PromptInputSelection(
                source,
                descriptionSelector,
                isOptional,
                style)?.ToString();
        }

        protected override string GetSelectionWithDefault<TKey, TValue>(
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            string defaultValue = null,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            return _console.PromptInputSelection(
                source,
                descriptionSelector,
                defaultValue,
                style)?.ToString();
        }
    }
}
