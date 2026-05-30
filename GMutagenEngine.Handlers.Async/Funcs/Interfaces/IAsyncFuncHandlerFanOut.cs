using GMutagenEngine.Handlers.Async.Funcs.Marks;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Handlers.Async.Funcs.Interfaces;

public interface IAsyncFuncHandlerFanOut : IAsyncFuncHandlerFanOutMark
{
}

public interface IAsyncFuncHandlerInFanOut : IAsyncFuncHandlerInFanOutMark
{
}

public interface IAsyncFuncHandlerFanOut<TOut> : IAsyncFuncHandler<IEnumerable<TOut?>>, IAsyncFuncHandlerFanOut, IAsyncFuncHandlerMark
{
}

public interface IAsyncFuncHandlerInFanOut<in TIn, TOut> : IAsyncFuncHandler<TIn, IEnumerable<TOut?>>, IAsyncFuncHandlerInFanOut, IAsyncFuncHandlerMark
{
}

public interface IAsyncFuncHandlerFanOutMark : ISelfMark<IAsyncFuncHandlerFanOutMark>
{
}

public interface IAsyncFuncHandlerInFanOutMark : ISelfMark<IAsyncFuncHandlerInFanOutMark>
{
}
