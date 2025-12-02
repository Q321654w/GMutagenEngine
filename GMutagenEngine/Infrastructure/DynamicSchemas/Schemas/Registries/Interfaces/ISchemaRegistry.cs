using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces
{
    public interface ISchemaRegistry : IIndexedSyncRegistry<Type, ISchema>
    {
    }
}