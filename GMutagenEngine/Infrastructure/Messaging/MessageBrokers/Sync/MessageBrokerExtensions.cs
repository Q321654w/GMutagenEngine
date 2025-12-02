using GMutagenEngine.Infrastructure.Disposables.Sync.Realizations;
using GMutagenEngine.Infrastructure.Handlers.Async.Actions;
using GMutagenEngine.Infrastructure.Handlers.Sync.Actions;
using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Common;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Subscriptions;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync;

public static partial class MessageBrokerExtensions
{
    public static ISubscription<TId> Subscribe<TId, TEvent>(
        this IMessageBroker<TId> messageBroker,
        TId topicId,
        ISyncActionHandler<TEvent> handler)
    {
        var subscription = new Subscription<TId, TEvent>(topicId, handler);
        return messageBroker.Subscribe(subscription);
    }

    public static ISubscription<TId> SubscribeForAny<TId>(
        this IMessageBroker<TId> broker,
        TId topicId,
        ISyncActionHandler<object> handler)
    {
        var subscription = new Subscription<TId, object>(topicId, handler);
        return broker.Subscribe(subscription);
    }

    public static ISubscription<TId> SubscribeForAny<TId>(
        this IMessageBroker<TId> broker,
        TId topicId,
        ISyncActionHandler handler)
    {
        var subscription = new Subscription<TId>(topicId, handler);
        return broker.Subscribe(subscription);
    }

    public static ISubscription<TId> Subscribe<TId, TEvent>(
        this IMessageBroker<TId> broker,
        TId topicId,
        Action<TEvent> handler)
    {
        var sync = new SyncActionHandler<TEvent>(handler);
        return broker.Subscribe(topicId, sync);
    }

    public static ISubscription<TId> SubscribeForAny<TId>(
        this IMessageBroker<TId> broker,
        TId topicId,
        Action<object> handler)
    {
        var sync = new SyncActionHandler<object>(handler);
        return broker.Subscribe(topicId, sync);
    }

    public static ISubscription<TId> SubscribeForAny<TId>(
        this IMessageBroker<TId> broker,
        TId topicId,
        Action handler)
    {
        var sync = new SyncActionHandler(handler);
        return broker.SubscribeForAny(topicId, sync);
    }
}

public static class MessageBrokerExtensionsAsDisposable
{
    public static IDisposable SubscribeAsDisposable<TId, TEvent>(
        this IMessageBroker<TId> broker,
        TId topicId,
        ISyncActionHandler<TEvent> handler)
    {
        var subscription = broker.Subscribe(topicId, handler);
        return new SyncActionDisposable(() => broker.Unsubscribe(subscription));
    }

    public static IDisposable SubscribeForAnyAsDisposable<TId>(
        this IMessageBroker<TId> broker,
        TId topicId,
        ISyncActionHandler<object> handler)
    {
        var subscription = broker.SubscribeForAny(topicId, handler);
        return new SyncActionDisposable(() => broker.Unsubscribe(subscription));
    }

    public static IDisposable SubscribeForAnyAsDisposable<TId>(
        this IMessageBroker<TId> broker,
        TId topicId,
        ISyncActionHandler handler)
    {
        var subscription = broker.SubscribeForAny(topicId, handler);
        return new SyncActionDisposable(() => broker.Unsubscribe(subscription));
    }

    public static IDisposable SubscribeAsDisposable<TId, TEvent>(
        this IMessageBroker<TId> broker,
        TId topicId,
        Action<TEvent> handler)
    {
        var subscription = broker.Subscribe(topicId, handler);
        return new SyncActionDisposable(() => broker.Unsubscribe(subscription));
    }

    public static IDisposable SubscribeForAnyAsDisposable<TId>(
        this IMessageBroker<TId> broker,
        TId topicId,
        Action<object> handler)
    {
        var subscription = broker.SubscribeForAny(topicId, handler);
        return new SyncActionDisposable(() => broker.Unsubscribe(subscription));
        ;
    }

    public static IDisposable SubscribeForAnyAsDisposable<TId>(
        this IMessageBroker<TId> broker,
        TId topicId,
        Action handler)
    {
        var subscription = broker.SubscribeForAny(topicId, handler);
        return new SyncActionDisposable(() => broker.Unsubscribe(subscription));
    }
}

public static partial class MessageBrokerExtensions
{
    public static void Publish<TId, TEvent>(
        this IMessageBroker<TId> broker,
        TId topicId,
        TEvent? data = default)
    {
        var context = new MessageContext<TId>(data, topicId, typeof(TEvent));
        broker.Publish(context);
    }

    public static void PublishActualType<TId, TEvent>(
        this IMessageBroker<TId> broker,
        TId topicId,
        TEvent? data = default)
    {
        var context = new MessageContext<TId>(data, topicId);
        broker.Publish(context);
    }

    public static void Publish<TId>(
        this IMessageBroker<TId> broker,
        TId topicId)
    {
        var context = new MessageContext<TId>(None.Obj, topicId, typeof(object));
        broker.Publish(context);
    }
}

public static partial class MessageBrokerExtensions
{
    public static ReplicationGroup<TId> Replicate<TId>(
        this IMessageBroker<TId> broker,
        TId source,
        params TId[] targets)
    {
        var group = new ReplicationGroup<TId>(
            broker,
            LoggerPresets.CreateConsoleLogger<ReplicationGroup<TId>>(),
            source,
            targets);

        group.Init();
        return group;
    }

    public static ReplicationGroup<TId, TEvent> Replicate<TId, TEvent>(
        this IMessageBroker<TId> broker,
        TId source,
        params TId[] targets)
    {
        var group = new ReplicationGroup<TId, TEvent>(
            broker,
            LoggerPresets.CreateConsoleLogger<ReplicationGroup<TId>>(),
            source,
            targets);

        group.Init();
        return group;
    }
}

public static partial class MessageBrokerExtensions
{
    public static MergeGroup<TId> MergeTopics<TId>(
        this IMessageBroker<TId> broker,
        TId target,
        params TId[] sources)
    {
        var group = new MergeGroup<TId>(
            broker,
            LoggerPresets.CreateConsoleLogger<MergeGroup<TId>>(),
            target,
            sources);

        group.Init();
        return group;
    }

    public static MergeGroup<TId, TEvent> MergeTopics<TId, TEvent>(
        this IMessageBroker<TId> broker,
        TId target,
        params TId[] sources)
    {
        var group = new MergeGroup<TId, TEvent>(
            broker,
            LoggerPresets.CreateConsoleLogger<MergeGroup<TId>>(),
            target,
            sources);

        group.Init();
        return group;
    }
}