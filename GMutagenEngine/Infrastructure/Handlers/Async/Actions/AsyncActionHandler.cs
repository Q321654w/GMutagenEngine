namespace GMutagenEngine.Infrastructure.Handlers.Async.Actions;

public class AsyncActionHandler(Func<CancellationToken, Task> action) : IAsyncActionHandler
{
    public Task Handle(CancellationToken cancellationToken = default)
    {
        return action(cancellationToken);
    }
}

public class AsyncActionHandler<TIn>(Func<TIn, CancellationToken, Task> action) : IAsyncActionHandler<TIn>
{
    public Task Handle(TIn data, CancellationToken cancellationToken = default)
    {
        return action(data, cancellationToken);
    }
}