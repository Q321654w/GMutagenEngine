
using GMutagenEngine.Middlewares.Async.Interfaces;

namespace GMutagenEngine.Middlewares.Async.BaseClasses;

public abstract class Middleware<TData> : IInMiddleware<TData>
{
    public abstract Task Invoke(TData context, Func<TData, CancellationToken, Task> next,
        CancellationToken cancellationToken = default);
}


public abstract class Middleware : IMiddleware
{
    public abstract Task Invoke(Func<CancellationToken, Task> next,
        CancellationToken cancellationToken = default);
}