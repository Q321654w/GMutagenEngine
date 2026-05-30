using System.Runtime.CompilerServices;
using Factories;
using GMutagenEngine.Handlers.Async.Actions.Interfaces;
using GMutagenEngine.Handlers.Async.Funcs.Interfaces;
using GMutagenEngine.Mediators.General;
using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Middlewares.Async.Interfaces;
using GMutagenEngine.Storing.Registries.Async.Indexed;

namespace GMutagenEngine.Mediators.Async;

public interface IAsyncMediator<in TId> : IAsyncMediatorPublish<TId>, IAsyncMediatorPublishWithInput<TId>, IAsyncMediatorSend<TId>, IAsyncMediatorSendWithInput<TId>, IAsyncMediatorMark {
}

public interface IAsyncMediatorPublish<in TId> : IAsyncMediatorPublishMark {
    Task Publish(TId? id = default, CancellationToken cancellationToken = default);
}

public interface IAsyncMediatorPublishWithInput<in TId> : IAsyncMediatorPublishWithInputMark {
    Task Publish<TIn>(TIn input, TId? id = default, CancellationToken cancellationToken = default);
}

public interface IAsyncMediatorSend<in TId> : IAsyncMediatorSendMark {
    Task<TOut?> Send<TOut>(TId? id = default, CancellationToken cancellationToken = default);
}

public interface IAsyncMediatorSendWithInput<in TId> : IAsyncMediatorSendWithInputMark {
    Task<TOut?> Send<TIn, TOut>(TIn input, TId? id = default, CancellationToken cancellationToken = default);
}

public interface IAsyncMediatorPublish : IAsyncMediatorPublishMark {
    Task Publish(CancellationToken cancellationToken = default);
}

public interface IAsyncMediatorPublishWithInput : IAsyncMediatorPublishWithInputMark {
    Task Publish<TIn>(TIn input, CancellationToken cancellationToken = default);
}

public interface IAsyncMediatorSend : IAsyncMediatorSendMark {
    Task<TOut?> Send<TOut>(CancellationToken cancellationToken = default);
}

public interface IAsyncMediatorSendWithInput : IAsyncMediatorSendWithInputMark {
    Task<TOut?> Send<TIn, TOut>(TIn request, CancellationToken cancellationToken = default);
}

public interface IAsyncMediator : IAsyncMediatorPublish, IAsyncMediatorPublishWithInput, IAsyncMediatorSend, IAsyncMediatorSendWithInput, IAsyncMediatorMark {
}

public sealed class AsyncMediatorPublish<TId>(
    IIndexedAsyncRegistry<TId, IAsyncActionHandler> registry)
    : IAsyncMediatorPublish<TId>
{
    public async Task Publish(TId? id = default, CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(id, out var handler, cancellationToken))
            return;
        
        await handler.Handle(cancellationToken);
    }
}

public sealed class AsyncMediatorPublishWithInput<TId>(
    IIndexedAsyncRegistry<TId, IAsyncActionHandlerIn> registry)
    : IAsyncMediatorPublishWithInput<TId>
{
    public async Task Publish<TIn>(TIn input, TId? id = default, CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(id, out var handler, cancellationToken))
            return;
        
        await ((IAsyncActionHandler<TIn>)handler).Handle(input, cancellationToken);
    }
}

public sealed class AsyncMediatorSend<TId>(
    IIndexedAsyncRegistry<TId, IAsyncFuncHandlerOut> registry)
    : IAsyncMediatorSend<TId>
{
    public async Task<TOut?> Send<TOut>(TId? id = default, CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(id, out var handler, cancellationToken))
            return default;
        
        return await ((IAsyncFuncHandler<TOut>)handler).Handle(cancellationToken);
    }
}

public sealed class AsyncMediatorSendWithInput<TId>(
    IIndexedAsyncRegistry<TId, IAsyncFuncHandlerInOut> registry)
    : IAsyncMediatorSendWithInput<TId>
{
    public async Task<TOut?> Send<TIn, TOut>(TIn input, TId? id = default,
        CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(id, out var handler, cancellationToken))
            return default;
        
        return await ((IAsyncFuncHandler<TIn, TOut>)handler).Handle(input, cancellationToken);
    }
}

public sealed class AsyncMediator<TId>(
    IAsyncMediatorPublish<TId> publish,
    IAsyncMediatorPublishWithInput<TId> publishWithInput,
    IAsyncMediatorSend<TId> send,
    IAsyncMediatorSendWithInput<TId> sendWithInput)
    : IAsyncMediator<TId>
{
    public Task Publish(TId? id = default, CancellationToken cancellationToken = default)
        => publish.Publish(id, cancellationToken);

    public Task Publish<TIn>(TIn input, TId? id = default, CancellationToken cancellationToken = default)
        => publishWithInput.Publish(input, id, cancellationToken);

    public Task<TOut?> Send<TOut>(TId? id = default, CancellationToken cancellationToken = default)
        => send.Send<TOut>(id, cancellationToken);

    public Task<TOut?> Send<TIn, TOut>(TIn input, TId? id = default, CancellationToken cancellationToken = default)
        => sendWithInput.Send<TIn, TOut>(input, id, cancellationToken);
}

public sealed class AsyncFanOutMediator<TId>(
    IAsyncMediatorPublish<TId> publish,
    IAsyncMediatorPublishWithInput<TId> publishWithInput,
    IAsyncFanOutSend<TId> send,
    IAsyncFanOutSendWithInput<TId> sendWithInput)
    : IAsyncFanOutMediator<TId>
{
    public Task Publish(TId? id = default, CancellationToken cancellationToken = default)
        => publish.Publish(id, cancellationToken);

    public Task Publish<TIn>(TIn input, TId? id = default, CancellationToken cancellationToken = default)
        => publishWithInput.Publish(input, id, cancellationToken);

    public IAsyncEnumerable<TOut?> Send<TOut>(TId? id = default, CancellationToken cancellationToken = default)
        => send.Send<TOut>(id, cancellationToken);

    public IAsyncEnumerable<TOut?> Send<TIn, TOut>(TIn input, TId? id = default,
        CancellationToken cancellationToken = default)
        => sendWithInput.Send<TIn, TOut>(input, id, cancellationToken);
}

public sealed class AsyncMediator(
    IAsyncMediatorPublish publish,
    IAsyncMediatorPublishWithInput publishWithInput,
    IAsyncMediatorSend send,
    IAsyncMediatorSendWithInput sendWithInput)
    : IAsyncMediator
{
    public Task Publish(CancellationToken cancellationToken = default)
        => publish.Publish(cancellationToken);

    public Task Publish<TIn>(TIn input, CancellationToken cancellationToken = default)
        => publishWithInput.Publish(input, cancellationToken);

    public Task<TOut?> Send<TOut>(CancellationToken cancellationToken = default)
        => send.Send<TOut>(cancellationToken);

    public Task<TOut?> Send<TIn, TOut>(TIn input, CancellationToken cancellationToken = default)
        => sendWithInput.Send<TIn, TOut>(input, cancellationToken);
}

public sealed class AsyncFanOutMediator(
    IAsyncMediatorPublish publish,
    IAsyncMediatorPublishWithInput publishWithInput,
    IAsyncFanOutSend send,
    IAsyncFanOutSendWithInput sendWithInput)
    : IAsyncFanOutMediator
{
    public Task Publish(CancellationToken cancellationToken = default)
        => publish.Publish(cancellationToken);

    public Task Publish<TIn>(TIn input, CancellationToken cancellationToken = default)
        => publishWithInput.Publish(input, cancellationToken);

    public IAsyncEnumerable<TOut?> Send<TOut>(CancellationToken cancellationToken = default)
        => send.Send<TOut>(cancellationToken);

    public IAsyncEnumerable<TOut?> Send<TIn, TOut>(TIn input, CancellationToken cancellationToken = default)
        => sendWithInput.Send<TIn, TOut>(input, cancellationToken);
}

public interface IAsyncFanOutMediator : IAsyncMediatorPublish, IAsyncMediatorPublishWithInput, IAsyncFanOutSend, IAsyncFanOutSendWithInput, IAsyncFanOutMediatorMark {
}

public interface IAsyncFanOutMediator<in TId> : IAsyncMediatorPublish<TId>, IAsyncMediatorPublishWithInput<TId>, IAsyncFanOutSend<TId>, IAsyncFanOutSendWithInput<TId>, IAsyncFanOutMediatorMark {
}

public sealed class AsyncDefaultIdPublish<TId>(
    IAsyncMediatorPublish<TId> inner,
    TId defaultId) : IAsyncMediatorPublish<TId>, IAsyncMediatorPublish
{
    private TId Resolve(TId? id) => id ?? defaultId;

    public Task Publish(TId? id = default, CancellationToken cancellationToken = default)
        => inner.Publish(Resolve(id), cancellationToken);

    public Task Publish(CancellationToken cancellationToken = default)
        => inner.Publish(defaultId, cancellationToken);
}

public sealed class AsyncDefaultIdPublishWithInput<TId>(
    IAsyncMediatorPublishWithInput<TId> inner,
    TId defaultId) : IAsyncMediatorPublishWithInput<TId>, IAsyncMediatorPublishWithInput
{
    private TId Resolve(TId? id) => id ?? defaultId;

    public Task Publish<TIn>(TIn input, TId? id = default, CancellationToken cancellationToken = default)
        => inner.Publish(input, Resolve(id), cancellationToken);

    public Task Publish<TIn>(TIn input, CancellationToken cancellationToken = default)
        => inner.Publish(input, defaultId, cancellationToken);
}

public sealed class AsyncDefaultIdSend<TId>(
    IAsyncMediatorSend<TId> inner,
    TId defaultId) : IAsyncMediatorSend<TId>, IAsyncMediatorSend
{
    private TId Resolve(TId? id) => id ?? defaultId;

    public Task<TOut?> Send<TOut>(TId? id = default, CancellationToken cancellationToken = default)
        => inner.Send<TOut>(Resolve(id), cancellationToken);

    public Task<TOut?> Send<TOut>(CancellationToken cancellationToken = default)
        => inner.Send<TOut>(defaultId, cancellationToken);
}

public sealed class AsyncDefaultIdSendWithInput<TId>(
    IAsyncMediatorSendWithInput<TId> inner,
    TId defaultId) : IAsyncMediatorSendWithInput<TId>, IAsyncMediatorSendWithInput
{
    private TId Resolve(TId? id) => id ?? defaultId;

    public Task<TOut?> Send<TIn, TOut>(TIn input, TId? id = default, CancellationToken cancellationToken = default)
        => inner.Send<TIn, TOut>(input, Resolve(id), cancellationToken);

    public Task<TOut?> Send<TIn, TOut>(TIn request, CancellationToken cancellationToken = default)
        => inner.Send<TIn, TOut>(request, defaultId, cancellationToken);
}

public sealed class AsyncMediatorGlobalPublishPipeline<TId, TMiddlewareId>(
    IAsyncMediatorPublish<TId> terminal,
    IFactory<TId, TMiddlewareId> middlewareIdFactory,
    IIndexedAsyncRegistry<TMiddlewareId, IEnumerable<IMiddleware>> registry)
    : IAsyncMediatorPublish<TId>
    where TId : notnull
{
    public async Task Publish(TId? id = default, CancellationToken cancellationToken = default)
    {
        Func<CancellationToken, Task> next = ct => terminal.Publish(id, ct);
       
        var middlewareId = middlewareIdFactory.Create(id);

        if (await registry.TryGet(middlewareId, out var middlewares, cancellationToken))
        {
            foreach (var middleware in middlewares.Reverse())
            {
                var current = next;
                next = ct => middleware.Invoke(current, ct);
            }
        }

        await next(cancellationToken);
    }
}

public sealed class AsyncMediatorGlobalPublishWithInputPipeline<TId, TMiddlewareId>(
    IAsyncMediatorPublishWithInput<TId> terminal,
    IMiddlewareIdFactory<TId, TMiddlewareId> middlewareIdFactory,
    IIndexedAsyncRegistry<TMiddlewareId, IEnumerable<IInMiddleware>> registry)
    : IAsyncMediatorPublishWithInput<TId>
    where TId : notnull
{
    public async Task Publish<TIn>(TIn input, TId? id = default, CancellationToken cancellationToken = default)
    {
        Func<TIn, CancellationToken, Task> next = (ctx, ct) => terminal.Publish(ctx, id, ct);

        var middlewareId = middlewareIdFactory.CreateIn(input, id);

        if (await registry.TryGet(middlewareId, out var middlewares, cancellationToken))
        {
            foreach (var middleware in middlewares
                         .Cast<IInMiddleware<TIn>>()
                         .Reverse())
            {
                var current = next;
                next = (ctx, ct) => middleware.Invoke(ctx, current, ct);
            }
        }

        await next(input, cancellationToken);
    }
}

public sealed class AsyncMediatorGlobalSendPipeline<TId, TMiddlewareId>(
    IAsyncMediatorSend<TId> terminal,
    IMiddlewareIdFactory<TId, TMiddlewareId> middlewareIdFactory,
    IIndexedAsyncRegistry<TMiddlewareId, IEnumerable<IOutMiddleware>> registry)
    : IAsyncMediatorSend<TId>
    where TId : notnull
{
    public async Task<TOut?> Send<TOut>(TId? id = default, CancellationToken cancellationToken = default)
    {
        Func<CancellationToken, Task<TOut?>> next = ct => terminal.Send<TOut>(id, ct);

        var middlewareId = middlewareIdFactory.CreateOut<TOut>(id);

        if (await registry.TryGet(middlewareId, out var middlewares, cancellationToken))
        {
            foreach (var middleware in middlewares
                         .Cast<IOutMiddleware<TOut?>>()
                         .Reverse())
            {
                var current = next;
                next = ct => middleware.Invoke(current, ct);
            }
        }

        return await next(cancellationToken);
    }
}

public sealed class AsyncMediatorGlobalSendWithInputPipeline<TId, TMiddlewareId>(
    IAsyncMediatorSendWithInput<TId> terminal,
    IMiddlewareIdFactory<TId, TMiddlewareId> middlewareIdFactory,
    IIndexedAsyncRegistry<TMiddlewareId, IEnumerable<IInOutMiddleware>> registry)
    : IAsyncMediatorSendWithInput<TId>
    where TId : notnull
{
    public async Task<TOut?> Send<TIn, TOut>(TIn input, TId? id = default,
        CancellationToken cancellationToken = default)
    {
        Func<TIn, CancellationToken, Task<TOut?>> next = (ctx, ct) => terminal.Send<TIn, TOut>(ctx, id, ct);

        var middlewareId = middlewareIdFactory.CreateInOut<TIn, TOut>(input, id);

        if (await registry.TryGet(middlewareId, out var middlewares, cancellationToken))
        {
            foreach (var middleware in middlewares
                         .Cast<IInOutMiddleware<TIn, TOut?>>()
                         .Reverse())
            {
                var current = next;
                next = (ctx, ct) => middleware.Invoke(ctx, current, ct);
            }
        }

        return await next(input, cancellationToken);
    }
}

public sealed class AsyncGroupPublishPipeline<TGroupId, THandlerId>(
    IIndexedAsyncRegistry<TGroupId, IEnumerable<THandlerId>> registry,
    IAsyncMediatorPublish<THandlerId> mediator) : IAsyncMediatorPublish<TGroupId>
    where TGroupId : notnull
    where THandlerId : notnull
{
    public async Task Publish(TGroupId? groupId = default, CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(groupId, out var group, cancellationToken))
            return;
        
        foreach (var id in group)
            await mediator.Publish(id, cancellationToken);
    }
}

public sealed class AsyncGroupPublishWithInputPipeline<TGroupId, THandlerId>(
    IIndexedAsyncRegistry<TGroupId, IEnumerable<THandlerId>> registry,
    IAsyncMediatorPublishWithInput<THandlerId> mediator)
    : IAsyncMediatorPublishWithInput<TGroupId>
    where TGroupId : notnull
    where THandlerId : notnull
{
    public async Task Publish<TIn>(TIn input, TGroupId? groupId = default,
        CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(groupId, out var group, cancellationToken))
            return;
        
        foreach (var id in group)
            await mediator.Publish(input, id, cancellationToken);
    }
}

public sealed class AsyncGroupSendPipeline<TGroupId, THandlerId>(
    IIndexedAsyncRegistry<TGroupId, IEnumerable<THandlerId>> registry,
    IAsyncMediatorSend<THandlerId> mediator) : IAsyncFanOutSend<TGroupId>
    where TGroupId : notnull
    where THandlerId : notnull
{
    public async IAsyncEnumerable<TOut?> Send<TOut>(TGroupId? groupId = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(groupId, out var group, cancellationToken))
            yield break;
        
        foreach (var id in group)
            yield return await mediator.Send<TOut>(id, cancellationToken);
    }
}

public sealed class AsyncGroupSendWithInputPipeline<TGroupId, THandlerId>(
    IIndexedAsyncRegistry<TGroupId, IEnumerable<THandlerId>> registry,
    IAsyncMediatorSendWithInput<THandlerId> mediator)
    : IAsyncFanOutSendWithInput<TGroupId>
    where TGroupId : notnull
    where THandlerId : notnull
{
    public async IAsyncEnumerable<TOut?> Send<TIn, TOut>(TIn input, TGroupId? groupId = default,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(groupId, out var group, cancellationToken))
            yield break;
        
        foreach (var id in group)
            yield return await mediator.Send<TIn, TOut>(input, id, cancellationToken);
    }
}

public sealed class AsyncGroupSendWithAggregatorPipeline<TGroupId>(
    IAsyncFanOutSend<TGroupId> fanOutSend,
    IIndexedAsyncRegistry<TGroupId, IAsyncFuncHandlerInOut> registry)
    : IAsyncMediatorSend<TGroupId>
    where TGroupId : notnull
{
    public async Task<TOut?> Send<TOut>(TGroupId? groupId = default, CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(groupId, out var aggregator, cancellationToken))
        {
            await foreach (var result in fanOutSend.Send<TOut>(groupId, cancellationToken))
                return result;
            
            return default;
        }

        if (aggregator is not IAsyncFuncHandler<IAsyncEnumerable<TOut?>, TOut?> handler)
        {
            await foreach (var result in fanOutSend.Send<TOut>(groupId, cancellationToken))
                return result;
            
            return default;
        }

        var results = fanOutSend.Send<TOut>(groupId, cancellationToken);
        return await handler.Handle(results, cancellationToken);
    }
}

public sealed class AsyncFanOutDefaultIdSend<TGroupId>(
    IAsyncFanOutSend<TGroupId> inner,
    TGroupId defaultId
) : IAsyncFanOutSend<TGroupId>, IAsyncFanOutSend
    where TGroupId : notnull
{
    private TGroupId Resolve(TGroupId? id) => id ?? defaultId;

    public IAsyncEnumerable<TOut?> Send<TOut>(TGroupId? groupId = default,
        CancellationToken cancellationToken = default)
        => inner.Send<TOut>(Resolve(groupId), cancellationToken);

    public IAsyncEnumerable<TOut?> Send<TOut>(CancellationToken cancellationToken = default)
        => inner.Send<TOut>(defaultId, cancellationToken);
}

public sealed class AsyncFanOutDefaultIdSendWithInput<TGroupId>(
    IAsyncFanOutSendWithInput<TGroupId> inner,
    TGroupId defaultId
) : IAsyncFanOutSendWithInput<TGroupId>, IAsyncFanOutSendWithInput
    where TGroupId : notnull
{
    private TGroupId Resolve(TGroupId? id) => id ?? defaultId;

    public IAsyncEnumerable<TOut?> Send<TIn, TOut>(TIn input, TGroupId? groupId = default,
        CancellationToken cancellationToken = default)
        => inner.Send<TIn, TOut>(input, Resolve(groupId), cancellationToken);

    public IAsyncEnumerable<TOut?> Send<TIn, TOut>(TIn input, CancellationToken cancellationToken = default)
        => inner.Send<TIn, TOut>(input, defaultId, cancellationToken);
}

public interface IAsyncFanOutSend<in TId> : IAsyncFanOutSendMark {
    IAsyncEnumerable<TOut?> Send<TOut>(TId? id = default, CancellationToken cancellationToken = default);
}

public interface IAsyncFanOutSend : IAsyncFanOutSendMark {
    IAsyncEnumerable<TOut?> Send<TOut>(CancellationToken cancellationToken = default);
}

public interface IAsyncFanOutSendWithInput<in TId> : IAsyncFanOutSendWithInputMark {
    IAsyncEnumerable<TOut?> Send<TIn, TOut>(TIn input, TId? id = default,
        CancellationToken cancellationToken = default);
}

public interface IAsyncFanOutSendWithInput : IAsyncFanOutSendWithInputMark {
    IAsyncEnumerable<TOut?> Send<TIn, TOut>(TIn input, CancellationToken cancellationToken = default);
}

public sealed class AsyncGroupSendWithInputAggregatorPipeline<TGroupId>(
    IAsyncFanOutSendWithInput<TGroupId> fanOutSendIn,
    IIndexedAsyncRegistry<TGroupId, IAsyncFuncHandlerInOut> registry)
    : IAsyncMediatorSendWithInput<TGroupId>
    where TGroupId : notnull
{
    public async Task<TOut?> Send<TIn, TOut>(TIn input, TGroupId? groupId = default,
        CancellationToken cancellationToken = default)
    {
        if (!await registry.TryGet(groupId, out var aggregator, cancellationToken))
        {
            await foreach (var result in fanOutSendIn.Send<TIn, TOut>(input, groupId, cancellationToken))
                return result;
            return default;
        }

        if (aggregator is not IAsyncFuncHandler<IAsyncEnumerable<TOut?>, TOut?> handler)
        {
            await foreach (var result in fanOutSendIn.Send<TIn, TOut>(input, groupId, cancellationToken))
                return result;
            return default;
        }

        var results = fanOutSendIn.Send<TIn, TOut>(input, groupId, cancellationToken);
        return await handler.Handle(results, cancellationToken);
    }
}
public sealed class AsyncMediatorPublishPipeline<TId>(
    IAsyncMediatorPublish<TId> terminal,
    IIndexedAsyncRegistry<TId, IEnumerable<IMiddleware>> registry)
    : IAsyncMediatorPublish<TId>
    where TId : notnull
{
    public async Task Publish(TId? id = default, CancellationToken cancellationToken = default)
    {
        Func<CancellationToken, Task> next = ct => terminal.Publish(id, ct);

        if (await registry.TryGet(id, out var middlewares, cancellationToken))
        {
            foreach (var middleware in middlewares.Reverse())
            {
                var current = next;
                next = ct => middleware.Invoke(current, ct);
            }
        }

        await next(cancellationToken);
    }
}

public sealed class AsyncMediatorPublishWithInputPipeline<TId>(
    IAsyncMediatorPublishWithInput<TId> terminal,
    IIndexedAsyncRegistry<TId, IEnumerable<IInMiddleware>> registry)
    : IAsyncMediatorPublishWithInput<TId>
    where TId : notnull
{
    public async Task Publish<TIn>(TIn input, TId? id = default, CancellationToken cancellationToken = default)
    {
        Func<TIn, CancellationToken, Task> next = (ctx, ct) => terminal.Publish(ctx, id, ct);

        if (await registry.TryGet(id, out var middlewares, cancellationToken))
        {
            foreach (var middleware in middlewares
                         .Cast<IInMiddleware<TIn>>()
                         .Reverse())
            {
                var current = next;
                next = (ctx, ct) => middleware.Invoke(ctx, current, ct);
            }
        }

        await next(input, cancellationToken);
    }
}

public sealed class AsyncMediatorSendPipeline<TId>(
    IAsyncMediatorSend<TId> terminal,
    IIndexedAsyncRegistry<TId, IEnumerable<IOutMiddleware>> registry)
    : IAsyncMediatorSend<TId>
    where TId : notnull
{
    public async Task<TOut?> Send<TOut>(TId? id = default, CancellationToken cancellationToken = default)
    {
        Func<CancellationToken, Task<TOut?>> next = ct => terminal.Send<TOut>(id, ct);

        if (await registry.TryGet(id, out var middlewares, cancellationToken))
        {
            foreach (var middleware in middlewares
                         .Cast<IOutMiddleware<TOut?>>()
                         .Reverse())
            {
                var current = next;
                next = ct => middleware.Invoke(current, ct);
            }
        }

        return await next(cancellationToken);
    }
}

public sealed class AsyncMediatorSendWithInputPipeline<TId>(
    IAsyncMediatorSendWithInput<TId> terminal,
    IIndexedAsyncRegistry<TId, IEnumerable<IInOutMiddleware>> registry)
    : IAsyncMediatorSendWithInput<TId>
    where TId : notnull
{
    public async Task<TOut?> Send<TIn, TOut>(TIn input, TId? id = default,
        CancellationToken cancellationToken = default)
    {
        Func<TIn, CancellationToken, Task<TOut?>> next = (ctx, ct) => terminal.Send<TIn, TOut>(ctx, id, ct);

        if (await registry.TryGet(id, out var middlewares, cancellationToken))
        {
            foreach (var middleware in middlewares
                         .Cast<IInOutMiddleware<TIn, TOut?>>()
                         .Reverse())
            {
                var current = next;
                next = (ctx, ct) => middleware.Invoke(ctx, current, ct);
            }
        }

        return await next(input, cancellationToken);
    }
}
public interface IAsyncMediatorMark : ISelfMark<IAsyncMediatorMark> {
}

public interface IAsyncMediatorPublishMark : ISelfMark<IAsyncMediatorPublishMark> {
}

public interface IAsyncMediatorPublishWithInputMark : ISelfMark<IAsyncMediatorPublishWithInputMark> {
}

public interface IAsyncMediatorSendMark : ISelfMark<IAsyncMediatorSendMark> {
}

public interface IAsyncMediatorSendWithInputMark : ISelfMark<IAsyncMediatorSendWithInputMark> {
}

public interface IAsyncFanOutMediatorMark : ISelfMark<IAsyncFanOutMediatorMark> {
}

public interface IAsyncFanOutSendMark : ISelfMark<IAsyncFanOutSendMark> {
}

public interface IAsyncFanOutSendWithInputMark : ISelfMark<IAsyncFanOutSendWithInputMark> {
}
