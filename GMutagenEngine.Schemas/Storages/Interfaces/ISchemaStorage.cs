using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Schemas.Storages.Interfaces;

public interface ISchemaStorage<TId, TMemberId> : IIndexedSyncStorage<TId, ISchema<TId, TMemberId>>, ISchemaStorageMark {
}
public interface ISchemaStorageMark : ISelfMark<ISchemaStorageMark> {
}
