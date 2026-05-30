using GMutagenEngine.Logging;
using GMutagenEngine.Middlewares.Async.BaseClasses;

namespace GMutagenEngine.Middlewares.Api.Async;

public class LoggingMiddleware<TData>(ISyncLogger<LoggingMiddlewareLogContext<TData>> syncLogger) : Middleware<TData>
{
    public override async Task Invoke(TData context, Func<TData, CancellationToken, Task> next,
        CancellationToken cancellationToken = default)
    {
        syncLogger.LogBefore(context);
        await next(context, cancellationToken);
        syncLogger.LogAfter(context);
    }
}
