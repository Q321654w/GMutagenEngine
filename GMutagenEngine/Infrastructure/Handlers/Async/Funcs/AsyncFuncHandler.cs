namespace GMutagenEngine.Infrastructure.Handlers.Async.Funcs;

public class AsyncFuncHandler<TOut>(Func<CancellationToken, Task<TOut>> func) : IAsyncFuncHandler<TOut>
{
    public Task<TOut> Invoke(CancellationToken cancellationToken = default)
    {
        return func(cancellationToken);
    }
}

public class AsyncFuncHandler<TIn, TOut>(Func<TIn, CancellationToken, Task<TOut>> func) : IAsyncFuncHandler<TIn, TOut>
{
    public Task<TOut> Handle(TIn data, CancellationToken cancellationToken = default)
    {
        return func(data, cancellationToken);
    }
}