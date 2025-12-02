using GMutagenEngine.Concept.Sync.Services.Interfaces;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Subscriptions;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async
{
    public interface IMessageBroker<TId> : IService
    {
        Task<ISubscription<TId>> Subscribe(ISubscription<TId> subscription, CancellationToken cancellationToken = default);
        Task Publish(MessageContext<TId> context, CancellationToken cancellationToken = default);
        Task Unsubscribe(ISubscription<TId> subscription, CancellationToken cancellationToken = default);
    }
}