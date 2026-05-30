using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Middlewares.Async.Interfaces;

public interface IInOutMiddleware<TIn, TOut> : IInOutMiddleware, IInOutMiddlewareMark {
    Task<TOut> Invoke(
        TIn context,
        Func<TIn, CancellationToken, Task<TOut>> next,
        CancellationToken cancellationToken = default);
}