using GMutagenEngine.Logging;

namespace GMutagenEngine.Middlewares.Api.Async;

public static class LoggingMiddlewareLoggerExtensions
{
    public static void LogBefore<TData>(this ISyncLogger<LoggingMiddlewareLogContext<TData>> syncLogger,
        TData data)
    {
        syncLogger.Log(new LoggingMiddlewareLogContext<TData>
        {
            Data = data,
            Phase = LoggingMiddlewarePhase.Before,
        });
    }
    public static void LogAfter<TData>(this ISyncLogger<LoggingMiddlewareLogContext<TData>> syncLogger,
        TData data)
    {
        syncLogger.Log(new LoggingMiddlewareLogContext<TData>
        {
            Data = data,
            Phase = LoggingMiddlewarePhase.After,
        });
    }
}
