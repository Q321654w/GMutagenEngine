using GMutagenEngine.Concept.Sync.Services.Interfaces;
using GMutagenEngine.Infrastructure.Handlers.Async.Actions;
using GMutagenEngine.Infrastructure.Handlers.Async.Funcs;

namespace GMutagenEngine.Infrastructure.Mediators.Async
{
    public interface IAsyncMediatorHandlerRegistry<in TId> : IService
    {
        IAsyncActionHandler? ResolveAction(TId? id = default);
        IAsyncActionHandler<TIn>? ResolveAction<TIn>(TId? id = default);
        IAsyncFuncHandler<TOut>? ResolveFunc<TOut>(TId? id = default);
        IAsyncFuncHandler<TRequest, TResponse>? ResolveFunc<TRequest, TResponse>(TId? id = default)
            where TRequest : IAsyncRequest<TResponse>;
    }
}
