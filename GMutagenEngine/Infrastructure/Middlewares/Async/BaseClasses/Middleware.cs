
using GMutagenEngine.Infrastructure.Middlewares.Async.Interfaces;

namespace GMutagenEngine.Infrastructure.Middlewares.Async.BaseClasses
{
    public abstract class Middleware<TData> : IMiddleware<TData>
    {
        public abstract Task InvokeAsync(TData context, Func<CancellationToken, Task> next,
            CancellationToken cancellationToken = default);
    }
}