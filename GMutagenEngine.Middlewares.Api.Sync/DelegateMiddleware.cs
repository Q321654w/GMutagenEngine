using GMutagenEngine.Middlewares.Sync.BaseClasses;

namespace GMutagenEngine.Middlewares.Api.Sync;

public class DelegateMiddleware<TData>(Action<TData, Action<TData>> action) : Middleware<TData>
{
    public override void Invoke(TData context, Action<TData> next)
    {
        action(context, next);
    }
}