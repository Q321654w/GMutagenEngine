using System.Collections;

namespace Utils;

public class DefaultTypeEnumerator : IEnumerator<Type>
{
    private readonly Queue<Type> _queue = new();
    private readonly TypeDiscoverySettings _settings;
    private readonly Type _rootType;

    public DefaultTypeEnumerator(Type rootType, TypeDiscoverySettings settings)
    {
        _settings = settings;
        _rootType = rootType;
        _queue.Enqueue(rootType);
    }

    public Type Current { get; private set; } = default!;
    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        while (_queue.Count > 0)
        {
            var t = _queue.Dequeue();

            foreach (var next in EnumerateChildren(t))
                _queue.Enqueue(next);

            Current = t;
            return true;
        }

        return false;
    }

    private IEnumerable<Type> EnumerateChildren(Type type)
    {
        var o = _settings.Options;
        var p = _settings.Pruning;
        var filter = _settings.Filter;

        var passedFilter = filter == null || filter.Handle(type);

        if (!passedFilter)
        {
            if (p.HasFlag(PruningRules.PruneNullable))
            {
                o &= ~BaseTypeOptions.IncludeNullableUnderlying;
            }
            
            if (p.HasFlag(PruningRules.PruneAbstract))
            {
                o &= ~BaseTypeOptions.IncludeAbstract;
            }

            if (p.HasFlag(PruningRules.PruneGenericDefinition))
            {
                o &= ~BaseTypeOptions.IncludeGenericDefinition;
            }

            if (p.HasFlag(PruningRules.PruneInterfaces))
            {
                o &= ~BaseTypeOptions.IncludeInterfaces;
            }

            if (p.HasFlag(PruningRules.PruneBaseTypes))
            {
                o &= ~BaseTypeOptions.IncludeBaseTypes;
            }
        }

        if (o.HasFlag(BaseTypeOptions.IncludeNullableUnderlying))
        {
            var underlying = Nullable.GetUnderlyingType(type);
            if (underlying != null)
                if((o.HasFlag(BaseTypeOptions.IncludeAbstract) && underlying.IsAbstract) || !o.HasFlag(BaseTypeOptions.IncludeAbstract)) 
                    yield return underlying;
        }

        if (o.HasFlag(BaseTypeOptions.IncludeGenericDefinition))
        {
            if (type.IsGenericType)
            {
                var genericDef = type.GetGenericTypeDefinition();
                if((o.HasFlag(BaseTypeOptions.IncludeAbstract) && genericDef.IsAbstract) || !o.HasFlag(BaseTypeOptions.IncludeAbstract))
                    yield return genericDef;
            }
        }

        if (o.HasFlag(BaseTypeOptions.IncludeInterfaces))
        {
            var interfaces = type.GetInterfaces();
            foreach (var i in interfaces)
                yield return i;
        }

        if (o.HasFlag(BaseTypeOptions.IncludeBaseTypes))
        {
            if (type.BaseType != null)
                if((o.HasFlag(BaseTypeOptions.IncludeAbstract) && type.BaseType.IsAbstract) || o.HasFlag(BaseTypeOptions.IncludeAbstract))
                    yield return type.BaseType;
        }
    }

    public void Reset() => throw new NotSupportedException();

    public void Dispose()
    {
        _queue.Clear();
    }
}