using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Realizations;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Presets
{
    public static class SchemeExtractorPresets
    {
        public static SchemaExtractor CreateDefault()
        {
            var storage = new SchemaVisitorStorage();
            storage.AddDefaults();

            return new SchemaExtractor(storage);
        }
    }
}