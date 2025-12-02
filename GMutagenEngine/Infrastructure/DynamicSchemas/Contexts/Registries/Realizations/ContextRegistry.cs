using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Realizations
{
    public class ContextRegistry : InMemoryIndexedSyncStorage<object, IContext>, IContextRegistry
    {
    }
}