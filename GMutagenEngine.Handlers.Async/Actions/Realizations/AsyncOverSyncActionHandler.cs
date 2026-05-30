using GMutagenEngine.Handlers.Async.Actions.Interfaces;

namespace GMutagenEngine.Handlers.Async.Actions.Realizations;

public class AsyncOverSyncActionHandler(Action action) : IAsyncActionHandler
{
    public Task Handle(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        action();
        return Task.CompletedTask;
    }
}

public class AsyncOverSyncActionHandler<TIn>(Action<TIn> action) : IAsyncActionHandler<TIn>
{
    public Task Handle(TIn data, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        action(data);
        return Task.CompletedTask;
    }
}