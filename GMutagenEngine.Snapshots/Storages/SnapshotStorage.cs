using GMutagenEngine.Snapshots.Interfaces;
using GMutagenEngine.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Snapshots.Storages;

public class SnapshotStorage<TId, TData> : InMemoryIndexedSyncStorage<TId, ISnapshot<TId, TData>>, ISnapshotStorage<TId, TData> where TId : notnull
{
    
}