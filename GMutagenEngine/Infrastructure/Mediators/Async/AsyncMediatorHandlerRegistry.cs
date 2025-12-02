using GMutagenEngine.Infrastructure.Handlers.Async.Actions;
using GMutagenEngine.Infrastructure.Handlers.Async.Funcs;
using GMutagenEngine.Infrastructure.Identification.Interfaces;


namespace GMutagenEngine.Infrastructure.Mediators.Async
{
    public class AsyncMediatorHandlerRegistry : IAsyncMediatorHandlerRegistry<IId>
    {
        private readonly Dictionary<DoubleTypedId, object> _handlers = new();

        public void RegisterHandler<TIn, TOut>(IAsyncFuncHandler<TIn, TOut> handler, IId? id = null)
            where TIn : IAsyncRequest<TOut>
        {
            var key = DoubleTypedId.From(typeof(TIn), typeof(TOut), id);
            _handlers[key] = handler;
        }

        public IAsyncFuncHandler<TIn, TOut>? ResolveFunc<TIn, TOut>(IId? id = null)
            where TIn : IAsyncRequest<TOut>
        {
            var key = DoubleTypedId.From(typeof(TIn), typeof(TOut), id);
            return _handlers.TryGetValue(key, out var handler)
                ? (IAsyncFuncHandler<TIn, TOut>)handler
                : null;
        }

        public void RegisterAction(IAsyncActionHandler handler, IId? id = null)
        {
            var key = DoubleTypedId.FromValue(id);
            _handlers[key] = handler;
        }

        public void RegisterAction<TIn>(IAsyncActionHandler<TIn> handler, IId? id = null)
        {
            var key = DoubleTypedId.FromFirst(typeof(TIn), id);
            _handlers[key] = handler;
        }

        public void RegisterFunc<TOut>(IAsyncFuncHandler<TOut> handler, IId? id = null)
        {
            var key = DoubleTypedId.FromSecond(typeof(TOut), id);
            _handlers[key] = handler;
        }

        public IAsyncActionHandler? ResolveAction(IId? id = null)
        {
            var key = DoubleTypedId.FromValue(id);
            return _handlers.TryGetValue(key, out var h) ? (IAsyncActionHandler)h : null;
        }

        public IAsyncActionHandler<TIn>? ResolveAction<TIn>(IId? id = null)
        {
            var key = DoubleTypedId.FromFirst(typeof(TIn), id);
            return _handlers.TryGetValue(key, out var h) ? (IAsyncActionHandler<TIn>)h : null;
        }

        public IAsyncFuncHandler<TOut>? ResolveFunc<TOut>(IId? id = null)
        {
            var key = DoubleTypedId.FromSecond(typeof(TOut), id);
            return _handlers.TryGetValue(key, out var h) ? (IAsyncFuncHandler<TOut>)h : null;
        }
    }
}