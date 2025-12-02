using System.Collections;
using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.Naming;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations
{
    public class LiveDictionaryContext(
        IDictionary dictionary,
        ISchema valueSchema,
        ISchema keySchema,
        IContextRegistry registry,
        bool isPrimitive,
        IStringSplitter stringSplitter)
        : ContextBase
    {
        public IDictionary Dictionary => dictionary;
        public ISchema ValueSchema => valueSchema;
        public ISchema KeySchema => keySchema;

        public LiveDictionaryContext(
            IDictionary dictionary,
            ISchema valueSchema,
            ISchema keySchema,
            IContextRegistry registry,
            bool isPrimitive)
            : this(dictionary, valueSchema, keySchema, registry, isPrimitive, new SplitterByChars())
        {
        }

        // ----------------- string → array → overload -----------------

        public override bool ExistContext(string path)
            => GetContext(path) != null;

        public override bool ExistValue(string path)
            => GetValue(path) != null;

        public override IValue? GetValue(string path)
        {
            var parts = stringSplitter.Split(path);
            return GetValue(parts);
        }

        public override IContext? GetContext(string path)
        {
            var parts = stringSplitter.Split(path);
            return GetContext(parts);
        }

        // ----------------- Основная логика -----------------

        public override IValue? GetValue(string[] parts)
        {
            if (parts.Length == 0)
                return null;

            var key = parts[0];

            if (!dictionary.Contains(key))
                return null;

            if (isPrimitive)
            {
                return parts.Length == 1
                    ? new DictionaryValue(dictionary, key, valueSchema.TargetType)
                    : null;
            }

            if (parts.Length == 1)
                return null;

            var itemContext = GetItemContext(key);
            return itemContext?.GetValue(parts.Skip(1).ToArray());
        }

        public override IContext? GetContext(string[] parts)
        {
            if (parts.Length == 0)
                return null;

            var key = parts[0];

            if (!dictionary.Contains(key))
                return null;

            if (isPrimitive)
                return null;

            var itemContext = GetItemContext(key);

            if (itemContext == null)
                return null;

            return parts.Length == 1
                ? itemContext
                : itemContext.GetContext(parts.Skip(1).ToArray());
        }

        // ----------------- Helpers -----------------

        public IContext? GetItemContext(string key)
        {
            if (isPrimitive || !dictionary.Contains(key))
                return null;

            var item = dictionary[key];
            return item != null ? registry.Get(item) : null;
        }

        // ----------------- Enumerable API -----------------

        public override IEnumerable<IContext> GetContexts()
        {
            if (isPrimitive)
                yield break;

            foreach (var item in dictionary.Values)
            {
                var ctx = registry.Get(item);
                if (ctx != null)
                    yield return ctx;
            }
        }

        public override IEnumerable<IValue> GetValues()
        {
            if (!isPrimitive)
                yield break;

            foreach (DictionaryEntry entry in dictionary)
            {
                yield return new DictionaryValue(dictionary, entry.Key.ToString()!, valueSchema.TargetType);
            }
        }

        // ----------------- Mutation -----------------

        public override void SetValue(string name, IValue value)
        {
            if (!isPrimitive)
                throw new InvalidOperationException("Cannot set value on complex type dictionary");

            dictionary[name] = value.Value;
        }

        public override void AttachChild(string name, ContextBase child)
            => throw new NotSupportedException("Use Dictionary API directly");

        public void AddItem(string key, object value)
            => dictionary[key] = value;

        public void RemoveItem(string key)
        {
            if (dictionary.Contains(key))
                dictionary.Remove(key);
        }

        public bool ContainsKey(string key) => dictionary.Contains(key);
    }
}
