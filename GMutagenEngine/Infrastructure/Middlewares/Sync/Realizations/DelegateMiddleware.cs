using GMutagenEngine.Infrastructure.Middlewares.Sync.BaseClasses;

namespace GMutagenEngine.Infrastructure.Middlewares.Sync.Realizations
{
    public class DelegateMiddleware<TData>(Action<TData, Action> action) 
        : Middleware<TData>
    {
        public override void Invoke(TData context, Action next)
        {
            action(context, next);
        }
    }
}