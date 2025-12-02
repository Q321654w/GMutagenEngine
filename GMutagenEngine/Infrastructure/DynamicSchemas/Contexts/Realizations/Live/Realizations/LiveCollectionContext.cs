using System.Collections;
using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.Naming;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations
{
namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations
{
    public class LiveCollectionContext(
        IList list,
        ISchema elementSchema,
        IContextRegistry registry,
        bool isPrimitive,
        IStringSplitter stringSplitter)
        : ContextBase
    {
        public IList List => list;
        public ISchema ElementSchema => elementSchema;

        public LiveCollectionContext(
            IList list,
            ISchema elementSchema,
            IContextRegistry registry,
            bool isPrimitive)
            : this(list, elementSchema, registry, isPrimitive, new SplitterByChars())
        {
        }

        // ----------------- string → string[] → overload -----------------

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

        // ----------------- Основная оптимизированная логика -----------------

        public override IValue? GetValue(string[] parts)
        {
            if (parts.Length == 0)
                return null;

            if (!TryGetIndex(parts[0], out var index))
                return null;

            if (isPrimitive)
            {
                // Доступ только напрямую по индексу
                return parts.Length == 1
                    ? new ListValue(list, index, elementSchema.TargetType)
                    : null;
            }

            if (parts.Length == 1)
                return null;

            var itemContext = GetItemContext(index);
            return itemContext?.GetValue(parts.Skip(1).ToArray());
        }

        public override IContext? GetContext(string[] parts)
        {
            if (parts.Length == 0)
                return null;

            if (!TryGetIndex(parts[0], out var index))
                return null;

            if (isPrimitive)
                return null;

            var itemContext = GetItemContext(index);
            if (itemContext == null)
                return null;

            return parts.Length == 1
                ? itemContext
                : itemContext.GetContext(parts.Skip(1).ToArray());
        }

        // ----------------- Helpers -----------------

        private bool TryGetIndex(string part, out int index)
        {
            index = -1;
            return int.TryParse(part, out index)
                   && index >= 0
                   && index < list.Count;
        }

        public IContext? GetItemContext(int index)
        {
            if (isPrimitive || index < 0 || index >= list.Count)
                return null;

            var item = list[index];
            return item != null ? registry.Get(item) : null;
        }

        // ----------------- Enumerable API -----------------

        public override IEnumerable<IContext> GetContexts()
        {
            if (isPrimitive)
                yield break;

            foreach (var item in list)
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

            for (var i = 0; i < list.Count; i++)
                yield return new ListValue(list, i, elementSchema.TargetType);
        }

        // ----------------- Mutation -----------------

        public override void SetValue(string name, IValue value)
        {
            if (!isPrimitive)
                throw new InvalidOperationException("Cannot set value on complex type collection");

            if (!int.TryParse(name, out var index) || index < 0)
                return;

            if (index >= list.Count)
            {
                while (list.Count <= index)
                    list.Add(GetDefaultValue(elementSchema.TargetType));
            }

            list[index] = value.Value;
        }

        public override void AttachChild(string name, ContextBase child)
            => throw new NotSupportedException("Use List API directly");

        public void AddItem(object item)
            => list.Add(item);

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < list.Count)
                list.RemoveAt(index);
        }

        public int Count => list.Count;

        private object? GetDefaultValue(Type? type)
        {
            if (type == null)
                return null;

            return type.IsValueType
                ? Activator.CreateInstance(type)
                : null;
        }
    }
}
}