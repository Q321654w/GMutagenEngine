using GMutagenEngine.Mediators.Api.General.Requests;
using GMutagenEngine.Mediators.Async;

namespace GMutagenEngine.Mediators.Api.Async;

public static class AsyncMediatorExtensions
{
    public static Task<TOut?> Send<TOut, TId>(this IAsyncMediator<TId> mediator, IRequest<TOut> request, TId? id = default)
        where TId : notnull
    {
        return mediator.Send<IRequest<TOut>, TOut>(request, id);
    }
}