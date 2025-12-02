using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Registries.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Registries.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Presets
{
    public static class SnapshotContextGeneratorPresets
    {
        public static SnapshotContextGenerator CreateDefault(
            ISchemaRegistry schemaRegistry,
            IDefaultValueFactory defaultValueFactory)
        {
            var storage = new SnapshotContextVisitorStorage();
            storage.AddDefaults(schemaRegistry, defaultValueFactory);

            return new SnapshotContextGenerator(storage);
        }
    }
}