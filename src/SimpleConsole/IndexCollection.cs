using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SimpleConsole
{
    public class IndexCollection<TEntity>
    {
        private readonly IDictionary<int, TEntity> _source;
        private readonly ICollection<(string Key, string Description)> _display;

        public IndexCollection(IEnumerable<TEntity> source)
            : this(source, x => x?.ToString())
        {
        }

        public IndexCollection(IEnumerable<TEntity> source, Func<TEntity, object> descriptionSelector)
        {
            if (!source.Any())
                throw new ArgumentException("Source must be populated");

            _source = new ConcurrentDictionary<int, TEntity>();
            _display = new List<(string, string)>();

            var padSize = source.Count().ToString().Length;

            for (int i = 0; i < source.Count(); i++)
            {
                var item = source.ElementAt(i);

                _source.Add(i, item);
                _display.Add((i.ToString().PadLeft(padSize), descriptionSelector(item).ToString()));
            }
        }

        public TEntity GetSelection(IConsole console)
        {
            console.WriteLine();

            foreach (var entity in _display)
                console.WriteLine($"[{entity.Key}] {entity.Description}");

            var input = console.PromptInputInt(null, _source.Keys.ToArray());
            return _source[input];
        }
    }
}