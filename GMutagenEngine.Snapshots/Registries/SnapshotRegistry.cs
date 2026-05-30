using GMutagenEngine.Snapshots.Interfaces;
using GMutagenEngine.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Snapshots.Registries;

public class SnapshotRegistry<TId, TData> : InMemoryIndexedSyncRegistry<TId, ISnapshot<TId, TData>>, ISnapshotRegistry<TId, TData> where TId : notnull
{
    
}