using System.Linq.Expressions;
using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace Filters.Sync.Api;

public interface ICompiledSyncHandler<in TIn, out TOut> : ICompiledSyncHandlerMark {
    Func<TIn, TOut> GetCompiledFunc();
}

public interface ICompiledSyncHandler<out TOut> : ICompiledSyncHandlerMark {
    Func<TOut> GetCompiledFunc();
}

public static class FilterExtensions
{
    public static ISyncFuncHandler<T, bool> And<T>(
        this ISyncFuncHandler<T, bool> left,
        ISyncFuncHandler<T, bool> right)
        => CombineAnd(left, right);

    public static ISyncFuncHandler<T, bool> And<T>(
        this ISyncFuncHandler<T, bool> left,
        Func<T, bool> right)
        => CombineAnd(left, right);

    public static ISyncFuncHandler<T, bool> And<T>(
        this Func<T, bool> left,
        ISyncFuncHandler<T, bool> right)
        => CombineAnd(left, right);

    public static ISyncFuncHandler<T, bool> And<T>(
        this Func<T, bool> left,
        Func<T, bool> right)
        => new AndSyncHandler<T>(new[] { left, right });

    public static ISyncFuncHandler<T, bool> Or<T>(
        this ISyncFuncHandler<T, bool> left,
        ISyncFuncHandler<T, bool> right)
        => CombineOr(left, right);

    public static ISyncFuncHandler<T, bool> Or<T>(
        this ISyncFuncHandler<T, bool> left,
        Func<T, bool> right)
        => CombineOr(left, right);

    public static ISyncFuncHandler<T, bool> Or<T>(
        this Func<T, bool> left,
        ISyncFuncHandler<T, bool> right)
        => CombineOr(left, right);

    public static ISyncFuncHandler<T, bool> Or<T>(
        this Func<T, bool> left,
        Func<T, bool> right)
        => new OrSyncHandler<T>(new[] { left, right });

    public static ISyncFuncHandler<T, bool> Not<T>(
        this ISyncFuncHandler<T, bool> filter)
        => new NotSyncHandler<T>(filter);

    public static ISyncFuncHandler<T, bool> Not<T>(
        this Func<T, bool> filter)
        => new NotSyncHandler<T>(filter);

    public static ISyncFuncHandler<bool> And(
        this ISyncFuncHandler<bool> left,
        ISyncFuncHandler<bool> right)
        => CombineAnd(left, right);

    public static ISyncFuncHandler<bool> And(
        this ISyncFuncHandler<bool> left,
        Func<bool> right)
        => CombineAnd(left, right);

    public static ISyncFuncHandler<bool> And(
        this Func<bool> left,
        ISyncFuncHandler<bool> right)
        => CombineAnd(left, right);

    public static ISyncFuncHandler<bool> And(
        this Func<bool> left,
        Func<bool> right)
        => new AndSyncHandler(new[] { left, right });

    public static ISyncFuncHandler<bool> Or(
        this ISyncFuncHandler<bool> left,
        ISyncFuncHandler<bool> right)
        => CombineOr(left, right);

    public static ISyncFuncHandler<bool> Or(
        this ISyncFuncHandler<bool> left,
        Func<bool> right)
        => CombineOr(left, right);

    public static ISyncFuncHandler<bool> Or(
        this Func<bool> left,
        ISyncFuncHandler<bool> right)
        => CombineOr(left, right);

    public static ISyncFuncHandler<bool> Or(
        this Func<bool> left,
        Func<bool> right)
        => new OrSyncHandler(new[] { left, right });

    public static ISyncFuncHandler<bool> Not(
        this ISyncFuncHandler<bool> filter)
        => new NotSyncHandler(filter);

    public static ISyncFuncHandler<bool> Not(
        this Func<bool> filter)
        => new NotSyncHandler(filter);

    // =========================
    // COMBINE LOGIC
    // =========================

    private static ISyncFuncHandler<T, bool> CombineAnd<T>(
        ISyncFuncHandler<T, bool> left,
        ISyncFuncHandler<T, bool> right)
    {
        var predicates = new List<Func<T, bool>>();

        if (left is AndSyncHandler<T> leftAnd)
            predicates.AddRange(leftAnd.GetPredicates());
        else if (left is ICompiledSyncHandler<T, bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(data => left.Handle(data));

        if (right is AndSyncHandler<T> rightAnd)
            predicates.AddRange(rightAnd.GetPredicates());
        else if (right is ICompiledSyncHandler<T, bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(data => right.Handle(data));

        return new AndSyncHandler<T>(predicates);
    }

    private static ISyncFuncHandler<T, bool> CombineAnd<T>(
        ISyncFuncHandler<T, bool> left,
        Func<T, bool> right)
    {
        var predicates = new List<Func<T, bool>>();

        if (left is AndSyncHandler<T> leftAnd)
            predicates.AddRange(leftAnd.GetPredicates());
        else if (left is ICompiledSyncHandler<T, bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(data => left.Handle(data));

        predicates.Add(right);
        return new AndSyncHandler<T>(predicates);
    }

    private static ISyncFuncHandler<T, bool> CombineAnd<T>(
        Func<T, bool> left,
        ISyncFuncHandler<T, bool> right)
    {
        var predicates = new List<Func<T, bool>> { left };

        if (right is AndSyncHandler<T> rightAnd)
            predicates.AddRange(rightAnd.GetPredicates());
        else if (right is ICompiledSyncHandler<T, bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(data => right.Handle(data));

        return new AndSyncHandler<T>(predicates);
    }

    private static ISyncFuncHandler<T, bool> CombineOr<T>(
        ISyncFuncHandler<T, bool> left,
        ISyncFuncHandler<T, bool> right)
    {
        var predicates = new List<Func<T, bool>>();

        if (left is OrSyncHandler<T> leftOr)
            predicates.AddRange(leftOr.GetPredicates());
        else if (left is ICompiledSyncHandler<T, bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(data => left.Handle(data));

        if (right is OrSyncHandler<T> rightOr)
            predicates.AddRange(rightOr.GetPredicates());
        else if (right is ICompiledSyncHandler<T, bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(data => right.Handle(data));

        return new OrSyncHandler<T>(predicates);
    }

    private static ISyncFuncHandler<T, bool> CombineOr<T>(
        ISyncFuncHandler<T, bool> left,
        Func<T, bool> right)
    {
        var predicates = new List<Func<T, bool>>();

        if (left is OrSyncHandler<T> leftOr)
            predicates.AddRange(leftOr.GetPredicates());
        else if (left is ICompiledSyncHandler<T, bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(data => left.Handle(data));

        predicates.Add(right);
        return new OrSyncHandler<T>(predicates);
    }

    private static ISyncFuncHandler<T, bool> CombineOr<T>(
        Func<T, bool> left,
        ISyncFuncHandler<T, bool> right)
    {
        var predicates = new List<Func<T, bool>> { left };

        if (right is OrSyncHandler<T> rightOr)
            predicates.AddRange(rightOr.GetPredicates());
        else if (right is ICompiledSyncHandler<T, bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(data => right.Handle(data));

        return new OrSyncHandler<T>(predicates);
    }

    private static ISyncFuncHandler<bool> CombineAnd(
        ISyncFuncHandler<bool> left,
        ISyncFuncHandler<bool> right)
    {
        var predicates = new List<Func<bool>>();

        if (left is AndSyncHandler leftAnd)
            predicates.AddRange(leftAnd.GetPredicates());
        else if (left is ICompiledSyncHandler<bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(() => left.Handle());

        if (right is AndSyncHandler rightAnd)
            predicates.AddRange(rightAnd.GetPredicates());
        else if (right is ICompiledSyncHandler<bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(() => right.Handle());

        return new AndSyncHandler(predicates);
    }

    private static ISyncFuncHandler<bool> CombineAnd(
        ISyncFuncHandler<bool> left,
        Func<bool> right)
    {
        var predicates = new List<Func<bool>>();

        if (left is AndSyncHandler leftAnd)
            predicates.AddRange(leftAnd.GetPredicates());
        else if (left is ICompiledSyncHandler<bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(() => left.Handle());

        predicates.Add(right);
        return new AndSyncHandler(predicates);
    }

    private static ISyncFuncHandler<bool> CombineAnd(
        Func<bool> left,
        ISyncFuncHandler<bool> right)
    {
        var predicates = new List<Func<bool>> { left };

        if (right is AndSyncHandler rightAnd)
            predicates.AddRange(rightAnd.GetPredicates());
        else if (right is ICompiledSyncHandler<bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(() => right.Handle());

        return new AndSyncHandler(predicates);
    }

    private static ISyncFuncHandler<bool> CombineOr(
        ISyncFuncHandler<bool> left,
        ISyncFuncHandler<bool> right)
    {
        var predicates = new List<Func<bool>>();

        if (left is OrSyncHandler leftOr)
            predicates.AddRange(leftOr.GetPredicates());
        else if (left is ICompiledSyncHandler<bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(() => left.Handle());

        if (right is OrSyncHandler rightOr)
            predicates.AddRange(rightOr.GetPredicates());
        else if (right is ICompiledSyncHandler<bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(() => right.Handle());

        return new OrSyncHandler(predicates);
    }

    private static ISyncFuncHandler<bool> CombineOr(
        ISyncFuncHandler<bool> left,
        Func<bool> right)
    {
        var predicates = new List<Func<bool>>();

        if (left is OrSyncHandler leftOr)
            predicates.AddRange(leftOr.GetPredicates());
        else if (left is ICompiledSyncHandler<bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(() => left.Handle());

        predicates.Add(right);
        return new OrSyncHandler(predicates);
    }

    private static ISyncFuncHandler<bool> CombineOr(
        Func<bool> left,
        ISyncFuncHandler<bool> right)
    {
        var predicates = new List<Func<bool>> { left };

        if (right is OrSyncHandler rightOr)
            predicates.AddRange(rightOr.GetPredicates());
        else if (right is ICompiledSyncHandler<bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(() => right.Handle());

        return new OrSyncHandler(predicates);
    }

    // =========================
    // SPECIALIZED HANDLERS
    // =========================

    private sealed class AndSyncHandler<T> : ISyncFuncHandler<T, bool>, ICompiledSyncHandler<T, bool>
    {
        private readonly Func<T, bool>[] _predicates;
        private readonly Func<T, bool> _compiled;

        public AndSyncHandler(IEnumerable<Func<T, bool>> predicates)
        {
            _predicates = predicates.ToArray();
            _compiled = BuildExpression();
        }

        public bool Handle(T data) => _compiled(data);

        public Func<T, bool> GetCompiledFunc() => _compiled;

        public IEnumerable<Func<T, bool>> GetPredicates() => _predicates;

        private Func<T, bool> BuildExpression()
        {
            if (_predicates.Length == 0) return _ => true;
            if (_predicates.Length == 1) return _predicates[0];

            var param = Expression.Parameter(typeof(T), "x");
            Expression body = null;

            foreach (var predicate in _predicates)
            {
                var call = Expression.Invoke(Expression.Constant(predicate), param);
                body = body == null ? call : Expression.AndAlso(body, call);
            }

            return Expression.Lambda<Func<T, bool>>(body!, param).Compile();
        }
    }

    private sealed class OrSyncHandler<T> : ISyncFuncHandler<T, bool>, ICompiledSyncHandler<T, bool>
    {
        private readonly Func<T, bool>[] _predicates;
        private readonly Func<T, bool> _compiled;

        public OrSyncHandler(IEnumerable<Func<T, bool>> predicates)
        {
            _predicates = predicates.ToArray();
            _compiled = BuildExpression();
        }

        public bool Handle(T data) => _compiled(data);

        public Func<T, bool> GetCompiledFunc() => _compiled;

        public IEnumerable<Func<T, bool>> GetPredicates() => _predicates;

        private Func<T, bool> BuildExpression()
        {
            if (_predicates.Length == 0) return _ => false;
            if (_predicates.Length == 1) return _predicates[0];

            var param = Expression.Parameter(typeof(T), "x");
            Expression body = null;

            foreach (var predicate in _predicates)
            {
                var call = Expression.Invoke(Expression.Constant(predicate), param);
                body = body == null ? call : Expression.OrElse(body, call);
            }

            return Expression.Lambda<Func<T, bool>>(body!, param).Compile();
        }
    }

    private sealed class AndSyncHandler : ISyncFuncHandler<bool>, ICompiledSyncHandler<bool>
    {
        private readonly Func<bool>[] _predicates;
        private readonly Func<bool> _compiled;

        public AndSyncHandler(IEnumerable<Func<bool>> predicates)
        {
            _predicates = predicates.ToArray();
            _compiled = BuildExpression();
        }

        public bool Handle() => _compiled();

        public Func<bool> GetCompiledFunc() => _compiled;

        public IEnumerable<Func<bool>> GetPredicates() => _predicates;

        private Func<bool> BuildExpression()
        {
            if (_predicates.Length == 0) return () => true;
            if (_predicates.Length == 1) return _predicates[0];

            Expression body = null;

            foreach (var predicate in _predicates)
            {
                var call = Expression.Invoke(Expression.Constant(predicate));
                body = body == null ? call : Expression.AndAlso(body, call);
            }

            return Expression.Lambda<Func<bool>>(body!).Compile();
        }
    }

    private sealed class OrSyncHandler : ISyncFuncHandler<bool>, ICompiledSyncHandler<bool>
    {
        private readonly Func<bool>[] _predicates;
        private readonly Func<bool> _compiled;

        public OrSyncHandler(IEnumerable<Func<bool>> predicates)
        {
            _predicates = predicates.ToArray();
            _compiled = BuildExpression();
        }

        public bool Handle() => _compiled();

        public Func<bool> GetCompiledFunc() => _compiled;

        public IEnumerable<Func<bool>> GetPredicates() => _predicates;

        private Func<bool> BuildExpression()
        {
            if (_predicates.Length == 0) return () => false;
            if (_predicates.Length == 1) return _predicates[0];

            Expression body = null;

            foreach (var predicate in _predicates)
            {
                var call = Expression.Invoke(Expression.Constant(predicate));
                body = body == null ? call : Expression.AndAlso(body, call);
            }

            return Expression.Lambda<Func<bool>>(body!).Compile();
        }
    }

    private sealed class NotSyncHandler<T> : ISyncFuncHandler<T, bool>, ICompiledSyncHandler<T, bool>
    {
        private readonly Func<T, bool> _compiled;

        public NotSyncHandler(ISyncFuncHandler<T, bool> inner)
        {
            // Пытаемся получить скомпилированную версию
            if (inner is ICompiledSyncHandler<T, bool> compiled)
            {
                var func = compiled.GetCompiledFunc();
                var param = Expression.Parameter(typeof(T), "x");
                var call = Expression.Invoke(Expression.Constant(func), param);
                var body = Expression.Not(call);
                _compiled = Expression.Lambda<Func<T, bool>>(body, param).Compile();
            }
            else
            {
                var param = Expression.Parameter(typeof(T), "x");
                var call = Expression.Call(
                    Expression.Constant(inner),
                    typeof(ISyncFuncHandler<T, bool>).GetMethod(nameof(Handle))!,
                    param);
                var body = Expression.Not(call);
                _compiled = Expression.Lambda<Func<T, bool>>(body, param).Compile();
            }
        }

        public NotSyncHandler(Func<T, bool> inner)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var call = Expression.Invoke(Expression.Constant(inner), param);
            var body = Expression.Not(call);
            _compiled = Expression.Lambda<Func<T, bool>>(body, param).Compile();
        }

        public bool Handle(T data) => _compiled(data);

        public Func<T, bool> GetCompiledFunc() => _compiled;
    }

    private sealed class NotSyncHandler : ISyncFuncHandler<bool>, ICompiledSyncHandler<bool>
    {
        private readonly Func<bool> _compiled;

        public NotSyncHandler(ISyncFuncHandler<bool> inner)
        {
            if (inner is ICompiledSyncHandler<bool> compiled)
            {
                var func = compiled.GetCompiledFunc();
                var call = Expression.Invoke(Expression.Constant(func));
                var body = Expression.Not(call);
                _compiled = Expression.Lambda<Func<bool>>(body).Compile();
            }
            else
            {
                var call = Expression.Call(
                    Expression.Constant(inner),
                    typeof(ISyncFuncHandler<bool>).GetMethod(nameof(Handle))!);
                var body = Expression.Not(call);
                _compiled = Expression.Lambda<Func<bool>>(body).Compile();
            }
        }

        public NotSyncHandler(Func<bool> inner)
        {
            var call = Expression.Invoke(Expression.Constant(inner));
            var body = Expression.Not(call);
            _compiled = Expression.Lambda<Func<bool>>(body).Compile();
        }

        public bool Handle() => _compiled();

        public Func<bool> GetCompiledFunc() => _compiled;
    }
}
public interface ICompiledSyncHandlerMark : ISelfMark<ICompiledSyncHandlerMark> {
}
