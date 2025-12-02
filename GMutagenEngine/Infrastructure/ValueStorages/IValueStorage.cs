using GMutagenEngine.Concept.Sync.Services.Interfaces;
using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.ValueStorages
{
    public interface IValueStorage : IIndexedIndexedSyncStorage<IId, IValue>, IService
    {
    }
}