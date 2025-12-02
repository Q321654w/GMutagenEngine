using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Registries.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Registries.Extensions
{
    public static class SnapshotContextRegistryExtensions
    {
        public static SnapshotContextVisitorStorage AddDefaults(this SnapshotContextVisitorStorage storage,
            ISchemaRegistry schemaRegistry,
            IDefaultValueFactory defaultValueFactory)
        {
            storage.Add(new ComplexObjectSnapshotContextVisitor(defaultValueFactory));
            storage.Add(new CollectionSnapshotContextVisitor(schemaRegistry, defaultValueFactory));
            storage.Add(new DictionarySnapshotVisitor(schemaRegistry, defaultValueFactory));
            storage.Add(new PrimitiveSnapshotVisitor(defaultValueFactory));

            return storage;
        }
    }
}