using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Handlers.Funcs.Interfaces;

public interface ISyncFuncHandler<out TOut> : ISyncFuncHandlerOut, ISyncFuncHandlerMark {
    TOut Handle();
}

public interface ISyncFuncHandler<in TIn, out TOut> : ISyncFuncHandlerInOut, ISyncFuncHandlerMark {
    TOut? Handle(TIn data);
}
public interface ISyncFuncHandlerMark : ISelfMark<ISyncFuncHandlerMark> {
}
