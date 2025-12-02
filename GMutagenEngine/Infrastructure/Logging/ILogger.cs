namespace GMutagenEngine.Infrastructure.Logging
{
    public interface ILogger
    {
        void Log(string data, int logLevel);
    }

    public interface ILogger<T> : ILogger
    {
        
    }
}