namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Subscriptions
{
    public interface ISubscription<out TId>
    {
        TId TopicId { get; }
        Task Handle(object e);
    }
}