using GMutagenEngine.Handlers.Actions.Intarfeces;
using GMutagenEngine.Handlers.Actions.Interfaces;
using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Routers.Sync.Api.Marks;

namespace GMutagenEngine.Routers.Sync.Api.Interfaces;

public interface ISyncRouter : ISyncActionHandler, ISyncRouterMark
{
}

public interface ISyncRouterIn<in TIn> : ISyncActionHandler<TIn>, ISyncRouterInMark
{
}

public interface ISyncRouterOut<out TOut> : ISyncFuncHandler<TOut>, ISyncRouterOutMark
{
}

public interface ISyncRouterInOut<in TIn, out TOut> : ISyncFuncHandler<TIn, TOut>, ISyncRouterInOutMark
{
}

public interface ISyncRouterFanOut<out TOut> : ISyncFuncHandlerFanOut<TOut>, ISyncRouterFanOutMark
{
}

public interface ISyncRouterInFanOut<in TIn, out TOut> : ISyncFuncHandlerInFanOut<TIn, TOut>, ISyncRouterInFanOutMark
{
}
