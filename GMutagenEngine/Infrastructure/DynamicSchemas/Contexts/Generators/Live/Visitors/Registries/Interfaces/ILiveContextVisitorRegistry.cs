using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Registries.Interfaces
{
    public interface ILiveContextVisitorRegistry : IIndexedSyncRegistry<Type, ILiveContextVisitor>
    {
    }
}