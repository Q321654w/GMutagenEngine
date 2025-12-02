using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.Naming;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations
{
    public class ContextSnapshot(IStringSplitter stringSplitter) : ContextBase
    {
        private readonly Dictionary<string, IValue> _values = new();
        private readonly Dictionary<string, IContext> _contexts = new();

        public IReadOnlyDictionary<string, IValue> Values => _values;
        public IReadOnlyDictionary<string, IContext> Contexts => _contexts;

        public ContextSnapshot() : this(new CharSplitter())
        {
        }

        public override IValue? GetValue(string[] parts)
        {
            return GetValueRecursive(parts, 0);
        }

        public override IContext? GetContext(string path)
        {
            var parts = stringSplitter.Split(path);
            return GetContext(parts);
        }

        public override IContext? GetContext(string[] parts)
        {
            return GetContextRecursive(parts, 0);
        }

        public override IValue? GetValue(string path)
        {
            var parts = stringSplitter.Split(path);
            return GetValue(parts);
        }


        public override void SetValue(string name, IValue value) => _values[name] = value;
        
        private IContext? GetContextRecursive(string[] parts, int index)
        {
            if (index == parts.Length - 1)
                return _contexts.TryGetValue(parts[index], out var v) ? v : null;

            return _contexts.TryGetValue(parts[index], out var ctx)
                ? ctx.GetContext(string.Join(ContextConstants.DELIMITER, parts.Skip(index + 1)))
                : null;
        }

        public override IEnumerable<IContext> GetContexts()
        {
            foreach (var context in _contexts)
                yield return context.Value;
        }

        public override IEnumerable<IValue> GetValues()
        {
            foreach (var value in _values)
                yield return value.Value;
        }

        public override void AttachChild(string name, ContextBase context) => _contexts[name] = context;

        public override bool ExistContext(string path) => GetContext(path) != null;

        private IValue? GetValueRecursive(string[] parts, int index)
        {
            if (index >= parts.Length)
                return null;

            if (index == parts.Length - 1)
                return _values.TryGetValue(parts[index], out var v) ? v : null;

            return _contexts.TryGetValue(parts[index], out var ctx)
                ? ctx.GetValue(string.Join(ContextConstants.DELIMITER, parts.Skip(index + 1)))
                : null;
        }


        public void Merge(ContextSnapshot other)
        {
            foreach (var kv in other._values)
                _values[kv.Key] = kv.Value;

            foreach (var kv in other._contexts)
            {
                if (_contexts.TryGetValue(kv.Key, out var existing))
                {
                    if (existing is ContextSnapshot contextBase)
                        contextBase.Merge(kv.Value as ContextSnapshot ?? new ContextSnapshot());
                }
                else
                    _contexts[kv.Key] = kv.Value;
            }
        }

        public override bool ExistValue(string path) => GetValue(path) != null;
    }
}