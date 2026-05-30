using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Schemas.Storages.Interfaces;
using GMutagenEngine.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Schemas.Storages.Realizations;

public class SchemaStorage<TId, TMemberId> : InMemoryIndexedSyncStorage<TId, ISchema<TId, TMemberId>>, ISchemaStorage<TId, TMemberId>
    where TId : notnull
{
}