using GMutagenEngine.Handlers.Actions.Intarfeces;
using GMutagenEngine.Handlers.Actions.Interfaces;

namespace GMutagenEngine.Handlers.Actions.Realizations;

public class SyncActionHandler(Action action) : ISyncActionHandler
{
    public void Handle()
    {
        action();
    }
}

public class SyncActionHandler<TIn>(Action<TIn> action) : ISyncActionHandler<TIn>
{
    public void Handle(TIn data)
    {
        action.Invoke(data);
    }
}