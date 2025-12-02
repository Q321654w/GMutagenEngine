using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Interfaces.Marks;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Interfaces
{
    public interface ISnapshotContextVisitor : ISnapshotContextVisitorMark
    {
        public IEnumerable<Type> CanVisitTypes();
    
        ContextBase Visit(ISchema schema, SnapshotContextGeneratorSettings settings, ContextBase? context = null,
            object? instance = null);
    }
}