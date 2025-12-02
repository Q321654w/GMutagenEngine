using GMutagenEngine.Infrastructure.Handlers.Sync.Actions;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Subscriptions
{
    public class Subscription<TId>(TId topicId, ISyncActionHandler handler)
        : ISubscription<TId>
    {
        public TId TopicId { get; } = topicId;
        private ISyncActionHandler Handler { get; } = handler;
        public void Handle(object e) => Handler.Handle();
    }
    
    public class Subscription<TId, TEvent>(
        TId topicId,
        ISyncActionHandler<TEvent> handler)
        : ISubscription<TId>
    {
        public TId TopicId { get; } = topicId;
        private ISyncActionHandler<TEvent> Handler { get; } = handler;

        public void Handle(object e)
        {
            if(e is TEvent eventData)
                 Handler.Handle(eventData);
        }
    }
}