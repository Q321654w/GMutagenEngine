using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Registries.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Registries.Extensions
{
    public static class LiveContextRegistryExtensions
    {
        public static LiveContextVisitorStorage AddDefaults(this LiveContextVisitorStorage storage,
            IContextRegistry contextRegistry,
            ISchemaRegistry schemaRegistry,
            IReflectionValueFactory reflectionValueFactory,
            IDefaultValueFactory defaultValueFactory)
        {
            storage.Add(new ComplexObjectLiveContextVisitor(contextRegistry, reflectionValueFactory, defaultValueFactory, schemaRegistry));
            storage.Add(new CollectionLiveContextVisitor(contextRegistry, schemaRegistry));
            storage.Add(new DictionaryLiveVisitor(contextRegistry, schemaRegistry));

            return storage;
        }
    }
}