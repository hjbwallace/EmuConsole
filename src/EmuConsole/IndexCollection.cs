using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class IndexCollection<TEntity>
    {
        private readonly IDictionary<int, TEntity> _source;
        private readonly IList<KeyValuePair<string, string>> _display;
        private readonly bool _isOptional;
        private readonly int? _defaultValue;

        public IndexCollection(IEnumerable<TEntity> source, bool isOptional = false, int? defaultValue = default)
            : this(source, x => x?.ToString(), isOptional, defaultValue)
        {
        }

        public IndexCollection(IEnumerable<TEntity> source, Func<TEntity, object> descriptionSelector, bool isOptional = false, int? defaultValue = default)
        {
            if (!source.Any())
                throw new ArgumentException("Source must be populated");

            _source = new ConcurrentDictionary<int, TEntity>();
            _display = new List<KeyValuePair<string, string>>();

            var length = source.Count();
            var padSize = length.ToString().Length;

            for (int i = 0; i < length; i++)
            {
                var item = source.ElementAt(i);

                _source.Add(i, item);
                _display.Add(new KeyValuePair<string, string>(i.ToString().PadLeft(padSize), descriptionSelector(item).ToString()));
            }

            _isOptional = isOptional;
            _defaultValue = defaultValue;
        }

        public TEntity GetSelection(IConsole console)
        {
            console.WriteLine();
            console.WriteCollection(_display);

            var keys = _source.Keys.ToArray();

            var input = _isOptional 
                ? console.PromptInputOptionalInt(null, keys, _defaultValue)
                : console.PromptInputInt(null, keys);

            return input == null ? default : _source[input.Value];
        }
    }
}