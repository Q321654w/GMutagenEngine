using GMutagenEngine.Handlers.Actions.Interfaces;
using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Mediators.Api.Sync.Channels;
using GMutagenEngine.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Logging.Handlers;

public class ContextFormatHandler<TKey, TValue, TId>(
    ISyncFuncHandler<IIndexedSyncStorage<TKey, TValue>, string> formatter)
    : ISyncActionHandler<IIndexedSyncStorage<TKey, TValue>>
{
    public ISyncSingleChannel<TId, string> SyncChannel { get; set; }

    public void Handle(IIndexedSyncStorage<TKey, TValue> data)
    {
        var formatted = formatter.Handle(data);
        SyncChannel.Publish(formatted);
    }
}
