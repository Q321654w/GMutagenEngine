using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Logging;
using GMutagenEngine.Infrastructure.Mediators.Sync;
using GMutagenEngine.Infrastructure.Messaging.MessageBrokers.Async;
using GMutagenEngine.Infrastructure.Middlewares.Async.Realizations;

namespace GMutagenEngine
{
    public class Boot
    {
        public void Start()
        {
            var mediatorHandlerRegistry = new SyncMediatorHandlerRegistry();
            var mediator = new SyncSimpleMediator<IId>(mediatorHandlerRegistry);
        
            var messageBroker =
                new MessageBroker<IId>(
                    [new LoggingMiddleware<MessageContext<IId>>(new Logger<LoggingMiddleware<IId>>())],
                    new Logger<MessageBroker<IId>>());

            /*var schemaBuilder = new SchemaBuilder();*/
        }
    }
}