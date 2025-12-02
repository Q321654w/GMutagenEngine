using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Realizations;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Realizations
{
    public class CollectionSchemaVisitor : BaseSchemaVisitor
    {
        private static readonly IEnumerable<Type> SupportedGenericDefinitions = FrameworkStandardTypes.Collections;

        public override IEnumerable<Type> CanVisitTypes() => SupportedGenericDefinitions;

        public override Schema VisitInternal(Type type, SchemaExtractorSettings settings, int depth)
        {
            var schema = new Schema
            {
                TargetType = type,
            };

            AddFieldsAndProperties(type, settings, depth, schema);

            var elementType = GetCollectionElementType(type);
            if (elementType == null)
                return schema;

            var elementSchema = settings.Extractor.Extract(elementType, settings, depth);
            schema.AddMember(new SchemaMemberInfo
            {
                Name = SchemaConstants.ELEMENT_KEY,
                Type = elementType,
                Info = null,
                MemberType = MemberTypes.Custom,
                Schema = elementSchema
            });

            return schema;
        }

        protected Type? GetCollectionElementType(Type type)
        {
            if (type.IsArray)
                return type.GetElementType();

            if (type.IsGenericType)
            {
                var def = type.GetGenericTypeDefinition();
                if (SupportedGenericDefinitions.Contains(def))
                    return type.GetGenericArguments()[0];
            }

            var enumerableInterface = type
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType &&
                                     SupportedGenericDefinitions.Contains(i.GetGenericTypeDefinition()));

            return enumerableInterface?.GetGenericArguments()[0];
        }
    }
}