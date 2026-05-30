using GMutagenEngine.Handlers.Funcs.Marks;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Handlers.Funcs.Interfaces;

public interface ISyncFuncHandlerFanOut : ISyncFuncHandlerFanOutMark
{
}

public interface ISyncFuncHandlerInFanOut : ISyncFuncHandlerInFanOutMark
{
}

public interface ISyncFuncHandlerFanOut<out TOut> : ISyncFuncHandler<IEnumerable<TOut?>>, ISyncFuncHandlerFanOut, ISyncFuncHandlerMark
{
}

public interface ISyncFuncHandlerInFanOut<in TIn, out TOut> : ISyncFuncHandler<TIn, IEnumerable<TOut?>>, ISyncFuncHandlerInFanOut, ISyncFuncHandlerMark
{
}

public interface ISyncFuncHandlerFanOutMark : ISelfMark<ISyncFuncHandlerFanOutMark>
{
}

public interface ISyncFuncHandlerInFanOutMark : ISelfMark<ISyncFuncHandlerInFanOutMark>
{
}
