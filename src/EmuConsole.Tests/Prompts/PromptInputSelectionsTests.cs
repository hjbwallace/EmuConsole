using EmuConsole.Tests.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole.Tests.Prompts
{
    public class PromptInputSelectionsTests : MultipleInputCollectionTests
    {
        protected override string[] GetSelection<TKey, TValue>(
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            bool isOptional = false,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            return _console
                .PromptInputSelections(source, descriptionSelector, isOptional, style)
                .Select(x => x?.ToString())
                .ToArray();
        }
    }
}