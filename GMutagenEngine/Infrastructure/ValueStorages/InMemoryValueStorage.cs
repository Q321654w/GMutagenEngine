using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.ValueStorages
{
    public class InMemoryValueStorage(Dictionary<IId, IValue> storage)
        : InMemoryIndexedSyncStorage<IId, IValue>(storage);
}