using GMutagenEngine.Schemas.Interfaces;
using GMutagenEngine.Schemas.Registries.Interfaces;
using GMutagenEngine.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Schemas.Registries.Realizations;

public class SchemaRegistry<TId, TMemberId> : InMemoryIndexedSyncRegistry<TId, ISchema<TId, TMemberId>>, ISchemaRegistry<TId, TMemberId>
    where TId : notnull
{
}