using GMutagenEngine.Infrastructure.Disposables.Sync.Realizations;
using GMutagenEngine.Infrastructure.Handlers.Sync.Actions;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Subscriptions;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Topics
{
    public static class TopicSubscriptions
    {
        public static ISubscription<TId> Subscribe<TId, TEvent>(
            this ITopic<TId> topic,
            ISyncActionHandler<TEvent> handler)
        {
            return topic.MessageBroker.Subscribe(topic.Id, handler);
        }

        public static ISubscription<TId> SubscribeForAny<TId>(
            this ITopic<TId> topic,
            ISyncActionHandler<object> handler)
        {
            return topic.MessageBroker.SubscribeForAny(topic.Id, handler);
        }

        public static ISubscription<TId> SubscribeForAny<TId>(
            this ITopic<TId> topic,
            ISyncActionHandler handler)
        {
            return topic.MessageBroker.SubscribeForAny(topic.Id, handler);
        }

        public static ISubscription<TId> Subscribe<TId, TEvent>(
            this ITopic<TId> topic,
            Action<TEvent> handler)
        {
            return topic.MessageBroker.Subscribe(topic.Id, handler);
        }

        public static ISubscription<TId> SubscribeForAny<TId>(
            this ITopic<TId> topic,
            Action<object> handler)
        {
            return topic.MessageBroker.SubscribeForAny(topic.Id, handler);
        }

        public static ISubscription<TId> SubscribeForAny<TId>(
            this ITopic<TId> topic,
            Action handler)
        {
            return topic.MessageBroker.SubscribeForAny(topic.Id, handler);
        }
    } 
    
    public static class TopicSubscriptionsAsDisposable
    {
        public static IDisposable SubscribeAsDisposable<TId, TEvent>(
            this ITopic<TId> topic,
            ISyncActionHandler<TEvent> handler)
        {
            var subscription = topic.MessageBroker.Subscribe(topic.Id, handler);
            return new SyncActionDisposable(() => topic.MessageBroker.Unsubscribe(subscription));
        }

        public static IDisposable SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            ISyncActionHandler<object> handler)
        {
            var subscription = topic.MessageBroker.SubscribeForAny(topic.Id, handler);
            return new SyncActionDisposable(() => topic.MessageBroker.Unsubscribe(subscription));
        }

        public static IDisposable SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            ISyncActionHandler handler)
        {
            var subscription = topic.MessageBroker.SubscribeForAny(topic.Id, handler);
            return new SyncActionDisposable(() => topic.MessageBroker.Unsubscribe(subscription));
        }

        public static IDisposable SubscribeAsDisposable<TId, TEvent>(
            this ITopic<TId> topic,
            Action<TEvent> handler)
        {
            var subscription = topic.MessageBroker.Subscribe(topic.Id, handler);
            return new SyncActionDisposable(() => topic.MessageBroker.Unsubscribe(subscription));
        }

        public static IDisposable SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            Action<object> handler)
        {
            var subscription = topic.MessageBroker.SubscribeForAny(topic.Id, handler);
            return new SyncActionDisposable(() => topic.MessageBroker.Unsubscribe(subscription));;
        }

        public static IDisposable SubscribeForAnyAsDisposable<TId>(
            this ITopic<TId> topic,
            Action handler)
        {
            var subscription = topic.MessageBroker.SubscribeForAny(topic.Id, handler);
            return new SyncActionDisposable(() => topic.MessageBroker.Unsubscribe(subscription));
        }
    }

    public static class TopicPublishing
    {
        public static void Publish<TId>(
            this ITopic<TId> topic)
        {
            topic.MessageBroker.Publish(topic.Id);
        }

        public static void Publish<TId, TEvent>(
            this ITopic<TId> topic,
            TEvent data)
        {
            topic.MessageBroker.Publish(topic.Id, data);
        }

        public static void PublishActualType<TId, TEvent>(
            this ITopic<TId> topic,
            TEvent data)
        {
            topic.MessageBroker.PublishActualType(topic.Id, data);
        }
    }

    public static class TopicReplication
    {
        public static ReplicationGroup<TId> ReplicateTo<TId>(
            this ITopic<TId> source,
            params ITopic<TId>[] targets)
        {
            var ids = targets.Select(t => t.Id).ToArray();

            return source.MessageBroker.Replicate(
                source.Id,
                ids);
        }

        public static ReplicationGroup<TId, TEvent> ReplicateTo<TId, TEvent>(
            this ITopic<TId> source,
            params ITopic<TId>[] targets)
        {
            var ids = targets.Select(t => t.Id).ToArray();

            return source.MessageBroker.Replicate<TId, TEvent>(
                source.Id,
                ids);
        }
    }


    public static class TopicMerge
    {
        public static MergeGroup<TId> MergeFrom<TId>(
            this ITopic<TId> target,
            params ITopic<TId>[] sources)
        {
            var ids = sources.Select(s => s.Id).ToArray();

            return target.MessageBroker.MergeTopics(
                target.Id,
                ids);
        }

        public static MergeGroup<TId, TEvent> MergeFrom<TId, TEvent>(
            this ITopic<TId> target,
            params ITopic<TId>[] sources)
        {
            var ids = sources.Select(s => s.Id).ToArray();

            return target.MessageBroker.MergeTopics<TId, TEvent>(
                target.Id,
                ids);
        }
    }
}