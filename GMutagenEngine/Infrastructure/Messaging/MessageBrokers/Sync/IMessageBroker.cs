using GMutagenEngine.Concept.Sync.Services.Interfaces;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Subscriptions;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync
{
    public interface IMessageBroker<TId> : IService
    {
        ISubscription<TId> Subscribe(ISubscription<TId> subscription);
        void Publish(MessageContext<TId> context);
        void Unsubscribe(ISubscription<TId> subscription);
    }
}