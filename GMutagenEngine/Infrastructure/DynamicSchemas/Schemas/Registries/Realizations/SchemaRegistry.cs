using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Realizations
{
    public class SchemaRegistry : InMemoryIndexedSyncRegistry<Type, ISchema>, ISchemaRegistry
    {
    }
}