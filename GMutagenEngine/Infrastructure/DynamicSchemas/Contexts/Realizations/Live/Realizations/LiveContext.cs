using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Descriptors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.Naming;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations
{
    public class LiveContext : ContextBase
    {
        private readonly object _instance;
        private readonly ISchema _schema;
        private readonly IContextRegistry _registry;
        private readonly IStringSplitter _stringSplitter;

        private readonly Dictionary<string, IValue> _values = new();
        private readonly Dictionary<string, IContextDescriptor> _descriptors = new();

        public object Instance => _instance;
        public ISchema Schema => _schema;

        public LiveContext(object instance, ISchema schema, IContextRegistry registry,
            IStringSplitter stringSplitter)
        {
            _instance = instance;
            _schema = schema;
            _registry = registry;
            _stringSplitter = stringSplitter;
        }

        public LiveContext(object instance, ISchema schema, IContextRegistry registry)
            : this(instance, schema, registry, new SplitterByChars())
        {
        }

        public override IValue? GetValue(string path)
        {
            var parts = _stringSplitter.Split(path);
            return GetValueRecursive(parts, 0);
        }

        public override IValue? GetValue(string[] parts) => GetValueRecursive(parts, 0);

        private IValue? GetValueRecursive(string[] parts, int index)
        {
            if (index == parts.Length - 1)
                return _values.TryGetValue(parts[index], out var v) ? v : null;

            if (_descriptors.TryGetValue(parts[index], out var descriptor))
            {
                var childContext = descriptor.ResolveContext(_registry);
                if (childContext != null)
                {
                    var remainingPath = parts.Skip(index + 1).ToArray();
                    return childContext.GetValue(remainingPath);
                }
            }

            return null;
        }

        public override IContext? GetContext(string path)
        {
            var parts = _stringSplitter.Split(path);
            return GetContextRecursive(parts, 0);
        }

        public override IContext? GetContext(string[] parts) => GetContextRecursive(parts, 0);

        private IContext? GetContextRecursive(string[] parts, int index)
        {
            if (index == parts.Length - 1)
                return _descriptors.TryGetValue(parts[index], out var v) ? v.ResolveContext(_registry) : null;

            if (_descriptors.TryGetValue(parts[index], out var descriptor))
            {
                var childContext = descriptor.ResolveContext(_registry);
                if (childContext != null)
                {
                    var remainingPath = parts.Skip(index + 1).ToArray();
                    return childContext.GetContext(remainingPath);
                }
            }

            return null;
        }

        public override IEnumerable<IContext> GetContexts()
        {
            foreach (var context in _descriptors.Values.Select(d => d.ResolveContext(_registry)))
                yield return context;
        }

        public override IEnumerable<IValue> GetValues()
        {
            foreach (var value in _values.Values)
                yield return value;
        }

        public void AddDescriptor(string name, IContextDescriptor descriptor)
        {
            _descriptors[name] = descriptor;
        }

        public override void AttachChild(string name, ContextBase child)
        {
            throw new NotSupportedException("Use AddDescriptor for live contexts");
        }

        public override bool ExistContext(string path) => GetContext(path) != null;

        public override bool ExistValue(string path) => GetValue(path) != null;

        public override void SetValue(string name, IValue value) => _values[name] = value;
    }
}