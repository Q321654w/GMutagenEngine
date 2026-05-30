using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Snapshots.Interfaces;
using GMutagenEngine.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Snapshots.Registries;

public interface ISnapshotRegistry<TId, TData> : IIndexedSyncRegistry<TId, ISnapshot<TId, TData>>, ISnapshotRegistryMark {
    
}
public interface ISnapshotRegistryMark : ISelfMark<ISnapshotRegistryMark> {
}
