using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Subscriptions;
using GMutagenEngine.Infrastructure.Middlewares.Sync.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Simple;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync
{
    public class MessageBroker<TId>(List<IMiddleware<MessageContext<TId>>> middlewares, ILogger<MessageBroker<TId>> logger, IIndexedIndexedSyncStorage<TId, ISubscription<TId>> subscriptions)
        : IMessageBroker<TId> where TId : notnull
    {
        public MessageBroker(ILogger<MessageBroker<TId>> logger, IIndexedIndexedSyncStorage<TId, ISubscription<TId>> subscriptions, ISyncStorage<TId> topicIdStorage) : this(new(), logger, subscriptions)
        {
        }
        
        public MessageBroker(List<IMiddleware<MessageContext<TId>>> middlewares, ILogger<MessageBroker<TId>> logger) 
            : this(middlewares, logger, new InMemoryIndexedSyncStorage<TId, ISubscription<TId>>())
        {
        }
        
        public MessageBroker(ILogger<MessageBroker<TId>> logger) : this(new(), logger)
        {
        }

        public ISubscription<TId> Subscribe(ISubscription<TId> subscription)
        {
            subscriptions.Add(subscription.TopicId, subscription);
            return subscription;
        }

        public void Publish(MessageContext<TId> context)
        {
            var pipeline = () => DispatchEvent(context);

            foreach (var middleware in middlewares)
            {
                var next = pipeline;
                pipeline = () => middleware.Invoke(context, next);
            }

            logger.LogDebug($"Publishing event of type {context.EventDataType.Name} to topic {context.TopicId}");
            pipeline();
        }

        private void DispatchEvent(MessageContext<TId> context)
        {
            var matched = subscriptions.Get(context.TopicId);
            matched.Handle(context);
        }
        
        public void Unsubscribe(ISubscription<TId> subscription)
        {
            subscriptions.Remove(subscription.TopicId);
            logger.LogDebug($"{subscription} unsubscribed from topic {subscription.TopicId}");
        }
    }
}