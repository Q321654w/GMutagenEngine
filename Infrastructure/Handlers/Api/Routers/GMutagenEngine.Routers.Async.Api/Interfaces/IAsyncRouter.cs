using GMutagenEngine.Handlers.Async.Actions.Interfaces;
using GMutagenEngine.Handlers.Async.Funcs.Interfaces;
using GMutagenEngine.Routers.Async.Api.Marks;

namespace GMutagenEngine.Routers.Async.Api.Interfaces;

public interface IAsyncRouter : IAsyncActionHandler, IAsyncRouterMark
{
}

public interface IAsyncRouterIn<in TIn> : IAsyncActionHandler<TIn>, IAsyncRouterInMark
{
}

public interface IAsyncRouterOut<TOut> : IAsyncFuncHandler<TOut>, IAsyncRouterOutMark
{
}

public interface IAsyncRouterInOut<in TIn, TOut> : IAsyncFuncHandler<TIn, TOut>, IAsyncRouterInOutMark
{
}

public interface IAsyncRouterFanOut<TOut> : IAsyncFuncHandlerFanOut<TOut>, IAsyncRouterFanOutMark
{
}

public interface IAsyncRouterInFanOut<in TIn, TOut> : IAsyncFuncHandlerInFanOut<TIn, TOut>, IAsyncRouterInFanOutMark
{
}
