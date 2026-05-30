using GMutagenEngine.Mediators.Async;

namespace GMutagenEngine.Builders.Async;

public abstract class Builder<TSelf, TId> : IBuilder<TSelf, TId>
    where TSelf : Builder<TSelf, TId>
    where TId : notnull
{
    private IAsyncMediator<TId> _mediator;

    public Builder(IAsyncMediator<TId> mediator)
    {
        _mediator = mediator;
    }
        
    public Builder()
    {
    }

    public async Task<TOut> Execute<TData, TOut>(TData data, TId id, CancellationToken cancellationToken)
        where TOut : Builder<TSelf, TId>, new()
    {
        await _mediator.Publish(data, id, cancellationToken);
        return Create<TOut>(_mediator);
    }

    public async Task<TOut> Execute<TOut>(TId id, CancellationToken cancellationToken)
        where TOut : Builder<TSelf, TId>, new()
    {
        await _mediator.Publish(id, cancellationToken);
        return Create<TOut>(_mediator);
    }

    public async Task<TData?> Build<TData>(TId id, CancellationToken cancellationToken)
    {
        return await _mediator.Send<TData>(id, cancellationToken);
    }

    public static TOut Create<TOut>(IAsyncMediator<TId> mediator) where TOut : Builder<TSelf, TId>, new()
    {
        return new TOut
        {
            _mediator = mediator,
        };
    }
}