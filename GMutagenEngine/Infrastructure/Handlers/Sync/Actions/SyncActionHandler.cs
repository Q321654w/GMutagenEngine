namespace GMutagenEngine.Infrastructure.Handlers.Sync.Actions;

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