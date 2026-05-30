using GMutagenEngine.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Logging.IndexedStorage.Api.Interfaces;

public interface IIndexedStorageSyncLogger<TKey, TValue> : ISyncLogger<IIndexedSyncStorage<TKey, TValue>>
{
}

public interface IIndexedStorageSyncLogger<TCategory, TKey, TValue>
    : ISyncLogger<TCategory, IIndexedSyncStorage<TKey, TValue>>
{
}