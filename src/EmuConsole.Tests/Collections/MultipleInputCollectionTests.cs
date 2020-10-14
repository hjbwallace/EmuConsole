using System;
using System.Collections.Generic;

namespace EmuConsole.Tests.Collections
{
    public class MultipleInputCollectionTests : MultipleInputCollectionTestBase
    {
        protected override string[] GetSelections(IDictionary<object, string> source,
                                                  Func<object, string, object> descriptionSelector = null,
                                                  bool allowEmpty = false,
                                                  CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            var collection = new MultipleInputCollection<string>(source, descriptionSelector, allowEmpty);
            return collection.GetSelection(_console, style);
        }
    }
}