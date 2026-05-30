using GMutagenEngine.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Schemas.Extraction;

public class PriorityRegistry<TId, TPriority>(Dictionary<TId, TPriority> storage)
    : InMemoryIndexedSyncStorage<TId, TPriority>(storage),
        IIndexedSyncStorage<TId, TPriority>
    where TId : notnull
    where TPriority : struct
{
    public new TPriority Get(TId id)
        => Storage.GetValueOrDefault(id);
}