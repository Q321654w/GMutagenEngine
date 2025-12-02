using GMutagenEngine.Concept.Sync.Services.Interfaces;

namespace GMutagenEngine.Infrastructure.Mediators.Async
{
    public interface IAsyncMediator<in TId> : IService
    {
        Task Send(TId? id = default, CancellationToken cancellationToken = default);

        Task Send<TIn>(TIn input, TId? id = default, CancellationToken cancellationToken = default)
            where TIn : IAsyncRequest;
    
        Task<TOut> Send<TOut>(TId? id = default, CancellationToken cancellationToken = default);
        Task<TOut> Send<TIn, TOut>(TIn request, TId? id = default, CancellationToken cancellationToken = default)
            where TIn : IAsyncRequest<TOut>;
    }
}