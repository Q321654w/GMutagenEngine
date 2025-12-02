namespace GMutagenEngine.Infrastructure.Middlewares.Async.Interfaces
{
    public interface IMiddleware<in T>
    {
        Task InvokeAsync(T context, Func<CancellationToken, Task> next, CancellationToken cancellationToken = default);
    }
}