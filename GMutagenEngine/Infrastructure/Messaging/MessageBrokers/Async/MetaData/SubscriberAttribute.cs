namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.MetaData
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SubscriberAttribute(string topic) : Attribute
    {
        public string Topic { get; } = topic;
    }
}