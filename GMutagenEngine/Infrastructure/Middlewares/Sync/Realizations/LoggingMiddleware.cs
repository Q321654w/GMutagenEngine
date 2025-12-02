using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Middlewares.Sync.BaseClasses;

namespace GMutagenEngine.Infrastructure.Middlewares.Sync.Realizations
{
    public class LoggingMiddleware<TId>(ILogger logger) : Middleware<TId>
    {
        public override void Invoke(TId context, Action next)
        {
            logger.LogInfo($"Before handler: {context.ToString()}");
            next();
            logger.LogInfo($"After handler: {context.ToString()}");
        }
    }
}