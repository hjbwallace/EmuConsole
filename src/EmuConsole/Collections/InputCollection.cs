using EmuConsole.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class InputCollection<TKey, TEntity> : InputCollectionBase<TKey, TEntity, TEntity>
    {
        private readonly bool _isOptional;
        private readonly string _defaultValue;

        public InputCollection(IEnumerable<KeyValuePair<TKey, TEntity>> source,
                               Func<TKey, TEntity, object> descriptionSelector = null,
                               bool isOptional = false,
                               string defaultValue = default)
            : base(source, descriptionSelector)
        {
            _isOptional = isOptional;
            _defaultValue = defaultValue;
        }

        protected override TEntity GetSelectionInternal(IConsole console,
                                                        CollectionWriteStyle style,
                                                        IList<KeyValuePair<TKey, TEntity>> source,
                                                        bool writeCollection)
        {
            if (writeCollection)
            {
                var display = GenerateDisplay(source);

                console.WriteLine();
                console.WriteCollection(display, style);
            }

            var input = PromptInput(console);

            var foundInput = source.Select(x => x.Key?.ToString()).Contains(input);

            if (foundInput)
                return source.Single(x => x.Key?.ToString() == input).Value;

            if (input.StartsWith("%") == true)
            {
                var filter = input.Substring(1).Trim();
                var newSourceItems = _source
                    .Where(x => GetDescription(x.Key, x.Value)?.Contains(filter, StringComparison.InvariantCultureIgnoreCase) == true);

                var newSource = MapSource(newSourceItems).ToArray();

                return GetSelectionInternal(console, style, newSource.Any() ? newSource : _source, true);
            }

            if (input == null || _isOptional)
                return source.SingleOrDefault(x => x.Key?.ToString() == _defaultValue).Value;

            return GetSelectionInternal(console, style, source, false);
        }

        protected string PromptInput(IConsole console)
        {
            return console.PromptInput(null);
        }

        protected virtual KeyValuePair<TKey, TEntity>[] MapSource(IEnumerable<KeyValuePair<TKey, TEntity>> source)
        {
            return source.ToArray();
        }
    }
}