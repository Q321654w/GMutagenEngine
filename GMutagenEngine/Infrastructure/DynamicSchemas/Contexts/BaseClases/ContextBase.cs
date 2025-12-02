using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases
{
    public abstract class ContextBase : IContext
    {
        public abstract bool ExistValue(string path);
        public abstract bool ExistContext(string path);
        public abstract IValue? GetValue(string path);
        public abstract IValue? GetValue(string[] parts);
        public abstract void SetValue(string name, IValue value);
        public abstract IContext? GetContext(string path);
        public abstract IContext? GetContext(string[] parts);
        public abstract IEnumerable<IContext> GetContexts();

        public abstract IEnumerable<IValue> GetValues();

        public abstract void AttachChild(string name, ContextBase child);

        public IEnumerable<IContext> GetAllContexts()
        {
            using var selfContextEnumerator = GetContexts().GetEnumerator();
            while (selfContextEnumerator.MoveNext())
            {
                var context = selfContextEnumerator.Current;
                yield return context;

                using var enumerator = context.GetAllContexts().GetEnumerator();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        public IEnumerable<IValue> GetAllValues()
        {
            using var selfValueEnumerator = GetValues().GetEnumerator();
            while (selfValueEnumerator.MoveNext())
            {
                yield return selfValueEnumerator.Current;
            }

            using var selfContextEnumerator = GetContexts().GetEnumerator();
            while (selfContextEnumerator.MoveNext())
            {
                var context = selfContextEnumerator.Current;
                using var enumerable = context.GetAllValues().GetEnumerator();
                while (enumerable.MoveNext())
                    yield return enumerable.Current;
            }
        }
    }
}