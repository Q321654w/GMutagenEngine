using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Middlewares.Async.Interfaces;

public interface IOutMiddleware<TOut> : IOutMiddleware, IOutMiddlewareMark {
    Task<TOut> Invoke(
        Func<CancellationToken, Task<TOut>> next,
        CancellationToken cancellationToken = default);
}