using System.Collections;
using System.Linq.Expressions;
using GMutagenEngine.Infrastructure.Handlers.Sync.Funcs;

namespace GMutagenEngine.Utils;

public class TypeDiscoverySettings
{
    public BaseTypeOptions Options { get; set; } = BaseTypeOptions.All;
    public int TypeLimit { get; set; } = 16;
    public ISyncFuncHandler<Type, bool>? Filter { get; set; }
    public PruningRules Pruning { get; set; } = PruningRules.None;
}


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
                yield return underlying;
        }

        if (o.HasFlag(BaseTypeOptions.IncludeGenericDefinition))
        {
            if (type.IsGenericType)
            {
                var genericDef = type.GetGenericTypeDefinition();
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
                yield return type.BaseType;
        }
    }

    public void Reset() => throw new NotSupportedException();

    public void Dispose()
    {
        _queue.Clear();
    }
}

public class TypeDiscovery(Type rootType, TypeDiscoverySettings settings) : IEnumerable<Type>
{
    private readonly DefaultTypeEnumerator _enumerator = new(rootType, settings);

    public IEnumerator<Type> GetEnumerator()
    {
        var seen = new HashSet<Type>();
        var count = 0;
            
        seen.Add(rootType);

        if (settings.Options.HasFlag(BaseTypeOptions.IncludeSelf))
        {
            if (settings.Filter == null || settings.Filter.Handle(rootType))
            {
                yield return rootType;

                if (++count >= settings.TypeLimit)
                    yield break;
            }
        }

        while (_enumerator.MoveNext())
        {
            var t = _enumerator.Current;

            if (!seen.Add(t))
                continue;

            if (settings.Filter != null && !settings.Filter.Handle(t))
                continue;

            yield return t;

            if (++count >= settings.TypeLimit)
                yield break;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public static class TypeDiscoveryExtensions
{
    public static TypeDiscovery AsTypeDiscovery(this Type type)
        => new(type, new TypeDiscoverySettings());

    public static TypeDiscovery AsTypeDiscovery(
        this Type type,
        Func<TypeDiscoverySettings, TypeDiscoverySettings> configure)
        => new(type, configure(new TypeDiscoverySettings()));

    public static TypeDiscovery AsTypeDiscovery(
        this Type type,
        Action<TypeDiscoverySettings> configure)
    {
        var settings = new TypeDiscoverySettings();
        configure(settings);
        return new TypeDiscovery(type, settings);
    }

    public static TypeDiscovery AsTypeDiscovery(
        this Type type,
        TypeDiscoverySettings settings)
        => new(type, settings);
}

public interface IFilter<in T> : ISyncFuncHandler<T, bool>
{
    
}

public static class FilterExtensions
{
    public static ISyncFuncHandler<T, bool> And<T>(this ISyncFuncHandler<T, bool> first, ISyncFuncHandler<T, bool> second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return BuildComposite(first, second, Expression.AndAlso);
    }

    public static ISyncFuncHandler<T, bool> Or<T>(this ISyncFuncHandler<T, bool> first, ISyncFuncHandler<T, bool> second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return BuildComposite(first, second, Expression.OrElse);
    }

    public static ISyncFuncHandler<T, bool> Not<T>(this ISyncFuncHandler<T, bool> filter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        var param = Expression.Parameter(typeof(T), "x");
        var call = Expression.Invoke(Expression.Constant(filter.Handle), param);
        var notExpr = Expression.Not(call);
        var lambda = Expression.Lambda<Func<T, bool>>(notExpr, param).Compile();

        return new SyncFuncHandler<T, bool>(lambda);
    }

    private static ISyncFuncHandler<T, bool> BuildComposite<T>(ISyncFuncHandler<T, bool> left, ISyncFuncHandler<T, bool> right,
        Func<Expression, Expression, BinaryExpression> combiner)
    {
        var param = Expression.Parameter(typeof(T), "x");

        var leftCall = Expression.Invoke(Expression.Constant(left.Handle), param);
        var rightCall = Expression.Invoke(Expression.Constant(right.Handle), param);

        var combinedExpr = combiner(leftCall, rightCall);
        var lambda = Expression.Lambda<Func<T, bool>>(combinedExpr, param).Compile();

        return new SyncFuncHandler<T, bool>(lambda);
    }

    public static ISyncFuncHandler<bool> And(this ISyncFuncHandler<bool> first, ISyncFuncHandler<bool> second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return BuildComposite(first, second, Expression.AndAlso);
    }

    public static ISyncFuncHandler<bool> Or(this ISyncFuncHandler<bool> first, ISyncFuncHandler<bool> second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return BuildComposite(first, second, Expression.OrElse);
    }

    public static ISyncFuncHandler<bool> Not(this ISyncFuncHandler<bool> filter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));

        var call = Expression.Invoke(Expression.Constant(filter.Handle));
        var notExpr = Expression.Not(call);
        var lambda = Expression.Lambda<Func<bool>>(notExpr).Compile();

        return new SyncFuncHandler<bool>(lambda);
    }

    private static ISyncFuncHandler<bool> BuildComposite(ISyncFuncHandler<bool> left, ISyncFuncHandler<bool> right,
        Func<Expression, Expression, BinaryExpression> combiner)
    {
        var leftCall = Expression.Invoke(Expression.Constant(left.Handle));
        var rightCall = Expression.Invoke(Expression.Constant(right.Handle));

        var combinedExpr = combiner(leftCall, rightCall);
        var lambda = Expression.Lambda<Func<bool>>(combinedExpr).Compile();

        return new SyncFuncHandler<bool>(lambda);
    }
}