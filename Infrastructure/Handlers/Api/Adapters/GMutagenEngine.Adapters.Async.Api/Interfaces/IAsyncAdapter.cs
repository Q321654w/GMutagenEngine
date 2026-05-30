using GMutagenEngine.Adapters.Async.Api.Marks;
using GMutagenEngine.Handlers.Async.Actions.Interfaces;
using GMutagenEngine.Handlers.Async.Funcs.Interfaces;

namespace GMutagenEngine.Adapters.Async.Api.Interfaces;

public interface IAsyncAdapter : IAsyncActionHandler, IAsyncAdapterMark
{
}

public interface IAsyncAdapterIn<in TIn> : IAsyncActionHandler<TIn>, IAsyncAdapterInMark
{
}

public interface IAsyncAdapterOut<TOut> : IAsyncFuncHandler<TOut>, IAsyncAdapterOutMark
{
}

public interface IAsyncAdapterInOut<in TIn, TOut> : IAsyncFuncHandler<TIn, TOut>, IAsyncAdapterInOutMark
{
}

public interface IAsyncAdapterFanOut<TOut> : IAsyncFuncHandlerFanOut<TOut>, IAsyncAdapterFanOutMark
{
}

public interface IAsyncAdapterInFanOut<in TIn, TOut> : IAsyncFuncHandlerInFanOut<TIn, TOut>, IAsyncAdapterInFanOutMark
{
}
