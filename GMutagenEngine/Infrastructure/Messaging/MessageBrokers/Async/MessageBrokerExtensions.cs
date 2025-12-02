using GMutagenEngine.Infrastructure.Disposables.Async.Realizations;
using GMutagenEngine.Infrastructure.Handlers.Async.Actions;
using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Common;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Subscriptions;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async
{
    public static partial class MessageBrokerExtensions
    {
        public static Task<ISubscription<TId>> Subscribe<TId, TEvent>(
            this IMessageBroker<TId> messageBroker,
            TId topicId,
            IAsyncActionHandler<TEvent> handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = new Subscription<TId, TEvent>(topicId, handler);
            return messageBroker.Subscribe(subscription, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            IAsyncActionHandler<object> handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = new Subscription<TId, object>(topicId, handler);
            return broker.Subscribe(subscription, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            IAsyncActionHandler handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = new Subscription<TId>(topicId, handler);
            return broker.Subscribe(subscription, cancellationToken);
        }

        public static Task<ISubscription<TId>> Subscribe<TId, TEvent>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Action<TEvent> handler,
            CancellationToken cancellationToken = default)
        {
            var async = new AsyncOverSyncActionHandler<TEvent>(handler);
            return broker.Subscribe(topicId, async, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Action<object> handler,
            CancellationToken cancellationToken = default)
        {
            var async = new AsyncOverSyncActionHandler<object>(handler);
            return broker.Subscribe(topicId, async, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Action handler,
            CancellationToken cancellationToken = default)
        {
            var async = new AsyncOverSyncActionHandler(handler);
            return broker.SubscribeForAny(topicId, async, cancellationToken);
        }

        public static Task<ISubscription<TId>> Subscribe<TId, TEvent>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Func<TEvent, CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            var async = new AsyncActionHandler<TEvent>(handler);
            return broker.Subscribe(topicId, async, cancellationToken);
        }

        public static Task<ISubscription<TId>> Subscribe<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Func<object, CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            var async = new AsyncActionHandler<object>(handler);
            return broker.SubscribeForAny(topicId, async, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Func<CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            var async = new AsyncActionHandler(handler);
            return broker.SubscribeForAny(topicId, async, cancellationToken);
        }
    }
}

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async
{
    public static class MessageBrokerExtensionsAsDisposableAsync
    {
        public static async Task<IAsyncDisposable> SubscribeAsDisposable<TId, TEvent>(
            this IMessageBroker<TId> broker,
            TId topicId,
            IAsyncActionHandler<TEvent> handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = await broker.Subscribe(
                topicId,
                handler,
                cancellationToken);

            return new AsyncActionDisposable(() => broker.Unsubscribe(subscription));
        }

        public static async Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            IAsyncActionHandler<object> handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = await broker.SubscribeForAny(
                topicId,
                handler,
                cancellationToken);

            return new AsyncActionDisposable(() => broker.Unsubscribe(subscription));
        }

        public static async Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            IAsyncActionHandler handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = await broker.SubscribeForAny(
                topicId,
                handler,
                cancellationToken);

            return new AsyncActionDisposable(() => broker.Unsubscribe(subscription));
        }

        public static async Task<IAsyncDisposable> SubscribeAsDisposable<TId, TEvent>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Action<TEvent> handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = await broker.Subscribe(
                topicId,
                handler,
                cancellationToken);

            return new AsyncActionDisposable(() => broker.Unsubscribe(subscription));
        }

        public static async Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Action<object> handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = await broker.SubscribeForAny(
                topicId,
                handler,
                cancellationToken);

            return new AsyncActionDisposable(() => broker.Unsubscribe(subscription));
        }

        public static async Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Action handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = await broker.SubscribeForAny(
                topicId,
                handler,
                cancellationToken);

            return new AsyncActionDisposable(() => broker.Unsubscribe(subscription));
        }

        public static async Task<IAsyncDisposable> SubscribeAsDisposable<TId, TEvent>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Func<TEvent, CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = await broker.Subscribe(
                topicId,
                handler,
                cancellationToken);

            return new AsyncActionDisposable(() => broker.Unsubscribe(subscription));
        }

        public static async Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Func<object, CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = await broker.Subscribe(
                topicId,
                handler,
                cancellationToken);

            return new AsyncActionDisposable(() => broker.Unsubscribe(subscription));
        }

        public static async Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            Func<CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            var subscription = await broker.SubscribeForAny(
                topicId,
                handler,
                cancellationToken);

            return new AsyncActionDisposable(() => broker.Unsubscribe(subscription));
        }
    }
}


namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async
{
    public static partial class MessageBrokerExtensions
    {
        public static async Task Publish<TId, TEvent>(
            this IMessageBroker<TId> broker,
            TId topicId,
            TEvent? data = default,
            CancellationToken cancellationToken = default)
        {
            var context = new MessageContext<TId>(data, topicId, typeof(TEvent));
            await broker.Publish(context, cancellationToken);
        }

        public static async Task PublishActualType<TId, TEvent>(
            this IMessageBroker<TId> broker,
            TId topicId,
            TEvent? data = default,
            CancellationToken cancellationToken = default)
        {
            var context = new MessageContext<TId>(data, topicId);
            await broker.Publish(context, cancellationToken);
        }

        public static async Task Publish<TId>(
            this IMessageBroker<TId> broker,
            TId topicId,
            CancellationToken cancellationToken = default)
        {
            var context = new MessageContext<TId>(None.Obj, topicId, typeof(object));
            await broker.Publish(context, cancellationToken);
        }
    }
}

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async
{
    public static partial class MessageBrokerExtensions
    {
        public static async Task<ReplicationGroup<TId>> Replicate<TId>(
            this IMessageBroker<TId> broker,
            TId source,
            CancellationToken cancellationToken = default,
            params TId[] targets)
        {
            var group = new ReplicationGroup<TId>(
                broker,
                LoggerPresets.CreateConsoleLogger<ReplicationGroup<TId>>(),
                source,
                targets);

            await group.Init(cancellationToken);
            return group;
        }

        public static async Task<ReplicationGroup<TId, TEvent>> Replicate<TId, TEvent>(
            this IMessageBroker<TId> broker,
            TId source,
            CancellationToken cancellationToken = default,
            params TId[] targets)
        {
            var group = new ReplicationGroup<TId, TEvent>(
                broker,
                LoggerPresets.CreateConsoleLogger<ReplicationGroup<TId>>(),
                source,
                targets);

            await group.Init(cancellationToken);
            return group;
        }
    }
}

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async
{
    public static partial class MessageBrokerExtensions
    {
        public static async Task<MergeGroup<TId>> MergeTopics<TId>(
            this IMessageBroker<TId> broker,
            TId target,
            CancellationToken cancellationToken = default,
            params TId[] sources)
        {
            var group = new MergeGroup<TId>(
                broker,
                LoggerPresets.CreateConsoleLogger<MergeGroup<TId>>(),
                target,
                sources);

            await group.Init(cancellationToken);
            return group;
        }

        public static async Task<MergeGroup<TId, TEvent>> MergeTopics<TId, TEvent>(
            this IMessageBroker<TId> broker,
            TId target,
            CancellationToken cancellationToken = default,
            params TId[] sources)
        {
            var group = new MergeGroup<TId, TEvent>(
                broker,
                LoggerPresets.CreateConsoleLogger<MergeGroup<TId>>(),
                target,
                sources);

            await group.Init(cancellationToken);
            return group;
        }
    }
}
