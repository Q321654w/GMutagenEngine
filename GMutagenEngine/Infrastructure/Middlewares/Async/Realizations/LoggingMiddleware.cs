using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Middlewares.Async.BaseClasses;

namespace GMutagenEngine.Infrastructure.Middlewares.Async.Realizations
{
    public class LoggingMiddleware<TData>(ILogger logger) : Middleware<TData>
    {
        public override async Task InvokeAsync(TData context, Func<CancellationToken, Task> next, CancellationToken cancellationToken = default)
        {
            logger.LogInfo($"Before handler: {context.ToString()}");
            await next(cancellationToken);
            logger.LogInfo($"After handler: {context.ToString()}");
        }
    }
}