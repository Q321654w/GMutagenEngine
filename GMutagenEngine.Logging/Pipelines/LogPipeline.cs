using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Realizations.Single;
using GMutagenEngine.Logging.Handlers;
using GMutagenEngine.Pipelines.Assembler;

namespace GMutagenEngine.Logging.Pipelines;

public class LogPipeline<TKey, TValue, TTopicId> : TypedPipeline
{
    public IId Formatter { get; } = new SingleId<string>("formatter");
    public IId ConsoleHandler { get; } = new SingleId<string>("console");

    public LogPipeline()
    {
        Create()
            .Add<ContextFormatHandler<TKey, TValue, TTopicId>>(Formatter)
            .Call(f => f.SyncChannel, ConsoleHandler)
            .Add<ConsoleLogHandler>(ConsoleHandler)
            .Call<ContextFormatHandler<TKey, TValue, TTopicId>>(f => f.SyncChannel, Formatter, ConsoleHandler);
    }
}