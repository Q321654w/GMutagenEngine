using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Mediators;
using GMutagenEngine.Storing.Storages.Sync.Indexed;

namespace GMutagenEngine.Logging.Adapters;

public class LoggerContextAdapter<TContext, TConverterId, TKey, TValue> : ISyncLogger<TContext>
{
    private readonly ISyncLogger<IIndexedSyncStorage<TKey, TValue>> _innerLogger;
    private readonly ISyncMediatorSendWithInput<TConverterId>? _converterRouter;
    private readonly ISyncFuncHandler<TContext, IIndexedSyncStorage<TKey, TValue>>? _converterHandler;
    private readonly Func<TContext, IIndexedSyncStorage<TKey, TValue>>? _fallbackConverter;
    private readonly Func<TContext, TConverterId>? _converterIdSelector;
    private readonly TConverterId _defaultConverterId;

    public LoggerContextAdapter(
        ISyncLogger<IIndexedSyncStorage<TKey, TValue>> innerLogger,
        ISyncMediatorSendWithInput<TConverterId> converterRouter,
        TConverterId defaultConverterId = default!,
        Func<TContext, TConverterId>? converterIdSelector = null,
        Func<TContext, IIndexedSyncStorage<TKey, TValue>>? fallbackConverter = null)
    {
        _innerLogger = innerLogger;
        _converterRouter = converterRouter;
        _defaultConverterId = defaultConverterId;
        _converterIdSelector = converterIdSelector;
        _fallbackConverter = fallbackConverter;
    }

    public LoggerContextAdapter(
        ISyncLogger<IIndexedSyncStorage<TKey, TValue>> innerLogger,
        ISyncFuncHandler<TContext, IIndexedSyncStorage<TKey, TValue>> converterHandler,
        Func<TContext, IIndexedSyncStorage<TKey, TValue>>? fallbackConverter = null)
    {
        _innerLogger = innerLogger;
        _converterHandler = converterHandler;
        _fallbackConverter = fallbackConverter;
        _defaultConverterId = default!;
    }

    public void Log(TContext context)
    {
        IIndexedSyncStorage<TKey, TValue>? storage = null;

        if (_converterHandler is not null)
            storage = _converterHandler.Handle(context);

        if (storage is null && _converterRouter is not null)
        {
            var converterId = _converterIdSelector is null ? _defaultConverterId : _converterIdSelector(context);
            storage = _converterRouter.Send<TContext, IIndexedSyncStorage<TKey, TValue>>(context, converterId);
        }

        if (storage is null && _fallbackConverter is not null)
            storage = _fallbackConverter(context);

        if (storage is null)
            throw new InvalidOperationException("Context conversion to indexed storage returned null.");

        _innerLogger.Log(storage);
    }
}

public sealed class LoggerContextAdapter<TCategory, TContext, TConverterId, TKey, TValue>
    : LoggerContextAdapter<TContext, TConverterId, TKey, TValue>, ISyncLogger<TCategory, TContext>
{
    public LoggerContextAdapter(
        ISyncLogger<IIndexedSyncStorage<TKey, TValue>> innerLogger,
        ISyncMediatorSendWithInput<TConverterId> converterRouter,
        TConverterId defaultConverterId = default!,
        Func<TContext, TConverterId>? converterIdSelector = null,
        Func<TContext, IIndexedSyncStorage<TKey, TValue>>? fallbackConverter = null)
        : base(innerLogger, converterRouter, defaultConverterId, converterIdSelector, fallbackConverter)
    {
    }

    public LoggerContextAdapter(
        ISyncLogger<IIndexedSyncStorage<TKey, TValue>> innerLogger,
        ISyncFuncHandler<TContext, IIndexedSyncStorage<TKey, TValue>> converterHandler,
        Func<TContext, IIndexedSyncStorage<TKey, TValue>>? fallbackConverter = null)
        : base(innerLogger, converterHandler, fallbackConverter)
    {
    }
}

