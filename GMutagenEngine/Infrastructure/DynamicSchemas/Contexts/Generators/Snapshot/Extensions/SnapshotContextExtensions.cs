using GMutagenEngine.Concept.Sync.Values.Factories.Default.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Extensions
{
    public static class SnapshotContextExtensions
    {
        public static IContext ToSnapshotContext(this object instance,
            ISchemaRegistry schemaRegistry,
            SnapshotContextGeneratorSettings? settings = null)
        {
            var schema = instance.GetType().ToSchema();
            var generator = SnapshotContextGeneratorPresets.CreateDefault(schemaRegistry, new DefaultValueFactory());
            return generator.Generate(schema, settings, null, instance);
        }

        public static IContext ToSnapshotContext(this object instance, ISchemaRegistry schemaRegistry, Schema schema,
            SnapshotContextGeneratorSettings? settings = null)
        {
            var generator = SnapshotContextGeneratorPresets.CreateDefault(schemaRegistry, new DefaultValueFactory());
            return generator.Generate(schema, settings, null, instance);
        }

        public static IContext ToSnapshotContext(this ISchema schema, ISchemaRegistry schemaRegistry,
            SnapshotContextGeneratorSettings? settings = null)
        {
            var generator = SnapshotContextGeneratorPresets.CreateDefault(schemaRegistry, new DefaultValueFactory());
            return generator.Generate(schema, settings);
        }

        public static IContext ToSnapshotContext(this Type type, ISchemaRegistry schemaRegistry,
            SnapshotContextGeneratorSettings? settings = null)
        {
            var schema = type.ToSchema();
            return schema.ToSnapshotContext(schemaRegistry, settings);
        }

        public static IContext ToSnapshotContext<T>(ISchemaRegistry schemaRegistry,
            SnapshotContextGeneratorSettings? settings = null)
        {
            var schema = SchemaExtensions.ToSchema<T>();
            return schema.ToSnapshotContext(schemaRegistry, settings);
        }

        public static IContext ToSnapshotContext<T>(this T instance, ISchemaRegistry schemaRegistry,
            SnapshotContextGeneratorSettings? settings = null)
            where T : class
        {
            return (instance as object).ToSnapshotContext(schemaRegistry, settings);
        }

        public static IContext ToSnapshotContext<T>(this T instance, ISchemaRegistry schemaRegistry, Schema schema,
            SnapshotContextGeneratorSettings? settings = null)
            where T : class
        {
            return (instance as object).ToSnapshotContext(schemaRegistry, schema, settings);
        }
    }
}