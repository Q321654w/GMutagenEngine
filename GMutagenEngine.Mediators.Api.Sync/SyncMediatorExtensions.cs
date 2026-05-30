using GMutagenEngine.Mediators.Api.General.Requests;

namespace GMutagenEngine.Mediators.Api.Sync;

public static class SyncMediatorExtensions
{
    public static TOut? Send<TOut, TId>(this ISyncMediator<TId> mediator, IRequest<TOut> request, TId? id = default)
        where TId : notnull
    {
        return mediator.Send<IRequest<TOut>, TOut>(request, id);
    }
}