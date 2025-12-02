using GMutagenEngine.Infrastructure.Handlers.Sync.Actions;
using GMutagenEngine.Infrastructure.Logging;

namespace GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Sync.Handling
{
    public class ReplicateHandler<TId>(IMessageBroker<TId> bus, ILogger logger, params TId[] targets)
        : ISyncActionHandler<object>
    {
        public void Handle(object data)
        {
            logger.LogDebug($"Replicating event {data.GetType().Name} to topic {targets}");
            foreach (var target in targets)
                 bus.Publish(target, data);
        }
    } 
    
    public class ReplicateHandler<TId, TData>(IMessageBroker<TId> bus, ILogger logger, params TId[] targets)
        : ISyncActionHandler<TData>
    {
        public void Handle(TData data)
        {
            logger.LogDebug($"Replicating event {data.GetType().Name} to topic {targets}");
            foreach (var target in targets)
                bus.Publish(target, data);
        }
    }
}