using GMutagenEngine.Identification.Identifiable.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Snapshots.Interfaces.Marks;

namespace GMutagenEngine.Snapshots.Interfaces;

public interface ISnapshot<TId, out TData> : IIdentifiable<TId>, ISnapshotMark {
    TData Data { get; }
}
public interface ISnapshotMark : ISelfMark<ISnapshotMark> {
}
