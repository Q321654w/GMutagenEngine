using GMutagenEngine.Concept.Sync.Services.Interfaces;

namespace GMutagenEngine.Infrastructure.Mediators.Sync
{
    public interface ISyncMediator<in TId> : IService
    {
        void Send(TId? id = default);

        void Send<TIn>(TIn input, TId? id = default)
            where TIn : ISyncRequest;
    
        TOut Send<TOut>(TId? id = default);
        TOut Send<TIn, TOut>(TIn request, TId? id = default)
            where TIn : ISyncRequest<TOut>;
    }
}