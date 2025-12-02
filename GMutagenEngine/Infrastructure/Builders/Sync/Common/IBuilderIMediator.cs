using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Mediators.Sync;

namespace GMutagenEngine.Infrastructure.Builders.Sync.Common
{
    public interface IBuilderIMediator
    {
        void Send(IId? id = null);

        void Send<TIn>(TIn input, IId? id = null);
    
        TOut Send<TOut>(IId? id = null);
        TOut Send<TIn, TOut>(TIn request, IId? id = null)
            where TIn : ISyncRequest<TOut>;
    }
}