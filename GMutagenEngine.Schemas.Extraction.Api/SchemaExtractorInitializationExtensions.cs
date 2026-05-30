using System.Collections;
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
        var dependencies = SyncMediatorInMemoryDependenciesFactory.Create<Type, Type>(
            out var handlerRegistry,
            out var middlewareRegistry);

        var plan = new SyncMediatorPipelinePlanBuilder<Type, Type>()
            .UseLocalMiddlewares()
            .WithDefaultId(typeof(Null))
            .Build();

        var assembly = new SyncMediatorPipelineAssembler<Type, Type>()
            .Assemble(dependencies, plan);

        var mediator = assembly.MediatorWithDefaultId
            ?? throw new InvalidOperationException("Default-id mediator is not configured.");

        handlerRegistry.AddDefaultHandlers(mediator, assembly.TypedMediator, handlerRegistry);
        middlewareRegistry.AddDefaultMiddlewares(mediator);

        return mediator;
    }

    public static ISchemaExtractor CreateDefault()
    {
        var mediator = CreateDefaultMediator();
        var schemaExtractor = new SchemaExtractor(mediator);

        return schemaExtractor;
    }
}
