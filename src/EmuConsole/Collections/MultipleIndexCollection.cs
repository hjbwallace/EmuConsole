using EmuConsole.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class MultipleIndexCollection<TEntity>
    {
        private readonly IList<KeyValuePair<string, TEntity>> _source;
        private readonly Func<TEntity, object> _descriptionSelector;
        private readonly bool _allowEmpty;

        public MultipleIndexCollection(IEnumerable<TEntity> source, bool allowEmpty)
            : this(source, x => x?.ToString(), allowEmpty)
        {
        }

        public MultipleIndexCollection(IEnumerable<TEntity> source, Func<TEntity, object> descriptionSelector, bool allowEmpty)
        {
            if (!source.Any())
                throw new ArgumentException("Source must be populated");

            _source = source.Select((x, i) => new KeyValuePair<string, TEntity>(i.ToString(), x)).ToList();
            _descriptionSelector = descriptionSelector;
            _allowEmpty = allowEmpty;
        }

        public TEntity[] GetSelection(IConsole console, CollectionWriteStyle style)
        {
            return GetSelectionInternal(console, style, _source, true);
        }

        private IList<KeyValuePair<string, string>> GenerateDisplay(IEnumerable<KeyValuePair<string, TEntity>> source)
        {
            var length = source.Count();
            var padSize = length.ToString().Length;

            return source.Select(x => new KeyValuePair<string, string>(
                x.Key?.PadLeft(padSize),
                _descriptionSelector(x.Value).ToString()))
                .ToList();
        }

        private TEntity[] GetSelectionInternal(IConsole console, CollectionWriteStyle style, IEnumerable<KeyValuePair<string, TEntity>> source, bool writeCollection)
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

            if (inputs[0]?.StartsWith("%") == true)
            {
                var filter = inputs[0].Substring(1).Trim();
                var newSource = _source
                    .Where(x => _descriptionSelector(x.Value)?.ToString().Contains(filter, StringComparison.InvariantCultureIgnoreCase) == true)
                    .Select((x, i) => new KeyValuePair<string, TEntity>(i.ToString(), x.Value))
                    .ToArray();

                return GetSelectionInternal(console, style, newSource.Any() ? newSource : _source, true);
            }

            var foundInputs = inputs.Intersect(source.Select(x => x.Key));

            if (!foundInputs.Any() && !_allowEmpty)
                return GetSelectionInternal(console, style, source, false);

            return foundInputs
                .Select(x => source.Single(a => a.Key == x).Value)
                .ToArray();
        }
    }
}