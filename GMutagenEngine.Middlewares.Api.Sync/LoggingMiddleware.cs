using GMutagenEngine.Logging;
using GMutagenEngine.Middlewares.Sync.BaseClasses;

namespace GMutagenEngine.Middlewares.Api.Sync;

public class LoggingMiddleware<TData>(ISyncLogger<string> syncLogger) : Middleware<TData>
{
    public override void Invoke(TData context, Action<TData> next)
    {
        syncLogger.Log($"Before handler: {context}");
        next(context);
        syncLogger.Log($"After handler: {context}");
    }
}
