using GMutagenEngine.Mediators;

namespace GMutagenEngine.Builders.Sync;

public abstract class Builder<TSelf, TId> : IBuilder<TSelf, TId>
    where TSelf : Builder<TSelf, TId>
    where TId : notnull
{
    private ISyncMediator<TId> _mediator;

    public Builder(ISyncMediator<TId> mediator)
    {
        _mediator = mediator;
    }
        
    public Builder()
    {
    }

    public TOut Execute<TData, TOut>(TData data, TId id)
        where TOut : Builder<TSelf, TId>, new()
    {
        _mediator.Publish(data, id);
        return Create<TOut>(_mediator);
    }

    public TOut Execute<TOut>(TId id)
        where TOut : Builder<TSelf, TId>, new()
    {
        _mediator.Publish(id);
        return Create<TOut>(_mediator);
    }

    public TData? Build<TData>(TId id)
    {
        return _mediator.Send<TData>(id);
    }

    public static TOut Create<TOut>(ISyncMediator<TId> mediator) where TOut : Builder<TSelf, TId>, new()
    {
        return new TOut
        {
            _mediator = mediator,
        };
    }
}