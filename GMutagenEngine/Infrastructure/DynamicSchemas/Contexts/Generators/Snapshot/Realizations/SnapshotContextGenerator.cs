using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Realizations
{
    public class SnapshotContextGenerator(ISnapshotContextVisitorRegistry visitors) : ISnapshotContextGenerator
    {
        public ContextBase Generate(ISchema schema, SnapshotContextGeneratorSettings? settings = null,
            ContextBase? context = null, object? instance = null)
        {
            settings ??= new SnapshotContextGeneratorSettings(this);

            var type = schema.TargetType;

            if (settings.ShouldHandle(type) is false)
                return null;
        
            if (TryGetVisitor(type, out var visitor))
                return visitor.Visit(schema, settings, context, instance);

            throw new Exception($"Could not find any visitor for type: {type.FullName}");
        }

        private bool TryGetVisitor(Type targetType, out ISnapshotContextVisitor visitor)
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