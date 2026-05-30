using System.Collections;
using GMutagenEngine.Handlers.Actions.Intarfeces;
using GMutagenEngine.Handlers.Actions.Interfaces;
using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Mediators;
using GMutagenEngine.Middlewares.Sync.Interfaces;
using GMutagenEngine.Storing.Storages.Sync.Indexed;
using Types;
using Utils;

namespace GMutagenEngine.Schemas.Extraction.Api;

public static class SchemaExtractorInitializationExtensions
{
    public static IIndexedSyncStorage<Type, ISyncFuncHandlerInOut> AddDefaultHandlers(
        this IIndexedSyncStorage<Type, ISyncFuncHandlerInOut> mediatorRegistry,
        ISyncMediator mediator,
        ISyncMediator<Type> typedMediator,
        InMemoryIndexedSyncStorage<Type, ISyncFuncHandlerInOut> handlerRegistry)
    {
        var priorityMap = new Dictionary<Type, int>
        {
            { typeof(IDictionary), -1 }
        };
        var typePriorityRegistry = new PriorityRegistry<Type, int>(priorityMap);

        mediatorRegistry.Add(Framework.Primitive, new PrimitiveSchemaHandler());
        mediatorRegistry.Add(Framework.Collections, new CollectionSchemaHandler());
        mediatorRegistry.Add(Framework.Dictionaries, new DictionarySchemaHandler());
        mediatorRegistry.Add(Framework.Object, new ObjectSchemaHandler(mediator));
        mediatorRegistry.Add(Framework.Null, new SchemaRouter(handlerRegistry, typedMediator, typePriorityRegistry));

        return mediatorRegistry;
    }

    public static IIndexedSyncStorage<Type, IEnumerable<IInOutMiddleware>> AddDefaultMiddlewares(
        this IIndexedSyncStorage<Type, IEnumerable<IInOutMiddleware>> mediatorRegistry,
        ISyncMediator mediator)
    {
        mediatorRegistry.Add(Framework.Dictionaries, new DynamicDictionaryBehavior(mediator));
        mediatorRegistry.Add(Framework.Collections, new DynamicCollectionBehavior(mediator));

        return mediatorRegistry;
    }

    public static ISyncMediator CreateDefaultMediator()
    {
        var publishStorage = new InMemoryIndexedSyncStorage<Type, ISyncActionHandler>();
        var publishWithInputStorage = new InMemoryIndexedSyncStorage<Type, ISyncActionHandlerIn>();
        var sendStorage = new InMemoryIndexedSyncStorage<Type, ISyncFuncHandlerOut>();
        var sendWithInputStorage = new InMemoryIndexedSyncStorage<Type, ISyncFuncHandlerInOut>();

        var publish = new SyncMediatorPublish<Type>(publishStorage);
        var publishWithInput = new SyncPublishWithInput<Type>(publishWithInputStorage);
        var send = new SyncSend<Type>(sendStorage);
        var sendWithInput = new SyncSendWithInput<Type>(sendWithInputStorage);

        var sendWithInputMiddlewareStorage = new InMemoryIndexedSyncStorage<Type, IEnumerable<IInOutMiddleware>>();
        var sendWithInputWithPipeline = new SyncMediatorSendWithInputPipeline<Type>(
            sendWithInput,
            sendWithInputMiddlewareStorage);

        var typedMediator = new SyncMediator<Type>(
            publish,
            publishWithInput,
            send,
            sendWithInputWithPipeline);

        var publishDefaultId = new SyncDefaultIdPublish<Type>(typedMediator, typeof(Null));
        var publishWithInputDefaultId = new SyncDefaultIdPublishWithInput<Type>(typedMediator, typeof(Null));
        var sendDefaultId = new SyncDefaultIdSend<Type>(typedMediator, typeof(Null));
        var sendWithInputDefaultId = new SyncDefaultIdSendWithInput<Type>(typedMediator, typeof(Null));

        var mediator = new SyncMediator(publishDefaultId, publishWithInputDefaultId,
            sendDefaultId, sendWithInputDefaultId);

        sendWithInputStorage.AddDefaultHandlers(mediator, typedMediator, sendWithInputStorage);

        sendWithInputMiddlewareStorage.AddDefaultMiddlewares(mediator);

        return mediator;
    }

    public static ISchemaExtractor CreateDefault()
    {
        var mediator = CreateDefaultMediator();
        var schemaExtractor = new SchemaExtractor(mediator);

        return schemaExtractor;
    }
}