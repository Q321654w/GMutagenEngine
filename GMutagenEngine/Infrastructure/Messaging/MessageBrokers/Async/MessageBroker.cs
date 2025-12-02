using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Subscriptions;
using GMutagenEngine.Infrastructure.Middlewares.Async.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Simple;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async
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

        public Task<ISubscription<TId>> Subscribe(ISubscription<TId> subscription, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            subscriptions.Add(subscription.TopicId, subscription);
            return Task.FromResult(subscription);
        }

        public async Task Publish(MessageContext<TId> context, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var pipeline = (CancellationToken ct) => DispatchEvent(context, ct);

            foreach (var middleware in middlewares)
            {
                var next = pipeline;
                pipeline = ct => middleware.InvokeAsync(context, next, ct);
            }

            logger.LogDebug($"Publishing event of type {context.EventDataType.Name} to topic {context.TopicId}");
            await pipeline(cancellationToken);
        }

        private async Task DispatchEvent(MessageContext<TId> context, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var matched = subscriptions.Get(context.TopicId);
            await matched.Handle(context);
        }
        
        public Task Unsubscribe(ISubscription<TId> subscription, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            subscriptions.Remove(subscription.TopicId);
            logger.LogDebug($"{subscription} unsubscribed from topic {subscription.TopicId}");
            
            return Task.CompletedTask;
        }
    }
}