using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Realizations;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Extensions
{
    public static class SchemaVisitorRegistryExtensions
    {
        public static SchemaVisitorStorage AddDefaults(this SchemaVisitorStorage storage)
        {
            storage.Add(new ComplexTypeSchemaVisitor());
            storage.Add(new CollectionSchemaVisitor());
            storage.Add(new DictionarySchemaVisitor());
            storage.Add(new PrimitiveSchemaVisitor());

            return storage;
        }
    }
}