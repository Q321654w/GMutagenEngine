using GMutagenEngine.Concept.Sync.Services.Interfaces;
using GMutagenEngine.Infrastructure.Handlers.Sync.Actions;
using GMutagenEngine.Infrastructure.Handlers.Sync.Funcs;

namespace GMutagenEngine.Infrastructure.Mediators.Sync
{
    public interface ISyncMediatorHandlerRegistry<in TId> : IService
    {
        ISyncActionHandler? ResolveAction(TId? id = default);
        ISyncActionHandler<TIn>? ResolveAction<TIn>(TId? id = default);
        ISyncFuncHandler<TOut>? ResolveFunc<TOut>(TId? id = default);
        ISyncFuncHandler<TRequest, TResponse>? ResolveFunc<TRequest, TResponse>(TId? id = default)
            where TRequest : ISyncRequest<TResponse>;
    }
}