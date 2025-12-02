using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Extensions
{
    public static class SchemeExtractorExtensions
    {
        public static ISchema Extract<T>(this ISchemaExtractor schemaExtractor, SchemaExtractorSettings? settings = null)
        {
            return schemaExtractor.Extract(typeof(T), settings);
        }
    }
}