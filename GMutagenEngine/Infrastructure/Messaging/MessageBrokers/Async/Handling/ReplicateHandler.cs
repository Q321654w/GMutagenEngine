using GMutagenEngine.Infrastructure.Handlers.Async.Actions;
using GMutagenEngine.Infrastructure.Logging;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async.Handling
{
    public class ReplicateHandler<TId>(IMessageBroker<TId> bus, ILogger logger, params TId[] targets)
        : IAsyncActionHandler<object>
    {
        public async Task Handle(object data, CancellationToken cancellationToken = default)
        {
            logger.LogDebug($"Replicating event {data.GetType().Name} to topic {targets}");
            foreach (var target in targets)
                await bus.Publish(target, data, cancellationToken);
        }
    } 
    
    public class ReplicateHandler<TId, TData>(IMessageBroker<TId> bus, ILogger logger, params TId[] targets)
        : IAsyncActionHandler<TData>
    {
        public async Task Handle(TData data, CancellationToken cancellationToken = default)
        {
            logger.LogDebug($"Replicating event {data.GetType().Name} to topic {targets}");
            foreach (var target in targets)
                await bus.Publish(target, data, cancellationToken);
        }
    }
}