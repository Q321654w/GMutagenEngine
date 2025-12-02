using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Realizations
{
    public class LiveContextGenerator(ILiveContextVisitorRegistry visitors) : ILiveContextGenerator
    {
        public ContextBase Generate(ISchema schema, LiveContextGeneratorSettings? settings = null,
            ContextBase? context = null, object? instance = null)
        {
            settings ??= new LiveContextGeneratorSettings(this);

            var type = schema.TargetType;

            if (TryGetVisitor(type, out var visitor))
                return visitor.Visit(schema, settings, context, instance);

            throw new Exception($"Could not find any visitor for type: {type.FullName}");
        }

        private bool TryGetVisitor(Type targetType, out ILiveContextVisitor visitor)
        {
            var typeDiscovery = targetType.AsTypeDiscovery();

            foreach (var type in typeDiscovery)
            {
                if (visitors.TryGet(type, out visitor))
                    return true;
            }

            visitor = null;
            return false;
        }
    }
}