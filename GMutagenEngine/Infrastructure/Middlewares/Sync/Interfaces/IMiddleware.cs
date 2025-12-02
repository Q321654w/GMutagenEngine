namespace GMutagenEngine.Infrastructure.Middlewares.Sync.Interfaces
{
    public interface IMiddleware<in T>
    {
        void Invoke(T context, Action next);
    }
}