using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Realizations;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Realizations
{
    public class DictionarySchemaVisitor : BaseSchemaVisitor
    {
        private static readonly IEnumerable<Type> SupportedGenericDefinitions = FrameworkStandardTypes.Dictionaries;

        public override IEnumerable<Type> CanVisitTypes() => SupportedGenericDefinitions;

        public override Schema VisitInternal(Type type, SchemaExtractorSettings settings, int depth)
        {
            var schema = new Schema
            {
                TargetType = type,
            };
        
            var (keyType, valueType) = GetDictionaryTypes(type);
            if (keyType == null || valueType == null)
                return schema;

            var kvpType = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType);
            var kvpSchema = new Schema
            {
                TargetType = kvpType,
            };

            var keySchema = settings.Extractor.Extract(keyType, settings, depth);
            kvpSchema.AddMember(new SchemaMemberInfo
            {
                Name = SchemaConstants.KEY_PROPERTY,
                Type = keyType,
                Info = null,
                MemberType = MemberTypes.Property,
                Schema = keySchema
            });

            var valueSchema = settings.Extractor.Extract(valueType, settings, depth);
            kvpSchema.AddMember(new SchemaMemberInfo
            {
                Name = SchemaConstants.VALUE_PROPERTY,
                Type = valueType,
                Info = null,
                MemberType = MemberTypes.Property,
                Schema = valueSchema
            });

            schema.AddMember(new SchemaMemberInfo
            {
                Name = SchemaConstants.ELEMENT_KEY,
                Type = kvpType,
                Info = null,
                MemberType = MemberTypes.Custom,
                Schema = kvpSchema
            });

            return schema;
        }

        protected (Type? keyType, Type? valueType) GetDictionaryTypes(Type type)
        {
            if (type.IsGenericType)
            {
                var def = type.GetGenericTypeDefinition();
                if (SupportedGenericDefinitions.Contains(def))
                {
                    var args = type.GetGenericArguments();
                    return (args[0], args[1]);
                }
            }

            var match = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType &&
                                     SupportedGenericDefinitions.Contains(i.GetGenericTypeDefinition()));

            if (match != null)
            {
                var args = match.GetGenericArguments();
                return (args[0], args[1]);
            }

            return (null, null);
        }
    }
}