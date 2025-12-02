using GMutagenEngine.Infrastructure.Storing.Storages.Async.Simple;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Topics
{
    public class Topic<TId>(TId id, IMessageBroker<TId> messageBroker) : ITopic<TId>
    {
        public TId Id { get; set; } = id;
        public IMessageBroker<TId> MessageBroker { get; set; } = messageBroker;
    }
    public class LifeTopic<TId> : Topic<TId>, IDisposable
    {
        private readonly IAsyncStorage<TId> _idStorage;

        public LifeTopic(TId id, IMessageBroker<TId> messageBroker, IAsyncStorage<TId> idStorage) : base(id, messageBroker)
        {
            _idStorage = idStorage;
            _idStorage.Add(id);
        }
        
        public void Dispose()
        {
            _idStorage.Remove(Id);
        }
    }
    
}