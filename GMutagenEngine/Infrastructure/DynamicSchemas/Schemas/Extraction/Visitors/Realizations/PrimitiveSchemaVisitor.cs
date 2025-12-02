using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Realizations
{
    public class PrimitiveSchemaVisitor : BaseSchemaVisitor
    {
        public override IEnumerable<Type> CanVisitTypes() => FrameworkStandardTypes.Primitive;

        public override Schema VisitInternal(Type type, SchemaExtractorSettings settings, int depth)
        {
            return new Schema
            {
                TargetType = type,
            };
        }
    }
}