using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Realizations
{
    public class ComplexTypeSchemaVisitor : BaseSchemaVisitor
    {
        private static readonly Type[] SupportedGenericDefinitions =
        {
            typeof(object),
        };

        public override IEnumerable<Type> CanVisitTypes() => SupportedGenericDefinitions;

        public override Schema VisitInternal(Type type, SchemaExtractorSettings settings, int depth)
        {
            var schema = new Schema
            {
                TargetType = type,
            };

            AddFieldsAndProperties(type, settings, depth, schema);
        
            return schema;
        }
    }
}