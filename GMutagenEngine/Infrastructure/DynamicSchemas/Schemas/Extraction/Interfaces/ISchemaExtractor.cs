using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Interfaces.Marks;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Interfaces
{
    public interface ISchemaExtractor : ISchemaExtractorMark
    {
        ISchema Extract(Type type, SchemaExtractorSettings? settings = null, int depth = 0);
    }
}