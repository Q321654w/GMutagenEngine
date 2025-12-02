using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Mediators.Async;

namespace GMutagenEngine.Infrastructure.Builders.Async.Common
{
    public interface IBuilderIMediator
    {
        Task Send(IId? id = null, CancellationToken cancellationToken = default);

        Task Send<TIn>(TIn input, IId? id, CancellationToken cancellationToken = default);
    
        Task<TOut> Send<TOut>(IId? id = null, CancellationToken cancellationToken = default);
        Task<TOut> Send<TIn, TOut>(TIn request, IId? id = null, CancellationToken cancellationToken = default)
            where TIn : IAsyncRequest<TOut>;
    }
}