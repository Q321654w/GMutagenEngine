using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.BaseClases
{
    public abstract class BaseLiveContextVisitor : ILiveContextVisitor
    {
        public abstract IEnumerable<Type> CanVisitTypes();
        public ContextBase Visit(ISchema schema, LiveContextGeneratorSettings settings, ContextBase context = null, object instance = null)
        {
            if (settings.ShouldHandle(schema.TargetType) is false)
                return null;

            return VisitInternal(schema, settings, context, instance);
        }
    
        public abstract ContextBase VisitInternal(ISchema schema, LiveContextGeneratorSettings settings, ContextBase context = null, object instance = null);
    }
}