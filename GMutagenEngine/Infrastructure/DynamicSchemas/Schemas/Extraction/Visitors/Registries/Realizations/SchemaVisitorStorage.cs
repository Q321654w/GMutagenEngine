using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Realizations
{
    public class SchemaVisitorStorage(Dictionary<Type, ISchemaVisitor> entities)
        : InMemoryIndexedSyncStorage<Type, ISchemaVisitor>(entities), ISchemaVisitorRegistry
    {
        public SchemaVisitorStorage() : this(new Dictionary<Type, ISchemaVisitor>())
        {
        }

        public void Add(ISchemaVisitor schemaVisitor)
        {
            var index = schemaVisitor.CanVisitTypes();
            foreach (var type in index)
                Add(type, schemaVisitor);
        }
    }
}