using GMutagenEngine.Logging;

namespace GMutagenEngine.Middlewares.Api.Async;

public class LoggingMiddlewareLogContext<TData>
{
    public TData Data { get; init; }
    public LoggingMiddlewarePhase Phase { get; init; }
}

public enum LoggingMiddlewarePhase
{
    Before,
    After
}
