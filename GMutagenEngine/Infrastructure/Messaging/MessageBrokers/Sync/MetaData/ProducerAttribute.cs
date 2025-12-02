namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.MetaData
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ProducerAttribute(string topic) : Attribute
    {
        public string Topic { get; } = topic;
    }
}