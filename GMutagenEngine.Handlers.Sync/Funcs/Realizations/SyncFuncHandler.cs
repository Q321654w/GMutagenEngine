using GMutagenEngine.Handlers.Funcs.Interfaces;

namespace GMutagenEngine.Handlers.Funcs.Realizations;

public class SyncFuncHandler<TOut>(Func<TOut> func) : ISyncFuncHandler<TOut>
{
    public TOut Handle()
    {
        return func.Invoke();
    }
}

public class SyncFuncHandler<TIn, TOut>(Func<TIn, TOut> func) : ISyncFuncHandler<TIn, TOut>
{
    public TOut Handle(TIn data)
    {
        return func.Invoke(data);
    }
}