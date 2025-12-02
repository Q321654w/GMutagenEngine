using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Builders.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Realizations;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Builders.Realizations
{
    public sealed class SchemaBuilder : ISchemaBuilder
    {
        private readonly Schema _schema = new();

        private SchemaBuilder(Type? targetType = null)
        {
            _schema.TargetType = targetType;
        }

        public static SchemaBuilder For(Type? type)
            => new SchemaBuilder(type);

        public static SchemaBuilder For<T>()
            => new SchemaBuilder(typeof(T));

        public static SchemaBuilder Anonymous()
            => new SchemaBuilder(null);
    
        public SchemaBuilder Member(
            string name,
            Type memberType,
            MemberTypes kind = MemberTypes.Custom,
            Func<object, object>? getter = null,
            Action<object, object>? setter = null)
        {
            _schema.Members[name] = new SchemaMemberInfo
            {
                Name = name,
                Type = memberType,
                MemberType = kind,
                Getter = getter,
                Setter = setter
            };

            return this;
        }

        public SchemaBuilder Nested(
            string name,
            Action<SchemaBuilder> nestedBuilder,
            MemberTypes kind = MemberTypes.Custom)
        {
            var b = Anonymous();
            nestedBuilder(b);
            var nestedSchema = b.Build();

            _schema.Members[name] = new SchemaMemberInfo
            {
                Name = name,
                Type = nestedSchema.TargetType,
                MemberType = kind,
                Schema = nestedSchema
            };

            return this;
        }

        public ISchema Build() => _schema;
    }
}