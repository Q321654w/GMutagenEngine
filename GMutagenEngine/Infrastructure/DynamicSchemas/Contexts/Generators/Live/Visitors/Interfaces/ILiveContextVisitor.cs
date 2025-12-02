using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Interfaces.Marks;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Interfaces
{
    public interface ILiveContextVisitor : ILiveContextVisitorMark
    {
        public IEnumerable<Type> CanVisitTypes();

        ContextBase Visit(ISchema schema, LiveContextGeneratorSettings settings, ContextBase? context = null,
            object? instance = null);
    }
}