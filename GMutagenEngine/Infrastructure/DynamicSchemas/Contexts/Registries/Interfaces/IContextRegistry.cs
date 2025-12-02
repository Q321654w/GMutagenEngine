using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces
{
    public interface IContextRegistry : IIndexedIndexedSyncStorage<object, IContext>
    {
    }
}