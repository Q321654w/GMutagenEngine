using GMutagenEngine.Middlewares.Async.BaseClasses;

namespace GMutagenEngine.Middlewares.Api.Async;

public class DelegateMiddleware<TData>(Func<TData, Func<TData, CancellationToken, Task>, CancellationToken, Task> func) : Middleware<TData>
{
    public override async Task Invoke(TData context, Func<TData, CancellationToken, Task> next, CancellationToken cancellationToken = default)
    {
        await func(context, next, cancellationToken);
    }
}