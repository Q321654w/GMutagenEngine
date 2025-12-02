using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Subscriptions;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync
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
        public ISubscription<TId> Subscribe(ISubscription<TId> subscription)
        {
            var disposable = inner.Subscribe(subscription);
            PublishMetaSubscribed(subscription.TopicId, subscription);
            return disposable;
        }

        public void Publish(MessageContext<TId> context)
        {
            try
            {
                inner.Publish(context);
                PublishMetaPublished(context.TopicId, context.EventDataType, context.EventData, true);
            }
            catch (Exception ex)
            {
                PublishMetaError(context.TopicId, context.EventDataType, ex, "Publish failed");
                throw;
            }
        }

        public void Unsubscribe(ISubscription<TId> subscription)
        {
            inner.Unsubscribe(subscription);
            PublishMetaUnsubscribed(subscription.TopicId, subscription);
        }

        #region Meta-events

        private void PublishMetaSubscribed(TId topicId, ISubscription<TId> subscription)
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
                inner.Publish(metaTopicSubscribed, metaEvent);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Failed to publish subscription meta event: {ex}");
            }
        }

        private void PublishMetaUnsubscribed(TId topicId, ISubscription<TId> subscription)
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
                inner.Publish(metaTopicUnsubscribed, metaEvent);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Failed to publish unsubscription meta event: {ex}");
            }
        }

        private void PublishMetaPublished(TId topicId, Type? eventType, object? eventData, bool success)
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
                inner.Publish(metaTopicPublished, metaEvent);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Failed to publish published meta event: {ex}");
            }
        }

        private void PublishMetaError(TId topicId, Type? eventType, Exception error, string context)
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
                inner.Publish(metaTopicError, metaEvent);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Failed to publish error meta event: {ex}");
            }
        }

        #endregion
    }
}