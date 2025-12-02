using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Registries.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Registries.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Presets
{
    public static class LiveContextGeneratorPresets
    {
        public static LiveContextGenerator CreateDefault(
            ISchemaRegistry schemaRegistry,
            IContextRegistry contextRegistry,
            IReflectionValueFactory reflectionValueFactory,
            IDefaultValueFactory defaultValueFactory)
        {
            var storage = new LiveContextVisitorStorage();
            storage.AddDefaults(contextRegistry, schemaRegistry, reflectionValueFactory, defaultValueFactory);

            return new LiveContextGenerator(storage);
        }
    }
}