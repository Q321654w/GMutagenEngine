using GMutagenEngine.Middlewares.Sync.Interfaces;

namespace GMutagenEngine.Middlewares.Sync.BaseClasses;

public abstract class Middleware : IMiddleware
{
    public abstract void Invoke(Action next);
}

public abstract class Middleware<TData> : IInMiddleware<TData>
{
    public abstract void Invoke(TData context, Action<TData> next);
}