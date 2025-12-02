namespace GMutagenEngine.Infrastructure.Handlers.Async.Actions
{
    public interface IAsyncActionHandler
    {
        Task Handle(CancellationToken cancellationToken = default);
    }

    public interface IAsyncActionHandler<in TIn>
    {
        Task Handle(TIn data, CancellationToken cancellationToken = default);
    }



    
}