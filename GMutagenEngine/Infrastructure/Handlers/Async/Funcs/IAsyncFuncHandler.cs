namespace GMutagenEngine.Infrastructure.Handlers.Async.Funcs
{
    public interface IAsyncFuncHandler<TOut>
    {
        Task<TOut> Invoke(CancellationToken cancellationToken = default);
    }

    public interface IAsyncFuncHandler<in TIn, TOut>
    {
        Task<TOut> Handle(TIn data, CancellationToken cancellationToken = default);
    }

  


}