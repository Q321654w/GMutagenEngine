using GMutagenEngine.Mediators.Api.Sync.Topics;

namespace GMutagenEngine.Logging.Loggers;

public sealed class SyncLogger<TCategory, TContext, TId>(ISyncSingleTopic<TId> syncTopic) : ISyncLogger<TCategory, TContext>
{
    public ISyncSingleTopic<TId> SyncTopic => syncTopic;

    public void Log(TContext context)
    {
        SyncTopic.Publish(context);
    }
}
