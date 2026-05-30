using GMutagenEngine.Adapters.Sync.Api.Marks;
using GMutagenEngine.Handlers.Actions.Intarfeces;
using GMutagenEngine.Handlers.Actions.Interfaces;
using GMutagenEngine.Handlers.Funcs.Interfaces;

namespace GMutagenEngine.Adapters.Sync.Api.Interfaces;

public interface ISyncAdapter : ISyncActionHandler, ISyncAdapterMark
{
}

public interface ISyncAdapterIn<in TIn> : ISyncActionHandler<TIn>, ISyncAdapterInMark
{
}

public interface ISyncAdapterOut<out TOut> : ISyncFuncHandler<TOut>, ISyncAdapterOutMark
{
}

public interface ISyncAdapterInOut<in TIn, out TOut> : ISyncFuncHandler<TIn, TOut>, ISyncAdapterInOutMark
{
}

public interface ISyncAdapterFanOut<out TOut> : ISyncFuncHandlerFanOut<TOut>, ISyncAdapterFanOutMark
{
}

public interface ISyncAdapterInFanOut<in TIn, out TOut> : ISyncFuncHandlerInFanOut<TIn, TOut>, ISyncAdapterInFanOutMark
{
}
