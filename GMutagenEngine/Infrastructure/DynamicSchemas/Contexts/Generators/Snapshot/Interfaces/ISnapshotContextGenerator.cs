using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Interfaces.Marks;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Interfaces
{
    public interface ISnapshotContextGenerator : ISnapshotContextGeneratorMark
    {
        ContextBase Generate(ISchema schema, SnapshotContextGeneratorSettings settings, ContextBase context, object instance);
    }
}