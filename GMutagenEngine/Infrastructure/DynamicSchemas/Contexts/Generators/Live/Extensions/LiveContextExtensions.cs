using GMutagenEngine.Concept.Sync.Values.Factories.Default.Realizations;
using GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Presets;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extensions;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Extensions
{
    public static class LiveContextExtensions
    {
        public static IContext ToLiveContext(this object instance, ISchemaRegistry schemaRegistry, IContextRegistry contextRegistry, LiveContextGeneratorSettings? settings = null)
        {
            var schema = instance.GetType().ToSchema();
            var generator =
                LiveContextGeneratorPresets.CreateDefault(schemaRegistry, contextRegistry, new ReflectionValueFactory(), new DefaultValueFactory());
            return generator.Generate(schema, settings, null, instance);
        }

        public static IContext ToLiveContext(this object instance, ISchemaRegistry schemaRegistry, IContextRegistry contextRegistry, Schema schema,
            LiveContextGeneratorSettings? settings = null)
        {
            var generator =
                LiveContextGeneratorPresets.CreateDefault(schemaRegistry, contextRegistry, new ReflectionValueFactory(), new DefaultValueFactory());
            return generator.Generate(schema, settings, null, instance);
        }

        public static IContext ToLiveContext<T>(this T instance, ISchemaRegistry schemaRegistry, IContextRegistry contextRegistry, LiveContextGeneratorSettings? settings = null)
            where T : class
        {
            return (instance as object).ToLiveContext(schemaRegistry, contextRegistry, settings);
        }
    }
}