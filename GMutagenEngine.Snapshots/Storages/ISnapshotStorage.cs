using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Snapshots.Interfaces;
using GMutagenEngine.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Snapshots.Storages;

public interface ISnapshotStorage<TId, TData> : IIndexedSyncStorage<TId, ISnapshot<TId, TData>>, ISnapshotStorageMark {
    
}
public interface ISnapshotStorageMark : ISelfMark<ISnapshotStorageMark> {
}
