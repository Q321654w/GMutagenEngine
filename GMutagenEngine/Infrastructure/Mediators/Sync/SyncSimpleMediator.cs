namespace GMutagenEngine.Infrastructure.Mediators.Sync
{
    public class SyncSimpleMediator<TId>(ISyncMediatorHandlerRegistry<TId> registry) : ISyncMediator<TId>
    {
        public TOut Send<TIn, TOut>(TIn request, TId? id = default)
            where TIn : ISyncRequest<TOut>
        {
            var handler = registry.ResolveFunc<TIn, TOut>(id)
                          ?? throw new InvalidOperationException(
                              $"No func<{typeof(TIn).Name}, {typeof(TOut).Name}> registered for {request.GetType().Name}");

            return handler.Handle(request);
        }

        public void Send(TId? id = default)
        {
            var handler = registry.ResolveAction(id)
                          ?? throw new InvalidOperationException($"No action handler registered for '{id}'");

            handler.Handle();
        }

        public void Send<TIn>(TIn input, TId? id = default)
            where TIn : ISyncRequest
        {
            var handler = registry.ResolveAction<TIn>(id)
                          ?? throw new InvalidOperationException(
                              $"No action<{typeof(TIn).Name}> handler registered for '{id}'");

            handler.Handle(input);
        }

        public TOut Send<TOut>(TId? id = default)
        {
            var handler = registry.ResolveFunc<TOut>(id)
                          ?? throw new InvalidOperationException(
                              $"No func<{typeof(TOut).Name}> handler registered for '{id}'");

            return handler.Handle();
        }
    }
}
