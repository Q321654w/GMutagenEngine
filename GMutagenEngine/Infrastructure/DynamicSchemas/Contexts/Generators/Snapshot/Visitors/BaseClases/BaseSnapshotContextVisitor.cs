using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.BaseClases
{
    public abstract class BaseSnapshotContextVisitor : ISnapshotContextVisitor
    {
        public abstract IEnumerable<Type> CanVisitTypes();
        public ContextBase Visit(ISchema schema, SnapshotContextGeneratorSettings settings, ContextBase context = null, object instance = null)
        {
            if (settings.ShouldHandle(schema.TargetType) is false)
                return null;

            return VisitInternal(schema, settings, context, instance);
        }
    
        public abstract ContextBase VisitInternal(ISchema schema, SnapshotContextGeneratorSettings settings, ContextBase context = null, object instance = null);
    }
}