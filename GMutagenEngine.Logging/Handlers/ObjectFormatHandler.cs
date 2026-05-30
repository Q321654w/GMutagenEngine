using GMutagenEngine.Handlers.Actions.Interfaces;
using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Logging.Formatters;
using GMutagenEngine.Mediators.Api.Sync.Topics;

namespace GMutagenEngine.Logging.Handlers;

public sealed class ObjectFormatHandler<TId>(
    ISyncSingleTopic<TId> syncTopic,
    ISyncFuncHandler<object?, string>? formatter = null)
    : ISyncActionHandler<object?>
{
    public ISyncSingleTopic<TId> SyncTopic => syncTopic;

    private readonly ISyncFuncHandler<object?, string> _formatter = formatter ?? new ObjectFormatter();

    public void Handle(object? data)
    {
        var formatted = _formatter.Handle(data);
        SyncTopic.Publish(formatted);
    }
}
