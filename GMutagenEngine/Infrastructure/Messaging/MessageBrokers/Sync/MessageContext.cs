
namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync
{
    public class MessageContext<TId>(object eventData, TId topicId, Type eventDataType)
    {
        public object EventData { get; } = eventData;
        public Type EventDataType { get; } = eventDataType;
        public TId TopicId { get; } = topicId;

        public MessageContext(object eventData, TId topicId) : this(eventData, topicId, eventData.GetType())
        {
        }
    }
}