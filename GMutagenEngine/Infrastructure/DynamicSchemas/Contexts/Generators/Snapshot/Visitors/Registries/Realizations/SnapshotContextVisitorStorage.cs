using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Registries.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Registries.Realizations
{
    public class SnapshotContextVisitorStorage : InMemoryIndexedSyncStorage<Type, ISnapshotContextVisitor>, ISnapshotContextVisitorRegistry
    {
        public void Add(ISnapshotContextVisitor visitor)
        {
            foreach (var type in visitor.CanVisitTypes())
                Add(type, visitor);
        }
    }
}