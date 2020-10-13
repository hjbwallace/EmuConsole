using EmuConsole.Tests.Collections;
using System;
using System.Collections.Generic;

namespace EmuConsole.Tests.Prompts
{
    public class PromptInputSelectionsTests : MultipleInputCollectionTestBase
    {
        protected override string[] GetSelections(IDictionary<object, string> source,
                                                  Func<object, string, object> descriptionSelector = null,
                                                  bool allowEmpty = false,
                                                  bool writeInline = false)
        {
            return _console.PromptInputSelections(source, descriptionSelector, allowEmpty, writeInline);
        }
    }
}