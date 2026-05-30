using GMutagenEngine.Handlers.Actions.Intarfeces;
using GMutagenEngine.Handlers.Actions.Interfaces;
using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Mediators.General;
using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Middlewares.Sync.Interfaces;
using GMutagenEngine.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Mediators;

public interface ISyncMediator<in TId> : ISyncMediatorPublish<TId>, ISyncMediatorPublishWithInput<TId>, ISyncMediatorSend<TId>, ISyncMediatorSendWithInput<TId>, ISyncMediatorMark {
}

public interface ISyncMediatorPublish<in TId> : ISyncMediatorPublishMark {
    void Publish(TId? id = default);
}

public interface ISyncMediatorPublishWithInput<in TId> : ISyncMediatorPublishWithInputMark {
    void Publish<TIn>(TIn input, TId? id = default);
}

public interface ISyncMediatorSend<in TId> : ISyncMediatorSendMark {
    TOut? Send<TOut>(TId? id = default);
}

public interface ISyncMediatorSendWithInput<in TId> : ISyncMediatorSendWithInputMark {
    TOut? Send<TIn, TOut>(TIn input, TId? id = default);
}

public interface ISyncMediatorPublish : ISyncMediatorPublishMark {
    void Publish();
}

public interface ISyncMediatorPublishWithInput : ISyncMediatorPublishWithInputMark {
    void Publish<TIn>(TIn input);
}

public interface ISyncMediatorSend : ISyncMediatorSendMark {
    TOut? Send<TOut>();
}
public interface ISyncMediatorSendWithInput : ISyncMediatorSendWithInputMark {
    TOut? Send<TIn, TOut>(TIn request);
}

public interface ISyncMediator : ISyncMediatorPublish, ISyncMediatorPublishWithInput, ISyncMediatorSend, ISyncMediatorSendWithInput, ISyncMediatorMark {
}

public sealed class SyncMediatorPublish<TId>(
    IIndexedSyncRegistry<TId, ISyncActionHandler> registry)
    : ISyncMediatorPublish<TId>
{
    public void Publish(TId? id = default)
    {
        if (!registry.TryGet(id, out var handler))
            return;

        handler.Handle();
    }
}

public sealed class SyncPublishWithInput<TId>(
    IIndexedSyncRegistry<TId, ISyncActionHandlerIn> registry)
    : ISyncMediatorPublishWithInput<TId>
{
    public void Publish<TIn>(TIn input, TId? id = default)
    {
        if (!registry.TryGet(id, out var handler))
            return;

        ((ISyncActionHandler<TIn>)handler).Handle(input);
    }
}

public sealed class SyncSend<TId>(
    IIndexedSyncRegistry<TId, ISyncFuncHandlerOut> registry)
    : ISyncMediatorSend<TId>
{
    public TOut? Send<TOut>(TId? id = default)
    {
        if (!registry.TryGet(id, out var handler))
            return default;

        return ((ISyncFuncHandler<TOut>)handler).Handle();
    }
}

public sealed class SyncSendWithInput<TId>(
    IIndexedSyncRegistry<TId, ISyncFuncHandlerInOut> registry)
    : ISyncMediatorSendWithInput<TId>
{
    public TOut? Send<TIn, TOut>(TIn input, TId? id = default)
    {
        if (!registry.TryGet(id, out var handler))
            return default;

        return ((ISyncFuncHandler<TIn, TOut>)handler).Handle(input);
    }
}

public sealed class SyncMediator<TId>(
    ISyncMediatorPublish<TId> publish,
    ISyncMediatorPublishWithInput<TId> publishWithInput,
    ISyncMediatorSend<TId> send,
    ISyncMediatorSendWithInput<TId> sendWithInput)
    : ISyncMediator<TId>
{
    public void Publish(TId? id = default)
        => publish.Publish(id);

    public void Publish<TIn>(TIn input, TId? id = default)
        => publishWithInput.Publish(input, id);

    public TOut? Send<TOut>(TId? id = default)
        => send.Send<TOut>(id);

    public TOut? Send<TIn, TOut>(TIn input, TId? id = default)
        => sendWithInput.Send<TIn, TOut>(input, id);
}

public sealed class SyncFanOutMediator<TId>(
    ISyncMediatorPublish<TId> publish,
    ISyncMediatorPublishWithInput<TId> publishWithInput,
    ISyncFanOutSend<TId> send,
    ISyncFanOutSendWithInput<TId> sendWithInput)
    : ISyncFanOutMediator<TId>
{
    public void Publish(TId? id = default)
        => publish.Publish(id);

    public void Publish<TIn>(TIn input, TId? id = default)
        => publishWithInput.Publish(input, id);

    public IEnumerable<TOut?> Send<TOut>(TId? id = default)
        => send.Send<TOut>(id);

    public IEnumerable<TOut?> Send<TIn, TOut>(TIn input, TId? id = default)
        => sendWithInput.Send<TIn, TOut>(input, id);
}

public sealed class SyncMediator(
    ISyncMediatorPublish publish,
    ISyncMediatorPublishWithInput publishWithInput,
    ISyncMediatorSend send,
    ISyncMediatorSendWithInput sendWithInput)
    : ISyncMediator
{
    public void Publish()
        => publish.Publish();

    public void Publish<TIn>(TIn input)
        => publishWithInput.Publish(input);

    public TOut? Send<TOut>()
        => send.Send<TOut>();

    public TOut? Send<TIn, TOut>(TIn input)
        => sendWithInput.Send<TIn, TOut>(input);
}

public sealed class SyncFanOutMediator(
    ISyncMediatorPublish publish,
    ISyncMediatorPublishWithInput publishWithInput,
    ISyncFanOutSend send,
    ISyncFanOutSendWithInput sendWithInput)
    : ISyncFanOutMediator
{
    public void Publish()
        => publish.Publish();

    public void Publish<TIn>(TIn input)
        => publishWithInput.Publish(input);

    public IEnumerable<TOut?> Send<TOut>()
        => send.Send<TOut>();

    public IEnumerable<TOut?> Send<TIn, TOut>(TIn input)
        => sendWithInput.Send<TIn, TOut>(input);
}

public interface ISyncFanOutMediator : ISyncMediatorPublish, ISyncMediatorPublishWithInput, ISyncFanOutSend, ISyncFanOutSendWithInput, ISyncFanOutMediatorMark {
}

public interface ISyncFanOutMediator<in TId> : ISyncMediatorPublish<TId>, ISyncMediatorPublishWithInput<TId>, ISyncFanOutSend<TId>, ISyncFanOutSendWithInput<TId>, ISyncFanOutMediatorMark {
}

public sealed class SyncDefaultIdPublish<TId>(
    ISyncMediatorPublish<TId> inner,
    TId defaultId) : ISyncMediatorPublish<TId>, ISyncMediatorPublish
{
    private TId Resolve(TId? id) => id ?? defaultId;

    public void Publish(TId? id = default)
        => inner.Publish(Resolve(id));

    public void Publish()
        => inner.Publish(defaultId);
}

public sealed class SyncDefaultIdPublishWithInput<TId>(
    ISyncMediatorPublishWithInput<TId> inner,
    TId defaultId) : ISyncMediatorPublishWithInput<TId>, ISyncMediatorPublishWithInput
{
    private TId Resolve(TId? id) => id ?? defaultId;

    public void Publish<TIn>(TIn input, TId? id = default)
        => inner.Publish(input, Resolve(id));

    public void Publish<TIn>(TIn input)
        => inner.Publish(input, defaultId);
}

public sealed class SyncDefaultIdSend<TId>(
    ISyncMediatorSend<TId> inner,
    TId defaultId) : ISyncMediatorSend<TId>, ISyncMediatorSend
{
    private TId Resolve(TId? id) => id ?? defaultId;

    public TOut? Send<TOut>(TId? id = default)
        => inner.Send<TOut>(Resolve(id));

    public TOut? Send<TOut>()
        => inner.Send<TOut>(defaultId);
}

public sealed class SyncDefaultIdSendWithInput<TId>(
    ISyncMediatorSendWithInput<TId> inner,
    TId defaultId) : ISyncMediatorSendWithInput<TId>, ISyncMediatorSendWithInput
{
    private TId Resolve(TId? id) => id ?? defaultId;

    public TOut? Send<TIn, TOut>(TIn input, TId? id = default)
        => inner.Send<TIn, TOut>(input, Resolve(id));

    public TOut? Send<TIn, TOut>(TIn request)
        => inner.Send<TIn, TOut>(request, defaultId);
}


public sealed class SyncMediatorGlobalPublishPipeline<TId, TMiddlewareId>(
    ISyncMediatorPublish<TId> terminal,
    IMiddlewareIdFactory<TId, TMiddlewareId> middlewareIdFactory,
    IIndexedSyncRegistry<TMiddlewareId, IEnumerable<IMiddleware>> registry)
    : ISyncMediatorPublish<TId>
    where TId : notnull
{
    public void Publish(TId? id = default)
    {
        Action next = () => terminal.Publish(id);

        var middlewareId = middlewareIdFactory.Create(id);

        if (registry.TryGet(middlewareId, out IEnumerable<IMiddleware> middlewares))
        {
            foreach (var middleware in middlewares.Reverse())
            {
                var current = next;
                next = () => middleware.Invoke(current);
            }
        }

        next();
    }
}

public sealed class SyncMediatorGlobalPublishWithInputPipeline<TId, TMiddlewareId>(
    ISyncMediatorPublishWithInput<TId> terminal,
    IMiddlewareIdFactory<TId, TMiddlewareId> middlewareIdFactory,
    IIndexedSyncRegistry<TMiddlewareId, IEnumerable<IInMiddleware>> registry)
    : ISyncMediatorPublishWithInput<TId>
    where TId : notnull
{
    public void Publish<TIn>(TIn input, TId? id = default)
    {
        Action<TIn> next = ctx => terminal.Publish(ctx, id);

        var middlewareId = middlewareIdFactory.CreateIn(input, id);

        if (registry.TryGet(middlewareId, out IEnumerable<IInMiddleware> middlewares))
        {
            foreach (var middleware in middlewares
                         .Cast<IInMiddleware<TIn>>()
                         .Reverse())
            {
                var current = next;
                next = ctx => middleware.Invoke(ctx, current);
            }
        }

        next(input);
    }
}

public sealed class SyncMediatorGlobalSendPipeline<TId, TMiddlewareId>(
    ISyncMediatorSend<TId> terminal,
    IMiddlewareIdFactory<TId, TMiddlewareId> middlewareIdFactory,
    IIndexedSyncRegistry<TMiddlewareId, IEnumerable<IOutMiddleware>> registry)
    : ISyncMediatorSend<TId>
    where TId : notnull
{
    public TOut? Send<TOut>(TId? id = default)
    {
        Func<TOut?> next = () => terminal.Send<TOut>(id);

        var middlewareId = middlewareIdFactory.CreateOut<TOut>(id);

        if (registry.TryGet(middlewareId, out IEnumerable<IOutMiddleware> middlewares))
        {
            foreach (var middleware in middlewares
                         .Cast<IOutMiddleware<TOut?>>()
                         .Reverse())
            {
                var current = next;
                next = () => middleware.Invoke(current);
            }
        }

        return next();
    }
}

public sealed class SyncMediatorGlobalSendWithInputPipeline<TId, TMiddlewareId>(
    ISyncMediatorSendWithInput<TId> terminal,
    IMiddlewareIdFactory<TId, TMiddlewareId> middlewareIdFactory,
    IIndexedSyncRegistry<TMiddlewareId, IEnumerable<IInOutMiddleware>> registry)
    : ISyncMediatorSendWithInput<TId>
    where TId : notnull
{
    public TOut? Send<TIn, TOut>(TIn input, TId? id = default)
    {
        Func<TIn, TOut?> next = ctx => terminal.Send<TIn, TOut>(ctx, id);

        var middlewareId = middlewareIdFactory.CreateInOut<TIn, TOut>(input, id);

        if (registry.TryGet(middlewareId, out IEnumerable<IInOutMiddleware> middlewares))
        {
            foreach (var middleware in middlewares
                         .Cast<IInOutMiddleware<TIn, TOut?>>()
                         .Reverse())
            {
                var current = next;
                next = ctx => middleware.Invoke(ctx, current);
            }
        }

        return next(input);
    }
}

public sealed class SyncGroupPublishPipeline<TGroupId, THandlerId>(
    IIndexedSyncRegistry<TGroupId, IEnumerable<THandlerId>> registry,
    ISyncMediatorPublish<THandlerId> mediator) : ISyncMediatorPublish<TGroupId>
    where TGroupId : notnull
    where THandlerId : notnull
{
    public void Publish(TGroupId? groupId = default)
    {
        if (!registry.TryGet(groupId, out var group))
            return;

        foreach (var id in group)
            mediator.Publish(id);
    }
}

public sealed class SyncGroupPublishWithInputPipeline<TGroupId, THandlerId>(
    IIndexedSyncRegistry<TGroupId, IEnumerable<THandlerId>> registry,
    ISyncMediatorPublishWithInput<THandlerId> mediator)
    : ISyncMediatorPublishWithInput<TGroupId>
    where TGroupId : notnull
    where THandlerId : notnull
{
    public void Publish<TIn>(TIn input, TGroupId? groupId = default)
    {
        if (!registry.TryGet(groupId, out var group))
            return;

        foreach (var id in group)
            mediator.Publish(input, id);
    }
}

public sealed class SyncGroupSendPipeline<TGroupId, THandlerId>(
    IIndexedSyncRegistry<TGroupId, IEnumerable<THandlerId>> registry,
    ISyncMediatorSend<THandlerId> mediator) : ISyncFanOutSend<TGroupId>
    where TGroupId : notnull
    where THandlerId : notnull
{
    public IEnumerable<TOut?> Send<TOut>(TGroupId? groupId = default)
    {
        if (!registry.TryGet(groupId, out var group))
            yield break;

        foreach (var id in group)
            yield return mediator.Send<TOut>(id);
    }
}

public sealed class SyncGroupSendWithInputPipeline<TGroupId, THandlerId>(
    IIndexedSyncRegistry<TGroupId, IEnumerable<THandlerId>> registry,
    ISyncMediatorSendWithInput<THandlerId> mediator)
    : ISyncFanOutSendWithInput<TGroupId>
    where TGroupId : notnull
    where THandlerId : notnull
{
    public IEnumerable<TOut?> Send<TIn, TOut>(TIn input, TGroupId? groupId = default)
    {
        if (!registry.TryGet(groupId, out var group))
            yield break;

        foreach (var id in group)
            yield return mediator.Send<TIn, TOut>(input, id);
    }
}

public sealed class SyncGroupSendWithAggregatorPipeline<TGroupId>(
    ISyncFanOutSend<TGroupId> fanOutSend,
    IIndexedSyncRegistry<TGroupId, ISyncFuncHandlerInOut> registry)
    : ISyncMediatorSend<TGroupId>
    where TGroupId : notnull
{
    public TOut? Send<TOut>(TGroupId? groupId = default)
    {
        var results = fanOutSend.Send<TOut>(groupId);

        if (!registry.TryGet(groupId, out var aggregator) ||
            aggregator is not ISyncFuncHandler<IEnumerable<TOut?>, TOut?> handler)
            return results.FirstOrDefault();


        return handler.Handle(results);
    }
}

public sealed class SyncFanOutDefaultIdSend<TGroupId>(
    ISyncFanOutSend<TGroupId> inner,
    TGroupId defaultId
) : ISyncFanOutSend<TGroupId>, ISyncFanOutSend
    where TGroupId : notnull
{
    private TGroupId Resolve(TGroupId? id) => id ?? defaultId;

    public IEnumerable<TOut?> Send<TOut>(TGroupId? groupId = default)
        => inner.Send<TOut>(Resolve(groupId));

    public IEnumerable<TOut?> Send<TOut>()
        => inner.Send<TOut>(defaultId);
}

public sealed class SyncFanOutDefaultIdSendWithInput<TGroupId>(
    ISyncFanOutSendWithInput<TGroupId> inner,
    TGroupId defaultId
) : ISyncFanOutSendWithInput<TGroupId>, ISyncFanOutSendWithInput
    where TGroupId : notnull
{
    private TGroupId Resolve(TGroupId? id) => id ?? defaultId;

    public IEnumerable<TOut?> Send<TIn, TOut>(TIn input, TGroupId? groupId = default)
        => inner.Send<TIn, TOut>(input, Resolve(groupId));

    public IEnumerable<TOut?> Send<TIn, TOut>(TIn input)
        => inner.Send<TIn, TOut>(input, defaultId);
}

public interface ISyncFanOutSend<in TId> : ISyncFanOutSendMark {
    IEnumerable<TOut?> Send<TOut>(TId? id = default);
}

public interface ISyncFanOutSend : ISyncFanOutSendMark {
    IEnumerable<TOut?> Send<TOut>();
}

public interface ISyncFanOutSendWithInput<in TId> : ISyncFanOutSendWithInputMark {
    IEnumerable<TOut?> Send<TIn, TOut>(TIn input, TId? id = default);
}

public interface ISyncFanOutSendWithInput : ISyncFanOutSendWithInputMark {
    IEnumerable<TOut?> Send<TIn, TOut>(TIn input);
}

public sealed class SyncGroupSendWithInputAggregatorPipeline<TGroupId>(
    ISyncFanOutSendWithInput<TGroupId> fanOutSendIn,
    IIndexedSyncRegistry<TGroupId, ISyncFuncHandlerInOut> registry)
    : ISyncMediatorSendWithInput<TGroupId>
    where TGroupId : notnull
{
    public TOut? Send<TIn, TOut>(TIn input, TGroupId? groupId = default)
    {
        var results = fanOutSendIn.Send<TIn, TOut>(input, groupId);

        if (!registry.TryGet(groupId, out var aggregator) ||
            aggregator is not ISyncFuncHandler<IEnumerable<TOut?>, TOut?> handler)
            return results.FirstOrDefault();


        return handler.Handle(results);
    }
}

public sealed class SyncMediatorPublishPipeline<TId>(
    ISyncMediatorPublish<TId> terminal,
    IIndexedSyncRegistry<TId, IEnumerable<IMiddleware>> registry)
    : ISyncMediatorPublish<TId>
    where TId : notnull
{
    public void Publish(TId? id = default)
    {
        Action next = () => terminal.Publish(id);

        if (registry.TryGet(id, out IEnumerable<IMiddleware> middlewares))
        {
            foreach (var middleware in middlewares.Reverse())
            {
                var current = next;
                next = () => middleware.Invoke(current);
            }
        }

        next();
    }
}


public sealed class SyncMediatorPublishWithInputPipeline<TId>(
    ISyncMediatorPublishWithInput<TId> terminal,
    IIndexedSyncRegistry<TId, IEnumerable<IInMiddleware>> registry)
    : ISyncMediatorPublishWithInput<TId>
    where TId : notnull
{
    public void Publish<TIn>(TIn input, TId? id = default)
    {
        Action<TIn> next = ctx => terminal.Publish(ctx, id);

        if (registry.TryGet(id, out IEnumerable<IInMiddleware> middlewares))
        {
            foreach (var middleware in middlewares
                         .Cast<IInMiddleware<TIn>>()
                         .Reverse())
            {
                var current = next;
                next = ctx => middleware.Invoke(ctx, current);
            }
        }

        next(input);
    }
}


public sealed class SyncMediatorSendPipeline<TId>(
    ISyncMediatorSend<TId> terminal,
    IIndexedSyncRegistry<TId, IEnumerable<IOutMiddleware>> registry)
    : ISyncMediatorSend<TId>
    where TId : notnull
{
    public TOut? Send<TOut>(TId? id = default)
    {
        Func<TOut?> next = () => terminal.Send<TOut>(id);

        if (registry.TryGet(id, out IEnumerable<IOutMiddleware> middlewares))
        {
            foreach (var middleware in middlewares
                         .Cast<IOutMiddleware<TOut?>>()
                         .Reverse())
            {
                var current = next;
                next = () => middleware.Invoke(current);
            }
        }

        return next();
    }
}


public sealed class SyncMediatorSendWithInputPipeline<TId>(
    ISyncMediatorSendWithInput<TId> terminal,
    IIndexedSyncRegistry<TId, IEnumerable<IInOutMiddleware>> registry)
    : ISyncMediatorSendWithInput<TId>
    where TId : notnull
{
    public TOut? Send<TIn, TOut>(TIn input, TId? id = default)
    {
        Func<TIn, TOut?> next = ctx => terminal.Send<TIn, TOut>(ctx, id);

        if (registry.TryGet(id, out IEnumerable<IInOutMiddleware> middlewares))
        {
            foreach (var middleware in middlewares
                         .Cast<IInOutMiddleware<TIn, TOut?>>()
                         .Reverse())
            {
                var current = next;
                next = ctx => middleware.Invoke(ctx, current);
            }
        }

        return next(input);
    }
}
public interface ISyncMediatorMark : ISelfMark<ISyncMediatorMark> {
}

public interface ISyncMediatorPublishMark : ISelfMark<ISyncMediatorPublishMark> {
}

public interface ISyncMediatorPublishWithInputMark : ISelfMark<ISyncMediatorPublishWithInputMark> {
}

public interface ISyncMediatorSendMark : ISelfMark<ISyncMediatorSendMark> {
}

public interface ISyncMediatorSendWithInputMark : ISelfMark<ISyncMediatorSendWithInputMark> {
}

public interface ISyncFanOutMediatorMark : ISelfMark<ISyncFanOutMediatorMark> {
}

public interface ISyncFanOutSendMark : ISelfMark<ISyncFanOutSendMark> {
}

public interface ISyncFanOutSendWithInputMark : ISelfMark<ISyncFanOutSendWithInputMark> {
}