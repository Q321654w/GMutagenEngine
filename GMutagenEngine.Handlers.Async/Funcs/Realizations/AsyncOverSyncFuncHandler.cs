using GMutagenEngine.Handlers.Async.Funcs.Interfaces;

namespace GMutagenEngine.Handlers.Async.Funcs.Realizations;

public class AsyncOverSyncFuncHandler<TOut>(Func<TOut> func) : IAsyncFuncHandler<TOut>
{
    public Task<TOut> Handle(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var res = func();
        return Task.FromResult(res);
    }
}

public class AsyncOverSyncFuncHandler<TIn, TOut>(Func<TIn, TOut> func) : IAsyncFuncHandler<TIn, TOut>
{
    public Task<TOut> Handle(TIn data, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var res = func(data);
        return Task.FromResult(res);
    }
}