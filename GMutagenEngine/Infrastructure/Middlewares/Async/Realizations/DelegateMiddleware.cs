using GMutagenEngine.Infrastructure.Middlewares.Async.BaseClasses;

namespace GMutagenEngine.Infrastructure.Middlewares.Async.Realizations
{
    public class DelegateMiddleware<TData>(Func<TData, Func<CancellationToken, Task>, CancellationToken, Task> func) 
        : Middleware<TData>
    {
        public override async Task InvokeAsync(TData context, Func<CancellationToken, Task> next, CancellationToken cancellationToken = default)
        {
            await func(context, next, cancellationToken);
        }
    }
}