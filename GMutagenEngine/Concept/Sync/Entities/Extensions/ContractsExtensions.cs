using GMutagenEngine.Concept.Sync.Entities.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Mediators.Sync;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async;
using GMutagenEngine.Infrastructure.ValueStorages;

namespace GMutagenEngine.Concept.Sync.Entities.Extensions
{
    public static class ContractsExtensions
    {
        public static IValueStorage ValueStorage(this IEntity entity)
        {
            return entity.GetService<IValueStorage>();
        }
    
        public static ISyncMediator<IId> Mediator(this IEntity entity)
        {
            return entity.GetService<ISyncMediator<IId>>();
        }
    
        public static ISyncMediatorHandlerRegistry<IId> MediatorHandlerRegistry(this IEntity entity)
        {
            return entity.GetService<ISyncMediatorHandlerRegistry<IId>>();
        }
    
        public static IMessageBroker<IId> MessageBroker(this IEntity entity)
        {
            return entity.GetService<IMessageBroker<IId>>();
        }
    
        /*public static IObjectIndex ObjectIndex(this IEntity entity)
        {
            return entity.GetService<IObjectIndex>();
        }*/
    }
}