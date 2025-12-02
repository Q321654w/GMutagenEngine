using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Subscriptions;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async
{
    public class MetaEventMessageBrokerDecorator<TId>(
        IMessageBroker<TId> inner,
        ILogger<MetaEventMessageBrokerDecorator<TId>> logger,
        TId metaTopicSubscribed,
        TId metaTopicUnsubscribed,
        TId metaTopicPublished,
        TId metaTopicError) 
        : IMessageBroker<TId>
    {
        public Task<ISubscription<TId>> Subscribe(ISubscription<TId> subscription, CancellationToken cancellationToken = default)
        {
            var disposable = inner.Subscribe(subscription, cancellationToken);
            _ = PublishMetaSubscribed(subscription.TopicId, subscription);
            return disposable;
        }

        public async Task Publish(MessageContext<TId> context, CancellationToken cancellationToken = default)
        {
            try
            {
                await inner.Publish(context, cancellationToken);
                await PublishMetaPublished(context.TopicId, context.EventDataType, context.EventData, true);
            }
            catch (Exception ex)
            {
                await PublishMetaError(context.TopicId, context.EventDataType, ex, "Publish failed");
                throw;
            }
        }
    
        public async Task Unsubscribe(ISubscription<TId> subscription, CancellationToken cancellationToken = default)
        {
            await inner.Unsubscribe(subscription, cancellationToken);
            _ = PublishMetaUnsubscribed(subscription.TopicId, subscription);
        }

        #region Meta-events

        private async Task PublishMetaSubscribed(TId topicId, ISubscription<TId> subscription)
        {
            try
            {
                var metaEvent = new
                {
                    TId = topicId,
                    HandlerType = subscription.ToString(),
                    Timestamp = DateTime.UtcNow,
                    Action = "subscribed"
                };
                await inner.Publish(metaTopicSubscribed, metaEvent);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Failed to publish subscription meta event: {ex}");
            }
        }

        private async Task PublishMetaUnsubscribed(TId topicId, ISubscription<TId> subscription)
        {
            try
            {
                var metaEvent = new
                {
                    TId = topicId,
                    HandlerType = subscription.ToString(),
                    Timestamp = DateTime.UtcNow,
                    Action = "unsubscribed"
                };
                await inner.Publish(metaTopicUnsubscribed, metaEvent);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Failed to publish unsubscription meta event: {ex}");
            }
        }

        private async Task PublishMetaPublished(TId topicId, Type? eventType, object? eventData, bool success)
        {
            try
            {
                var metaEvent = new
                {
                    TId = topicId,
                    EventType = eventType?.Name ?? Literals.NONE,
                    EventDataSummary = eventData?.ToString() ?? Literals.NULL,
                    Timestamp = DateTime.UtcNow,
                    Success = success
                };
                await inner.Publish(metaTopicPublished, metaEvent);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Failed to publish published meta event: {ex}");
            }
        }

        private async Task PublishMetaError(TId topicId, Type? eventType, Exception error, string context)
        {
            try
            {
                var metaEvent = new
                {
                    TId = topicId,
                    EventType = eventType?.Name,
                    ErrorMessage = error.Message,
                    ErrorType = error.GetType().Name,
                    Context = context,
                    Timestamp = DateTime.UtcNow,
                    error.StackTrace
                };
                await inner.Publish(metaTopicError, metaEvent);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Failed to publish error meta event: {ex}");
            }
        }

        #endregion
    }
}