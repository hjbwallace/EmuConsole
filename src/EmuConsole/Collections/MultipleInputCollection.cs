using EmuConsole.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class MultipleInputCollection<TKey, TEntity> : InputCollectionBase<TKey, TEntity, TEntity[]>
    {
        private readonly bool _allowEmpty;

        public MultipleInputCollection(IEnumerable<KeyValuePair<TKey, TEntity>> source,
                                       Func<TKey, TEntity, object> descriptionSelector = null,
                                       bool allowEmpty = false)
            : base(source, descriptionSelector)
        {
            _allowEmpty = allowEmpty;
        }

        protected override TEntity[] GetSelectionInternal(IConsole console, CollectionWriteStyle style, IList<KeyValuePair<TKey, TEntity>> source, bool writeCollection)
        {
            if (writeCollection)
            {
                var display = GenerateDisplay(source);

                console.WriteLine();
                console.WriteCollection(display, style);
            }

            var inputs = console.PromptInputs(null, _allowEmpty);

            if (!inputs.Any())
                return new TEntity[0];

            var sourceKeys = source.Select(x => GetKey(x.Key)).ToArray();

            if (inputs[0]?.StartsWith("%") == true && !sourceKeys.Contains(inputs[0]))
            {
                var filter = inputs[0].Substring(1).Trim();
                var newSourceItems = _source
                    .Where(x => GetDescription(x.Key, x.Value)?.Contains(filter, StringComparison.InvariantCultureIgnoreCase) == true);

                var newSource = MapSource(newSourceItems);

                return GetSelectionInternal(console, style, newSource.Any() ? newSource : _source, true);
            }

            var foundInputs = inputs.SelectMany(MapInput).Intersect(sourceKeys);

            if (!foundInputs.Any() && !_allowEmpty)
                return GetSelectionInternal(console, style, source, false);

            return foundInputs
                .Select(x => source.Single(a => GetKey(a.Key) == x).Value)
                .ToArray();
        }

        protected virtual IEnumerable<string> MapInput(string input)
        { 
            yield return input; 
        }

        protected virtual KeyValuePair<TKey, TEntity>[] MapSource(IEnumerable<KeyValuePair<TKey, TEntity>> source)
        {
            return source.ToArray();
        }
    }
}