using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Realizations;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.BaseClases
{
    public abstract class BaseSchemaVisitor : ISchemaVisitor
    {
        public abstract IEnumerable<Type> CanVisitTypes();

        public Schema Visit(Type type, SchemaExtractorSettings settings, int depth)
        {
            if (depth > settings.MaxDepth)
                return null;
        
            if (settings.ShouldHandle(type) is false)
                return null;

            return VisitInternal(type, settings, depth);
        }

        public abstract Schema VisitInternal(Type type, SchemaExtractorSettings settings, int depth);

        protected void AddFieldsAndProperties(Type type, SchemaExtractorSettings settings, int depth, Schema schema)
        {
            foreach (var field in type.GetFields(settings.FieldsBindingFlags))
            {
                var memberSchema = settings.Extractor.Extract(field.FieldType, settings, depth);
                schema.AddMember(new SchemaMemberInfo
                {
                    Name = field.Name,
                    Type = field.FieldType,
                    Info = field,
                    MemberType = MemberTypes.Field,
                    Schema = memberSchema,
                    Getter = field.GetValue,
                    Setter = field.SetValue,
                });
            }

            foreach (var property in type.GetProperties(settings.PropertiesBindingFlags))
            {
                var memberSchema = settings.Extractor.Extract(property.PropertyType, settings, depth);
                schema.AddMember(new SchemaMemberInfo
                {
                    Name = property.Name,
                    Type = property.PropertyType,
                    Info = property,
                    MemberType = MemberTypes.Property,
                    Schema = memberSchema,
                    Getter = property.CanRead ? property.GetValue : null,
                    Setter = property.CanWrite ?  property.SetValue : null,
                });
            }
        }
    }
}