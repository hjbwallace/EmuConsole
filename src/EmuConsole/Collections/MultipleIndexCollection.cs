using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class MultipleIndexCollection<TEntity> : MultipleInputCollection<int, TEntity>
    {
        public MultipleIndexCollection(IDictionary<int, TEntity> source,
                                       Func<int, TEntity, object> descriptionSelector = null,
                                       bool allowEmpty = false) 
            : base(source, descriptionSelector, allowEmpty)
        {
        }

        public MultipleIndexCollection(IEnumerable<TEntity> source,
                                       Func<int, TEntity, object> descriptionSelector = null,
                                       bool allowEmpty = false) 
            : this(MapSourceFromEnumerable(source), descriptionSelector, allowEmpty)
        {
        }

        public MultipleIndexCollection(IEnumerable<TEntity> source, bool allowEmpty)
            : this(source, x => x?.ToString(), allowEmpty)
        {
        }

        public MultipleIndexCollection(IEnumerable<TEntity> source,
                                       Func<TEntity, object> descriptionSelector,
                                       bool allowEmpty)
            : this(MapSourceFromEnumerable(source), GenerateDescriptionSelector(descriptionSelector), allowEmpty)
        {
        }

        protected override KeyValuePair<int, TEntity>[] MapSource(IEnumerable<KeyValuePair<int, TEntity>> source)
        {
            return source
                .Select((x, i) => new KeyValuePair<int, TEntity>(i, x.Value))
                .ToArray();
        }

        private static Func<int, TEntity, object> GenerateDescriptionSelector(Func<TEntity, object> descriptionSelector)
        {
            return descriptionSelector != null
                ? ((key, value) => descriptionSelector(value))
                : (Func<int, TEntity, object>)null;
        }

        private static IDictionary<int, TEntity> MapSourceFromEnumerable(IEnumerable<TEntity> source)
        {
            return source?
                .Select((x, i) => new KeyValuePair<int, TEntity>(i, x))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}