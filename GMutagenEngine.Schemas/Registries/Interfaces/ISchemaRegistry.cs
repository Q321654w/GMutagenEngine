using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Schemas.Registries.Interfaces;

public interface ISchemaRegistry<TId, TMemberId> : IIndexedSyncRegistry<TId, ISchema<TId, TMemberId>>, ISchemaRegistryMark {
}
public interface ISchemaRegistryMark : ISelfMark<ISchemaRegistryMark> {
}
