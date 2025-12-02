namespace GMutagenEngine.Infrastructure.Handlers.Sync.Funcs
{
    public interface ISyncFuncHandler<out TOut>
    {
        TOut Handle();
    }

    public interface ISyncFuncHandler<in TIn, out TOut>
    {
        TOut Handle(TIn data);
    }

 
}