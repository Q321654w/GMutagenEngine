using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Schemas.Snapshots.Interfaces.Marks;
using GMutagenEngine.Snapshots.Interfaces;

namespace GMutagenEngine.Schemas.Snapshots.Interfaces;

public interface ISchemaSnapshot<TId, TSchemaId, TMemberId> : ISnapshot<TId, ISchema<TSchemaId, TMemberId>>, ISchemaSnapshotMark {
}
public interface ISchemaSnapshotMark : ISelfMark<ISchemaSnapshotMark> {
}
