namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Topics
{
    public interface ITopic<TId>
    {
        TId Id { get; set; }
        IMessageBroker<TId> MessageBroker { get; set; }
    }
}