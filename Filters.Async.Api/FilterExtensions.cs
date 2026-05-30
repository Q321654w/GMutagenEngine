using GMutagenEngine.Handlers.Async.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace Filters.Async.Api;

public interface ICompiledAsyncHandler<in TIn, TOut> : ICompiledAsyncHandlerMark {
    Func<TIn, CancellationToken, Task<TOut>> GetCompiledFunc();
}

public interface ICompiledAsyncHandler<TOut> : ICompiledAsyncHandlerMark {
    Func<CancellationToken, Task<TOut>> GetCompiledFunc();
}

public static class FilterExtensions
{
    public static IAsyncFuncHandler<T, bool> And<T>(
        this IAsyncFuncHandler<T, bool> left,
        IAsyncFuncHandler<T, bool> right)
        => CombineAnd(left, right);

    public static IAsyncFuncHandler<T, bool> And<T>(
        this IAsyncFuncHandler<T, bool> left,
        Func<T, CancellationToken, Task<bool>> right)
        => CombineAnd(left, right);

    public static IAsyncFuncHandler<T, bool> And<T>(
        this Func<T, CancellationToken, Task<bool>> left,
        IAsyncFuncHandler<T, bool> right)
        => CombineAnd(left, right);

    public static IAsyncFuncHandler<T, bool> And<T>(
        this Func<T, CancellationToken, Task<bool>> left,
        Func<T, CancellationToken, Task<bool>> right)
        => new AndAsyncHandler<T>(new[] { left, right });

    public static IAsyncFuncHandler<T, bool> Or<T>(
        this IAsyncFuncHandler<T, bool> left,
        IAsyncFuncHandler<T, bool> right)
        => CombineOr(left, right);

    public static IAsyncFuncHandler<T, bool> Or<T>(
        this IAsyncFuncHandler<T, bool> left,
        Func<T, CancellationToken, Task<bool>> right)
        => CombineOr(left, right);

    public static IAsyncFuncHandler<T, bool> Or<T>(
        this Func<T, CancellationToken, Task<bool>> left,
        IAsyncFuncHandler<T, bool> right)
        => CombineOr(left, right);

    public static IAsyncFuncHandler<T, bool> Or<T>(
        this Func<T, CancellationToken, Task<bool>> left,
        Func<T, CancellationToken, Task<bool>> right)
        => new OrAsyncHandler<T>(new[] { left, right });

    public static IAsyncFuncHandler<T, bool> Not<T>(
        this IAsyncFuncHandler<T, bool> filter)
        => new NotAsyncHandler<T>(filter);

    public static IAsyncFuncHandler<T, bool> Not<T>(
        this Func<T, CancellationToken, Task<bool>> filter)
        => new NotAsyncHandler<T>(filter);

    public static IAsyncFuncHandler<bool> And(
        this IAsyncFuncHandler<bool> left,
        IAsyncFuncHandler<bool> right)
        => CombineAnd(left, right);

    public static IAsyncFuncHandler<bool> And(
        this IAsyncFuncHandler<bool> left,
        Func<CancellationToken, Task<bool>> right)
        => CombineAnd(left, right);

    public static IAsyncFuncHandler<bool> And(
        this Func<CancellationToken, Task<bool>> left,
        IAsyncFuncHandler<bool> right)
        => CombineAnd(left, right);

    public static IAsyncFuncHandler<bool> And(
        this Func<CancellationToken, Task<bool>> left,
        Func<CancellationToken, Task<bool>> right)
        => new AndAsyncHandler(new[] { left, right });

    public static IAsyncFuncHandler<bool> Or(
        this IAsyncFuncHandler<bool> left,
        IAsyncFuncHandler<bool> right)
        => CombineOr(left, right);

    public static IAsyncFuncHandler<bool> Or(
        this IAsyncFuncHandler<bool> left,
        Func<CancellationToken, Task<bool>> right)
        => CombineOr(left, right);

    public static IAsyncFuncHandler<bool> Or(
        this Func<CancellationToken, Task<bool>> left,
        IAsyncFuncHandler<bool> right)
        => CombineOr(left, right);

    public static IAsyncFuncHandler<bool> Or(
        this Func<CancellationToken, Task<bool>> left,
        Func<CancellationToken, Task<bool>> right)
        => new OrAsyncHandler(new[] { left, right });

    public static IAsyncFuncHandler<bool> Not(
        this IAsyncFuncHandler<bool> filter)
        => new NotAsyncHandler(filter);

    public static IAsyncFuncHandler<bool> Not(
        this Func<CancellationToken, Task<bool>> filter)
        => new NotAsyncHandler(filter);

    private static IAsyncFuncHandler<T, bool> CombineAnd<T>(
        IAsyncFuncHandler<T, bool> left,
        IAsyncFuncHandler<T, bool> right)
    {
        var predicates = new List<Func<T, CancellationToken, Task<bool>>>();

        if (left is AndAsyncHandler<T> leftAnd)
            predicates.AddRange(leftAnd.GetPredicates());
        else if (left is ICompiledAsyncHandler<T, bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add((data, ct) => left.Handle(data, ct));

        if (right is AndAsyncHandler<T> rightAnd)
            predicates.AddRange(rightAnd.GetPredicates());
        else if (right is ICompiledAsyncHandler<T, bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add((data, ct) => right.Handle(data, ct));

        return new AndAsyncHandler<T>(predicates);
    }

    private static IAsyncFuncHandler<T, bool> CombineAnd<T>(
        IAsyncFuncHandler<T, bool> left,
        Func<T, CancellationToken, Task<bool>> right)
    {
        var predicates = new List<Func<T, CancellationToken, Task<bool>>>();

        if (left is AndAsyncHandler<T> leftAnd)
            predicates.AddRange(leftAnd.GetPredicates());
        else if (left is ICompiledAsyncHandler<T, bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add((data, ct) => left.Handle(data, ct));

        predicates.Add(right);
        return new AndAsyncHandler<T>(predicates);
    }

    private static IAsyncFuncHandler<T, bool> CombineAnd<T>(
        Func<T, CancellationToken, Task<bool>> left,
        IAsyncFuncHandler<T, bool> right)
    {
        var predicates = new List<Func<T, CancellationToken, Task<bool>>> { left };

        if (right is AndAsyncHandler<T> rightAnd)
            predicates.AddRange(rightAnd.GetPredicates());
        else if (right is ICompiledAsyncHandler<T, bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add((data, ct) => right.Handle(data, ct));

        return new AndAsyncHandler<T>(predicates);
    }

    private static IAsyncFuncHandler<T, bool> CombineOr<T>(
        IAsyncFuncHandler<T, bool> left,
        IAsyncFuncHandler<T, bool> right)
    {
        var predicates = new List<Func<T, CancellationToken, Task<bool>>>();

        if (left is OrAsyncHandler<T> leftOr)
            predicates.AddRange(leftOr.GetPredicates());
        else if (left is ICompiledAsyncHandler<T, bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add((data, ct) => left.Handle(data, ct));

        if (right is OrAsyncHandler<T> rightOr)
            predicates.AddRange(rightOr.GetPredicates());
        else if (right is ICompiledAsyncHandler<T, bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add((data, ct) => right.Handle(data, ct));

        return new OrAsyncHandler<T>(predicates);
    }

    private static IAsyncFuncHandler<T, bool> CombineOr<T>(
        IAsyncFuncHandler<T, bool> left,
        Func<T, CancellationToken, Task<bool>> right)
    {
        var predicates = new List<Func<T, CancellationToken, Task<bool>>>();

        if (left is OrAsyncHandler<T> leftOr)
            predicates.AddRange(leftOr.GetPredicates());
        else if (left is ICompiledAsyncHandler<T, bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add((data, ct) => left.Handle(data, ct));

        predicates.Add(right);
        return new OrAsyncHandler<T>(predicates);
    }

    private static IAsyncFuncHandler<T, bool> CombineOr<T>(
        Func<T, CancellationToken, Task<bool>> left,
        IAsyncFuncHandler<T, bool> right)
    {
        var predicates = new List<Func<T, CancellationToken, Task<bool>>> { left };

        if (right is OrAsyncHandler<T> rightOr)
            predicates.AddRange(rightOr.GetPredicates());
        else if (right is ICompiledAsyncHandler<T, bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add((data, ct) => right.Handle(data, ct));

        return new OrAsyncHandler<T>(predicates);
    }

    private static IAsyncFuncHandler<bool> CombineAnd(
        IAsyncFuncHandler<bool> left,
        IAsyncFuncHandler<bool> right)
    {
        var predicates = new List<Func<CancellationToken, Task<bool>>>();

        if (left is AndAsyncHandler leftAnd)
            predicates.AddRange(leftAnd.GetPredicates());
        else if (left is ICompiledAsyncHandler<bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(ct => left.Handle(ct));

        if (right is AndAsyncHandler rightAnd)
            predicates.AddRange(rightAnd.GetPredicates());
        else if (right is ICompiledAsyncHandler<bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(ct => right.Handle(ct));

        return new AndAsyncHandler(predicates);
    }

    private static IAsyncFuncHandler<bool> CombineAnd(
        IAsyncFuncHandler<bool> left,
        Func<CancellationToken, Task<bool>> right)
    {
        var predicates = new List<Func<CancellationToken, Task<bool>>>();

        if (left is AndAsyncHandler leftAnd)
            predicates.AddRange(leftAnd.GetPredicates());
        else if (left is ICompiledAsyncHandler<bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(ct => left.Handle(ct));

        predicates.Add(right);
        return new AndAsyncHandler(predicates);
    }

    private static IAsyncFuncHandler<bool> CombineAnd(
        Func<CancellationToken, Task<bool>> left,
        IAsyncFuncHandler<bool> right)
    {
        var predicates = new List<Func<CancellationToken, Task<bool>>> { left };

        if (right is AndAsyncHandler rightAnd)
            predicates.AddRange(rightAnd.GetPredicates());
        else if (right is ICompiledAsyncHandler<bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(ct => right.Handle(ct));

        return new AndAsyncHandler(predicates);
    }

    private static IAsyncFuncHandler<bool> CombineOr(
        IAsyncFuncHandler<bool> left,
        IAsyncFuncHandler<bool> right)
    {
        var predicates = new List<Func<CancellationToken, Task<bool>>>();

        if (left is OrAsyncHandler leftOr)
            predicates.AddRange(leftOr.GetPredicates());
        else if (left is ICompiledAsyncHandler<bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(ct => left.Handle(ct));

        if (right is OrAsyncHandler rightOr)
            predicates.AddRange(rightOr.GetPredicates());
        else if (right is ICompiledAsyncHandler<bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(ct => right.Handle(ct));

        return new OrAsyncHandler(predicates);
    }

    private static IAsyncFuncHandler<bool> CombineOr(
        IAsyncFuncHandler<bool> left,
        Func<CancellationToken, Task<bool>> right)
    {
        var predicates = new List<Func<CancellationToken, Task<bool>>>();

        if (left is OrAsyncHandler leftOr)
            predicates.AddRange(leftOr.GetPredicates());
        else if (left is ICompiledAsyncHandler<bool> leftCompiled)
            predicates.Add(leftCompiled.GetCompiledFunc());
        else
            predicates.Add(ct => left.Handle(ct));

        predicates.Add(right);
        return new OrAsyncHandler(predicates);
    }

    private static IAsyncFuncHandler<bool> CombineOr(
        Func<CancellationToken, Task<bool>> left,
        IAsyncFuncHandler<bool> right)
    {
        var predicates = new List<Func<CancellationToken, Task<bool>>> { left };

        if (right is OrAsyncHandler rightOr)
            predicates.AddRange(rightOr.GetPredicates());
        else if (right is ICompiledAsyncHandler<bool> rightCompiled)
            predicates.Add(rightCompiled.GetCompiledFunc());
        else
            predicates.Add(ct => right.Handle(ct));

        return new OrAsyncHandler(predicates);
    }

    private sealed class AndAsyncHandler<T> : IAsyncFuncHandler<T, bool>, ICompiledAsyncHandler<T, bool>
    {
        private readonly Func<T, CancellationToken, Task<bool>>[] _predicates;
        private readonly Func<T, CancellationToken, Task<bool>> _compiled;

        public AndAsyncHandler(IEnumerable<Func<T, CancellationToken, Task<bool>>> predicates)
        {
            _predicates = predicates.ToArray();
            _compiled = _predicates.Length <= 3 ? BuildSequential() : ExecuteParallel;
        }

        public Task<bool> Handle(T data, CancellationToken cancellationToken = default)
            => _compiled(data, cancellationToken);

        public Func<T, CancellationToken, Task<bool>> GetCompiledFunc() => _compiled;

        public IEnumerable<Func<T, CancellationToken, Task<bool>>> GetPredicates() => _predicates;
        
        private Func<T, CancellationToken, Task<bool>> BuildSequential()
        {
            if (_predicates.Length == 0) 
                return (_, __) => Task.FromResult(true);
            
            if (_predicates.Length == 1) 
                return _predicates[0];
            
            return async (data, ct) =>
            {
                foreach (var predicate in _predicates)
                {
                    if (!await predicate(data, ct).ConfigureAwait(false))
                        return false;
                }
                return true;
            };
        }
        
        private async Task<bool> ExecuteParallel(T data, CancellationToken ct)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var tasks = new Task<bool>[_predicates.Length];
            var failFast = new TaskCompletionSource<bool>();

            for (int i = 0; i < _predicates.Length; i++)
            {
                var predicate = _predicates[i];
                tasks[i] = Task.Run(async () =>
                {
                    try
                    {
                        var result = await predicate(data, cts.Token).ConfigureAwait(false);
                        if (!result)
                        {
                            failFast.TrySetResult(false);
                            cts.Cancel();
                        }
                        return result;
                    }
                    catch (OperationCanceledException) { return false; }
                }, cts.Token);
            }

            var winner = await Task.WhenAny(failFast.Task, Task.WhenAll(tasks)).ConfigureAwait(false);

            if (winner == failFast.Task)
            {
                cts.Cancel();
                return false;
            }

            var results = await Task.WhenAll(tasks).ConfigureAwait(false);
            return Array.TrueForAll(results, r => r);
        }
    }

    private sealed class OrAsyncHandler<T> : IAsyncFuncHandler<T, bool>, ICompiledAsyncHandler<T, bool>
    {
        private readonly Func<T, CancellationToken, Task<bool>>[] _predicates;
        private readonly Func<T, CancellationToken, Task<bool>> _compiled;

        public OrAsyncHandler(IEnumerable<Func<T, CancellationToken, Task<bool>>> predicates)
        {
            _predicates = predicates.ToArray();
            _compiled = _predicates.Length <= 3 ? BuildSequential() : ExecuteParallel;
        }

        public Task<bool> Handle(T data, CancellationToken cancellationToken = default)
            => _compiled(data, cancellationToken);

        public Func<T, CancellationToken, Task<bool>> GetCompiledFunc() => _compiled;

        public IEnumerable<Func<T, CancellationToken, Task<bool>>> GetPredicates() => _predicates;

        private Func<T, CancellationToken, Task<bool>> BuildSequential()
        {
            if (_predicates.Length == 0) 
                return (_, __) => Task.FromResult(false);
            
            if (_predicates.Length == 1) 
                return _predicates[0];

            return async (data, ct) =>
            {
                foreach (var predicate in _predicates)
                {
                    if (await predicate(data, ct).ConfigureAwait(false))
                        return true;
                }
                return false;
            };
        }

        private async Task<bool> ExecuteParallel(T data, CancellationToken ct)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var tasks = new Task<bool>[_predicates.Length];
            var successFast = new TaskCompletionSource<bool>();

            for (int i = 0; i < _predicates.Length; i++)
            {
                var predicate = _predicates[i];
                tasks[i] = Task.Run(async () =>
                {
                    try
                    {
                        var result = await predicate(data, cts.Token).ConfigureAwait(false);
                        if (result)
                        {
                            successFast.TrySetResult(true);
                            cts.Cancel();
                        }
                        return result;
                    }
                    catch (OperationCanceledException) { return false; }
                }, cts.Token);
            }

            var winner = await Task.WhenAny(successFast.Task, Task.WhenAll(tasks)).ConfigureAwait(false);

            if (winner == successFast.Task)
            {
                cts.Cancel();
                return true;
            }

            var results = await Task.WhenAll(tasks).ConfigureAwait(false);
            return Array.Exists(results, r => r);
        }
    }

    private sealed class AndAsyncHandler : IAsyncFuncHandler<bool>, ICompiledAsyncHandler<bool>
    {
        private readonly Func<CancellationToken, Task<bool>>[] _predicates;
        private readonly Func<CancellationToken, Task<bool>> _compiled;

        public AndAsyncHandler(IEnumerable<Func<CancellationToken, Task<bool>>> predicates)
        {
            _predicates = predicates.ToArray();
            _compiled = _predicates.Length <= 3 ? BuildSequential() : ExecuteParallel;
        }

        public Task<bool> Handle(CancellationToken cancellationToken = default)
            => _compiled(cancellationToken);

        public Func<CancellationToken, Task<bool>> GetCompiledFunc() => _compiled;

        public IEnumerable<Func<CancellationToken, Task<bool>>> GetPredicates() => _predicates;

        private Func<CancellationToken, Task<bool>> BuildSequential()
        {
            if (_predicates.Length == 0) 
                return _ => Task.FromResult(true);
            
            if (_predicates.Length == 1) 
                return _predicates[0];

            return async ct =>
            {
                foreach (var predicate in _predicates)
                {
                    if (!await predicate(ct).ConfigureAwait(false))
                        return false;
                }
                return true;
            };
        }

        private async Task<bool> ExecuteParallel(CancellationToken ct)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var tasks = new Task<bool>[_predicates.Length];
            var failFast = new TaskCompletionSource<bool>();

            for (int i = 0; i < _predicates.Length; i++)
            {
                var predicate = _predicates[i];
                tasks[i] = Task.Run(async () =>
                {
                    try
                    {
                        var result = await predicate(cts.Token).ConfigureAwait(false);
                        if (!result)
                        {
                            failFast.TrySetResult(false);
                            cts.Cancel();
                        }
                        return result;
                    }
                    catch (OperationCanceledException) { return false; }
                }, cts.Token);
            }

            var winner = await Task.WhenAny(failFast.Task, Task.WhenAll(tasks)).ConfigureAwait(false);

            if (winner == failFast.Task)
            {
                cts.Cancel();
                return false;
            }

            var results = await Task.WhenAll(tasks).ConfigureAwait(false);
            return Array.TrueForAll(results, r => r);
        }
    }

    private sealed class OrAsyncHandler : IAsyncFuncHandler<bool>, ICompiledAsyncHandler<bool>
    {
        private readonly Func<CancellationToken, Task<bool>>[] _predicates;
        private readonly Func<CancellationToken, Task<bool>> _compiled;

        public OrAsyncHandler(IEnumerable<Func<CancellationToken, Task<bool>>> predicates)
        {
            _predicates = predicates.ToArray();
            _compiled = _predicates.Length <= 3 ? BuildSequential() : ExecuteParallel;
        }

        public Task<bool> Handle(CancellationToken cancellationToken = default)
            => _compiled(cancellationToken);

        public Func<CancellationToken, Task<bool>> GetCompiledFunc() => _compiled;

        public IEnumerable<Func<CancellationToken, Task<bool>>> GetPredicates() => _predicates;

        private Func<CancellationToken, Task<bool>> BuildSequential()
        {
            if (_predicates.Length == 0) 
                return _ => Task.FromResult(false);
            
            if (_predicates.Length == 1) 
                return _predicates[0];

            return async ct =>
            {
                foreach (var predicate in _predicates)
                {
                    if (await predicate(ct).ConfigureAwait(false))
                        return true;
                }
                return false;
            };
        }

        private async Task<bool> ExecuteParallel(CancellationToken ct)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            var tasks = new Task<bool>[_predicates.Length];
            var successFast = new TaskCompletionSource<bool>();

            for (int i = 0; i < _predicates.Length; i++)
            {
                var predicate = _predicates[i];
                tasks[i] = Task.Run(async () =>
                {
                    try
                    {
                        var result = await predicate(cts.Token).ConfigureAwait(false);
                        if (result)
                        {
                            successFast.TrySetResult(true);
                            cts.Cancel();
                        }
                        return result;
                    }
                    catch (OperationCanceledException) { return false; }
                }, cts.Token);
            }

            var winner = await Task.WhenAny(successFast.Task, Task.WhenAll(tasks)).ConfigureAwait(false);

            if (winner == successFast.Task)
            {
                cts.Cancel();
                return true;
            }

            var results = await Task.WhenAll(tasks).ConfigureAwait(false);
            return Array.Exists(results, r => r);
        }
    }

    private sealed class NotAsyncHandler<T> : IAsyncFuncHandler<T, bool>, ICompiledAsyncHandler<T, bool>
    {
        private readonly Func<T, CancellationToken, Task<bool>> _compiled;

        public NotAsyncHandler(IAsyncFuncHandler<T, bool> handler)
        {
            if (handler is ICompiledAsyncHandler<T, bool> compiled)
            {
                var func = compiled.GetCompiledFunc();
                _compiled = async (data, ct) => !await func(data, ct).ConfigureAwait(false);
            }
            else
            {
                _compiled = async (data, ct) => !await handler.Handle(data, ct).ConfigureAwait(false);
            }
        }

        public NotAsyncHandler(Func<T, CancellationToken, Task<bool>> func)
        {
            _compiled = async (data, ct) => !await func(data, ct).ConfigureAwait(false);
        }

        public Task<bool> Handle(T data, CancellationToken cancellationToken = default)
            => _compiled(data, cancellationToken);

        public Func<T, CancellationToken, Task<bool>> GetCompiledFunc() => _compiled;
    }

    private sealed class NotAsyncHandler : IAsyncFuncHandler<bool>, ICompiledAsyncHandler<bool>
    {
        private readonly Func<CancellationToken, Task<bool>> _compiled;

        public NotAsyncHandler(IAsyncFuncHandler<bool> handler)
        {
            if (handler is ICompiledAsyncHandler<bool> compiled)
            {
                var func = compiled.GetCompiledFunc();
                _compiled = async ct => !await func(ct).ConfigureAwait(false);
            }
            else
            {
                _compiled = async ct => !await handler.Handle(ct).ConfigureAwait(false);
            }
        }

        public NotAsyncHandler(Func<CancellationToken, Task<bool>> func)
        {
            _compiled = async ct => !await func(ct).ConfigureAwait(false);
        }

        public Task<bool> Handle(CancellationToken cancellationToken = default)
            => _compiled(cancellationToken);

        public Func<CancellationToken, Task<bool>> GetCompiledFunc() => _compiled;
    }
}
public interface ICompiledAsyncHandlerMark : ISelfMark<ICompiledAsyncHandlerMark> {
}
