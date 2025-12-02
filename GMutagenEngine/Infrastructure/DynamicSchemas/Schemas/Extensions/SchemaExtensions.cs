using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extensions
{
    public static class SchemaExtensions
    {
        public static ISchema ToSchema(this Type type, SchemaExtractorSettings? settings = null)
        {
            var schemaExtractor = SchemeExtractorPresets.CreateDefault();
            return schemaExtractor.Extract(type, settings);
        }

        public static ISchema ToSchema<T>(SchemaExtractorSettings? settings = null)
        {
            var schemaExtractor = SchemeExtractorPresets.CreateDefault();
            return schemaExtractor.Extract<T>(settings);
        }

        public static ISchema ToSchema(this object obj, SchemaExtractorSettings? settings = null)
        {
            var schemaExtractor = SchemeExtractorPresets.CreateDefault();
            return schemaExtractor.Extract(obj.GetType(), settings);
        }
    }
}