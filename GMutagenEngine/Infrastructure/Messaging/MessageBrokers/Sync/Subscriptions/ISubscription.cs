namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Subscriptions
{
    public interface ISubscription<out TId>
    {
        TId TopicId { get; }
        void Handle(object e);
    }
}