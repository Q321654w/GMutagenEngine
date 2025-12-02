using GMutagenEngine.Infrastructure.Logging;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async
{
    public static class MessageBrokerPresets
    {
        public static MessageBroker<TId> Default<TId>() where TId : notnull 
            => new(LoggerPresets.CreateConsoleLogger<MessageBroker<TId>>());
    }
}