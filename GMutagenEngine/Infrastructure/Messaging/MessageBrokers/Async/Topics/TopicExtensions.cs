using GMutagenEngine.Infrastructure.Handlers.Async.Actions;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Subscriptions;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Topics
{
    public static class TopicSubscriptions
    {
        public static Task<ISubscription<TId>> Subscribe<TId, TEvent>(
            this ITopic<TId> topic,
            IAsyncActionHandler<TEvent> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.Subscribe(topic.Id, handler, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this ITopic<TId> topic,
            IAsyncActionHandler<object> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAny(topic.Id, handler, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this ITopic<TId> topic,
            IAsyncActionHandler handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAny(topic.Id, handler, cancellationToken);
        }

        public static Task<ISubscription<TId>> Subscribe<TId, TEvent>(
            this ITopic<TId> topic,
            Action<TEvent> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.Subscribe(topic.Id, handler, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this ITopic<TId> topic,
            Action<object> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAny(topic.Id, handler, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this ITopic<TId> topic,
            Action handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAny(topic.Id, handler, cancellationToken);
        }

        public static Task<ISubscription<TId>> Subscribe<TId, TEvent>(
            this ITopic<TId> topic,
            Func<TEvent, CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.Subscribe(topic.Id, handler, cancellationToken);
        }

        public static Task<ISubscription<TId>> Subscribe<TId>(
            this ITopic<TId> topic,
            Func<object, CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.Subscribe(topic.Id, handler, cancellationToken);
        }

        public static Task<ISubscription<TId>> SubscribeForAny<TId>(
            this ITopic<TId> topic,
            Func<CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAny(topic.Id, handler, cancellationToken);
        }
    }

    public static class TopicSubscriptionsAsDisposableAsync
    {
        public static Task<IAsyncDisposable> SubscribeAsDisposable<TId, TEvent>(
            this ITopic<TId> topic,
            IAsyncActionHandler<TEvent> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeAsDisposable(
                topic.Id,
                handler,
                cancellationToken);
        }

        public static Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            IAsyncActionHandler<object> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAnyAsDisposable(
                topic.Id,
                handler,
                cancellationToken);
        }

        public static Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            IAsyncActionHandler handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAnyAsDisposable(
                topic.Id,
                handler,
                cancellationToken);
        }

        public static Task<IAsyncDisposable> SubscribeAsDisposable<TId, TEvent>(
            this ITopic<TId> topic,
            Action<TEvent> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeAsDisposable(
                topic.Id,
                handler,
                cancellationToken);
        }

        public static Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            Action<object> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAnyAsDisposable(
                topic.Id,
                handler,
                cancellationToken);
        }

        public static Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            Action handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAnyAsDisposable(
                topic.Id,
                handler,
                cancellationToken);
        }

        public static Task<IAsyncDisposable> SubscribeAsDisposable<TId, TEvent>(
            this ITopic<TId> topic,
            Func<TEvent, CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeAsDisposable(
                topic.Id,
                handler,
                cancellationToken);
        }

        public static Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            Func<object, CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeAsDisposable(
                topic.Id,
                handler,
                cancellationToken);
        }

        public static Task<IAsyncDisposable> SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            Func<CancellationToken, Task> handler,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.SubscribeForAnyAsDisposable(
                topic.Id,
                handler,
                cancellationToken);
        }
    }


    public static class TopicPublishing
    {
        public static Task Publish<TId>(
            this ITopic<TId> topic,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.Publish(topic.Id, cancellationToken);
        }

        public static Task Publish<TId, TEvent>(
            this ITopic<TId> topic,
            TEvent data,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.Publish(topic.Id, data, cancellationToken);
        }

        public static Task PublishActualType<TId, TEvent>(
            this ITopic<TId> topic,
            TEvent data,
            CancellationToken cancellationToken = default)
        {
            return topic.MessageBroker.PublishActualType(topic.Id, data, cancellationToken);
        }
    }

    public static class TopicReplication
    {
        public static Task<ReplicationGroup<TId>> ReplicateTo<TId>(
            this ITopic<TId> source,
            CancellationToken cancellationToken = default,
            params ITopic<TId>[] targets)
        {
            var ids = targets.Select(t => t.Id).ToArray();

            return source.MessageBroker.Replicate(
                source.Id,
                cancellationToken,
                ids);
        }

        public static Task<ReplicationGroup<TId, TEvent>> ReplicateTo<TId, TEvent>(
            this ITopic<TId> source,
            CancellationToken cancellationToken = default,
            params ITopic<TId>[] targets)
        {
            var ids = targets.Select(t => t.Id).ToArray();

            return source.MessageBroker.Replicate<TId, TEvent>(
                source.Id,
                cancellationToken,
                ids);
        }
    }


    public static class TopicMerge
    {
        public static Task<MergeGroup<TId>> MergeFrom<TId>(
            this ITopic<TId> target,
            CancellationToken cancellationToken = default,
            params ITopic<TId>[] sources)
        {
            var ids = sources.Select(s => s.Id).ToArray();

            return target.MessageBroker.MergeTopics(
                target.Id,
                cancellationToken,
                ids);
        }

        public static Task<MergeGroup<TId, TEvent>> MergeFrom<TId, TEvent>(
            this ITopic<TId> target,
            CancellationToken cancellationToken = default,
            params ITopic<TId>[] sources)
        {
            var ids = sources.Select(s => s.Id).ToArray();

            return target.MessageBroker.MergeTopics<TId, TEvent>(
                target.Id,
                cancellationToken,
                ids);
        }
    }
}