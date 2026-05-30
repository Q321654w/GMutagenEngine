using GMutagenEngine.Schemas.Interfaces;

namespace GMutagenEngine.Schemas.Extraction;

public static class SchemaExtractorApiExtensions
{
    public static ISchema<string, string> Extract<T>(this ISchemaExtractor schemaExtractor, T obj)
        => schemaExtractor.Extract(obj?.GetType(), obj);
}