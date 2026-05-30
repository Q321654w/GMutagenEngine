using GMutagenEngine.Handlers.Async.Funcs.Marks;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Handlers.Async.Funcs.Interfaces;

public interface IAsyncFuncHandler<TOut> : IAsyncFuncHandlerOut, IAsyncFuncHandlerOutMark, IAsyncFuncHandlerMark {
    Task<TOut> Handle(CancellationToken cancellationToken = default);
}

public interface IAsyncFuncHandler<in TIn, TOut> : IAsyncFuncHandlerInOut, IAsyncFuncHandlerInOutMark, IAsyncFuncHandlerMark {
    Task<TOut> Handle(TIn data, CancellationToken cancellationToken = default);
}
public interface IAsyncFuncHandlerMark : ISelfMark<IAsyncFuncHandlerMark> {
}
