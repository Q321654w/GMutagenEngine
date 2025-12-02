using GMutagenEngine.Infrastructure.Handlers.Async.Actions;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Subscriptions
{
    public class Subscription<TId>(TId topicId, IAsyncActionHandler handler)
        : ISubscription<TId>
    {
        public TId TopicId { get; } = topicId;
        private IAsyncActionHandler Handler { get; } = handler;
        public async Task Handle(object e) => await Handler.Handle();
    }
    
    public class Subscription<TId, TEvent>(
        TId topicId,
        IAsyncActionHandler<TEvent> handler)
        : ISubscription<TId>
    {
        public TId TopicId { get; } = topicId;
        private IAsyncActionHandler<TEvent> Handler { get; } = handler;

        public async Task Handle(object e)
        {
            if(e is TEvent eventData)
                await Handler.Handle(eventData);
        }
    }
}