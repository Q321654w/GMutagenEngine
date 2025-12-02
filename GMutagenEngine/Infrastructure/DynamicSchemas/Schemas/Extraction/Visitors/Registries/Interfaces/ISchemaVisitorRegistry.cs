using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Interfaces
{
    public interface ISchemaVisitorRegistry : IIndexedSyncRegistry<Type, ISchemaVisitor>
    {
        void Add(ISchemaVisitor schemaVisitor);
    }
}