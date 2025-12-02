namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Topics
{
    public interface ITopic<TId>
    {
        TId Id { get; set; }
        IMessageBroker<TId> MessageBroker { get; set; }
    }
}