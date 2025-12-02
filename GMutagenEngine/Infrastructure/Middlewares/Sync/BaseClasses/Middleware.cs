using GMutagenEngine.Infrastructure.Middlewares.Sync.Interfaces;

namespace GMutagenEngine.Infrastructure.Middlewares.Sync.BaseClasses
{
    public abstract class Middleware<TData> : IMiddleware<TData>
    {
        public abstract void Invoke(TData context, Action next);
    }
}