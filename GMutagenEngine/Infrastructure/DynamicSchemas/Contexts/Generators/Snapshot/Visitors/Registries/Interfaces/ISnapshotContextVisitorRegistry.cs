using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Registries.Interfaces
{
    public interface ISnapshotContextVisitorRegistry : IIndexedIndexedSyncStorage<Type, ISnapshotContextVisitor>
    {
    }
}