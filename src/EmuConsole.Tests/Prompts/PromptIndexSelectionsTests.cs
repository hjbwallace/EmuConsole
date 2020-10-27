using EmuConsole.Tests.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole.Tests.Prompts
{
    public class PromptIndexSelectionsTests : MultipleIndexCollectionTests
    {
        protected override string[] GetSelection<T>(IEnumerable<T> source,
                                                    Func<T, object> descriptionSelector = null,
                                                    bool writeInline = false,
                                                    bool isOptional = false)
        {
            return _console.PromptIndexSelections(
                source,
                descriptionSelector,
                allowEmpty: isOptional,
                writeInline: writeInline).Select(x => x?.ToString()).ToArray();
        }
    }
}
