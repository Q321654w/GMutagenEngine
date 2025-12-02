using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Interfaces
{
    public interface ISchemaVisitor
    {
        IEnumerable<Type> CanVisitTypes();
        Schema Visit(Type type, SchemaExtractorSettings settings, int depth);
    }
}